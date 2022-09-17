using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using AngleSharp.Media;
using AnimeLover.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Busi
{
    public class ComcatProvider
    {
        #region 获取新番
        /// <summary>
        /// 获取新番列表
        /// </summary>
        /// <returns></returns>
        public static async Task<List<SeasonAnime>> GetSeasonAnimies()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(SearchEngine.ComitCat);
            var table = document.QuerySelectorAll(".box .table_fixed")[0];
            var rows = table.QuerySelectorAll("tr");
            var newAnimies = new List<SeasonAnime>();
            var season = Util.GetCurrentSeason();
            for(var index = 1; index < rows.Length; index++)
            {
                var row = rows[index];
                var tds = row.QuerySelectorAll("td");
                var textTime = tds[0].TextContent.Trim().TrimStart('●');
                (var day, var dayText) = GetDateOfWeek(textTime);
                foreach (var ancher in tds[1].QuerySelectorAll("a"))
                {
                    if (ancher.TextContent.Contains("新番") || ancher.TextContent.Contains("配信"))
                    {
                        continue;
                    }

                    var animeName = ancher.TextContent.Trim();

                    newAnimies.Add(new SeasonAnime
                    {
                        Name = animeName,
                        DayOfWeek = day,
                        DayOfWeekText = dayText,
                        Keyword = $"{animeName} 1080p",
                        Season = season,
                        
                    });
                }
            }

            context.Dispose();

            return newAnimies;
        }

        /// <summary>
        /// 获取星期几
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private static (int, string) GetDateOfWeek(string day)
        {
            return day switch
            {
                "星期一" => (1, "星期一"),
                "星期二" => (2, "星期二"),
                "星期三" => (3, "星期三"),
                "星期四" => (4, "星期四"),
                "星期五" => (5, "星期五"),
                "星期六" => (6, "星期六"),
                "星期日" => (0, "星期天"),
                "剧场版" => (7, "剧场版"),
                "特殊放送" => (8, "特殊放送"),
                "今天" => DateTime.Now.DayOfWeek switch
                {
                    DayOfWeek.Sunday => (0, "星期天"),
                    DayOfWeek.Monday => (1, "星期一"),
                    DayOfWeek.Tuesday => (2, "星期二"),
                    DayOfWeek.Wednesday => (3, "星期三"),
                    DayOfWeek.Thursday => (4, "星期四"),
                    DayOfWeek.Friday => (5, "星期五"),
                    DayOfWeek.Saturday => (6, "星期六"),
                    _ => (-1, string.Empty),
                },

                _ => (-1, string.Empty)
            };
        }

        #endregion

        /// <summary>
        /// 获取动漫分集信息
        /// </summary>
        /// <param name="animieId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static async Task<List<AnimeVideo>> GetAnimeVideoAsync(int animieId, string keyword)
        {
            keyword = keyword.Replace(" ", "+");
            var result = new List<AnimeVideo>();

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var db = BlazorApp.Database;
            var videos = db.Queryable<AnimeVideo>().Where(m => m.AnimeId == animieId).ToList();
            var anime = db.Queryable<Anime>().Where(m => m.Id == animieId).First();
            for (var episode = 1; episode <= 24; episode++)
            {
                var txtEpisode = episode.ToString().PadLeft(2, '0');
                if(videos.Any(m => m.Episode == txtEpisode))
                {
                    continue;
                }

                var key = keyword + $"+{txtEpisode}";
                var url = SearchEngine.ComitCat + "/search.php?keyword=" + key;
                var document = await context.OpenAsync(url);
                var rows = document.QuerySelectorAll("#listTable>tbody>tr");
                var tdCount = rows.Children("td").Count();
                
                if (rows.Length > 0 && tdCount > 1)
                {
                    //至少集数完全匹配
                    var row = rows.FirstOrDefault(row => row.QuerySelector("td:nth-child(3) > a").TextContent.Contains(txtEpisode));
                    if (null == row)
                        break;

                    var anchor = row.QuerySelector("td:nth-child(3) > a") as IHtmlAnchorElement;

                    var name = anchor.Text.Trim();
                    var href = anchor.Href;
                    var detail = await context.OpenAsync(anchor.Href);
                    IElement span;
                    while(true)
                    {
                        span = detail.QuerySelector("#text_hash_id");
                        if(null != span)
                        {
                            break;
                        }

                        await Task.Delay(100);

                        detail.Load(anchor.Href);
                    }

                    var link = "magnet:?xt=urn:btih:" + detail.QuerySelector("#text_hash_id").TextContent.Trim().Replace("，特征码：", string.Empty);
                    var video = new AnimeVideo
                    {
                        AnimeId = animieId,
                        Episode = txtEpisode,
                        CreatedDate = DateTime.Now,
                        Name = name,
                        OriginUrl = href,
                        MagnetLink = link,
                        PhysicalPath = string.Empty
                    };

                    await db.Insertable(video).ExecuteCommandIdentityIntoEntityAsync();

                    if (video.Id > 0)
                    {
                        result.Add(video);
                    }

                    await Task.Delay(500);
                }
                else
                {
                    break;
                }

            }

            context.Dispose();

            return result;
        }
    }
}