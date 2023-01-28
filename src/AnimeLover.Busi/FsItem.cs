using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Busi
{
    /// <summary>
    /// 文件项目
    /// </summary>
    public class FsItem
    {
        /// <summary>
        /// guid
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 媒体类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 是否为文件
        /// </summary>
        public bool IsFile { get; set; }
    }
}
