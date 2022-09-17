using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Busi
{
    public class QBitTorrentApi
    {
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="link"></param>
        /// <param name="path"></param>
        public static async Task Download(string link, string path)
        {
            var config = BlazorApp.Config;
            var client = new QBittorrent.Client.QBittorrentClient(new Uri(config.QBitApi));
            await client.LoginAsync(config.QBitUser, config.QBitPassword);
            var req = new QBittorrent.Client.AddTorrentUrlsRequest(new Uri(link)) { DownloadFolder = path };
            await client.AddTorrentsAsync(req);
        }
    }
}
