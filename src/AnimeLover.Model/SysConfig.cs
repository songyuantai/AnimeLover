using SqlSugar;

namespace AnimeLover.Model
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [SugarTable("SysConfig")]
    public class SysConfig
    {
        /// <summary>
        /// 配置id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ConfigId { get; set; }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string Addr { get; set; }

        /// <summary>
        /// qbittorrent webui地址
        /// </summary>
        public string QBitApi { get; set; }

        /// <summary>
        /// qbittorrent 用户名
        /// </summary>
        public string QBitUser { get; set; }

        /// <summary>
        /// qbittorrent 密码
        /// </summary>
        public string QBitPassword { get; set; }

    }
}