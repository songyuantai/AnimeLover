using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Model
{
    [SugarTable("AnimeVideo")]
    public class AnimeVideo
    {
        /// <summary>
        /// 视频id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 动漫ID
        /// </summary>
        public int AnimeId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 物理路径
        /// </summary>
        public string PhysicalPath { get; set; }

        /// <summary>
        /// 集数
        /// </summary>
        public string Episode { get; set; }

        /// <summary>
        /// 磁力连接
        /// </summary>
        public string MagnetLink { get; set; }

        /// <summary>
        /// 原始网址
        /// </summary>
        public string OriginUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 上次播放时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public TimeSpan? LastPlayTime { get; set; }
    }
}
