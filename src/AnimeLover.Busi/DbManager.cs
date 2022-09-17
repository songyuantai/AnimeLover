using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Busi
{
    /// <summary>
    /// 数据库管理器
    /// </summary>
    public class DbManager
    {
        public const string DB_FILE = "animelover.db";

        public const string MODEL_NAME_SPACE = "AnimeLover.Model";

        public static void Merge()
        {
            var conn = BlazorApp.Database;
            conn.CurrentConnectionConfig.InitKeyType = InitKeyType.Attribute;
            conn.DbMaintenance.CreateDatabase();
            conn.CodeFirst.InitTables(MODEL_NAME_SPACE);
        }

    }
}
