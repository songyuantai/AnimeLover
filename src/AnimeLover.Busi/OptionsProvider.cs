using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Busi
{
    public class OptionsProvider
    {
        public static Dictionary<string, string> GetAnimeSeasons()
        {
            var year = DateTime.Now.Year;
            var season = DateTime.Now.Month switch
            {
                1 or 2 or 3 => 1,
                4 or 5 or 6 => 2,
                7 or 8 or 9 => 3,
                10 or 11 or 12 => 4,
                _ => 0,
            };

            List<ValueTuple<int, int>> values = new();
            const int MAX_SEASON = 12;
            while(year > 0)
            {
                while (season > 0)
                {
                    if (values.Count > MAX_SEASON)
                        break;

                    values.Add((year, season));
                    season--;
                }
                season = 4;
                year--;
            }

            return values.Select(m =>
            {
                var year = m.Item1;
                var season = m.Item2 == 1 ? "01" : (m.Item2 == 2 ? "04" : (m.Item2 == 3 ? "07" : "10"));

                return $"{year}-{season}";
            }).ToDictionary(m => m, m => m);
        }


    }
}
