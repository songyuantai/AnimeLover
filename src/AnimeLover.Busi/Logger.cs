using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Busi
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public static class Logger
    {
        #region 日志类型《可以用于根据不同类型书写不同日志项》
        public static int ASSERT = 7;
        public static int DEBUG = 3;
        public static int ERROR = 6;
        public static int INFO = 4;
        public static int VERBOSE = 2;
        public static int WARN = 5;
        #endregion

        static ReaderWriterLockSlim LogWriteLock = new ReaderWriterLockSlim();
        public static void WriteLogs(string dirName, string type, string tag, string content)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    LogWriteLock.EnterWriteLock();
                    path = AppDomain.CurrentDomain.BaseDirectory + dirName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                    if (!File.Exists(path))
                    {
                        FileStream fs = File.Create(path);
                        fs.Close();
                    }
                    if (File.Exists(path))
                    {
                        StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + (tag ?? "") + " : " + type + " --> " + content);
                        sw.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    LogWriteLock.ExitWriteLock();
                }
            }
        }

        private static void Log(string type, string tag, string content)
        {
            WriteLogs("logs", type, tag, content);
        }

        public static void Debug(string tag, string content)
        {
            Log("Debug", tag, content);
        }

        public static void Info(string tag, string content)
        {
            Log("Info", tag, content);
        }

        public static void Warning(string tag, string content)
        {
            Log("Warn", tag, content);
        }

        public static void Error(string tag, string content)
        {
            Log("Error", tag, content);
        }

        public static void Fatal(string tag, string content)
        {
            Log("Fatal", tag, content);
        }
    }
}
