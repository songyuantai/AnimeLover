using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Model
{
    /// <summary>
    /// 季度新番
    /// </summary>
    public class SeasonAnime
    {
        /// <summary>
        /// 配置id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int SeasonAnimeId { get; set; }

        /// <summary>
        /// 季度
        /// </summary>
        public string Season { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public int DayOfWeek { get; set; }

        /// <summary>
        /// 更新时间文本
        /// </summary>
        public string DayOfWeekText { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
    }
}
