using AnimeLover.Model;
using AnimeLover.Busi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace AnimeLover.Busi
{
    public class BlazorApp
    {

        private static SysConfig _sysConfig;

        /// <summary>
        /// 系统配置
        /// </summary>
        public static SysConfig Config
        {
            get 
            { 
                if(null == _sysConfig)
                {
                    _sysConfig = Database.Queryable<SysConfig>().First();
                }
                return _sysConfig;
            }

            set 
            { 
                _sysConfig = value; 
            }
        }

        private static SqlSugarClient _database;

        /// <summary>
        /// 数据库实例
        /// </summary>
        public static SqlSugarClient Database {
            get
            {
                if (null == _database)
                {
                    _database = DbConfig.GetClient();
                }
                return _database;
            }

            set
            {
                _database = value;
            }
        }

        //public static FileExtensionContentTypeProvider ContentTypeProvider => new FileExtensionContentTypeProvider();
    }
}
