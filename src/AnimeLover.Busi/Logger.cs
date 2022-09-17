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
                    //设置读写锁为写入模式独占资源，其他写入请求需要等待本次写入结束之后才能继续写入
                    //注意：长时间持有读线程锁或写线程锁会使其他线程发生饥饿 (starve)。 为了得到最好的性能，需要考虑重新构造应用程序以将写访问的持续时间减少到最小。
                    //      从性能方面考虑，请求进入写入模式应该紧跟文件操作之前，在此处进入写入模式仅是为了降低代码复杂度
                    //      因进入与退出写入模式应在同一个try finally语句块内，所以在请求进入写入模式之前不能触发异常，否则释放次数大于请求次数将会触发异常
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
                    //退出写入模式，释放资源占用
                    //注意：一次请求对应一次释放
                    //      若释放次数大于请求次数将会触发异常[写入锁定未经保持即被释放]
                    //      若请求处理完成后未释放将会触发异常[此模式不下允许以递归方式获取写入锁定]
                    LogWriteLock.ExitWriteLock();
                }
            }
        }

        private static void Log(string type, string tag, string content)
        {
            WriteLogs("logs", type, tag, content);
        }

        public static void d(string tag, string content)
        {
            Log("Debug", tag, content);
        }

        public static void i(string tag, string content)
        {
            Log("Info", tag, content);
        }

        public static void w(string tag, string content)
        {
            Log("Warn", tag, content);
        }

        public static void e(string tag, string content)
        {
            Log("Error", tag, content);
        }

        public static void f(string tag, string content)
        {
            Log("Fatal", tag, content);
        }
    }
}
