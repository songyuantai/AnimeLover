using SqlSugar;

namespace AnimeLover.Busi
{
    public class DbConfig
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString { get; } = "Data Source=animelover.db;Cache=Shared";

        /// <summary>
        /// 获取配置
        /// </summary>
        public static ConnectionConfig SugarConfig { get; } = new()
        {
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute,
            ConnectionString = ConnectionString,
            DbType = DbType.Sqlite
        };

        /// <summary>
        /// 数据库是否就绪
        /// </summary>
        public static bool Ready => File.Exists(DbManager.DB_FILE);

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient GetClient()
        {
            return new SqlSugarClient(SugarConfig);
        }
    }
}