using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Busi
{
    public static class FsTool
    {
        public static string Path { get; set; } = BlazorApp.Config.LatestLocation;
        public static string ToMD5(this string txt)
        {
            using MD5 mi = MD5.Create();
            byte[] buffer = Encoding.Default.GetBytes(txt);
            //开始加密
            byte[] newBuffer = mi.ComputeHash(buffer);
            StringBuilder sb = new ();
            for (int i = 0; i < newBuffer.Length; i++)
            {
                sb.Append(newBuffer[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static List<FsItem> GetFsItems()
        {
            //处理默认路径
            if (string.IsNullOrEmpty(Path))
            {
                Path = "C:\\";
            }

            var list = new List<FsItem>();

            try
            {
                if (System.IO.Directory.Exists(Path))
                {
                    var rootDir = System.IO.Directory.GetParent(Path);
                    if (rootDir != null)
                    {
                        var name = System.IO.Path.GetRelativePath(Path, rootDir.FullName);
                        var item = new FsItem
                        {
                            Guid = rootDir.FullName.ToMD5(),
                            Name = name,
                            Path = rootDir.FullName,
                            IsFile = false
                        };
                        list.Add(item);
                    }

                    //目录
                    var dirs = System.IO.Directory.GetDirectories(Path);
                    foreach (var fp in dirs)
                    {
                        var name = System.IO.Path.GetRelativePath(Path, fp);
                        var item = new FsItem
                        {
                            Guid = fp.ToMD5(),
                            Name = name,
                            Path = fp,
                            IsFile = false
                        };
                        list.Add(item);
                    }

                    //文件
                    var files = System.IO.Directory.GetFiles(Path);
                    foreach (var fp in files)
                    {
                        var name = System.IO.Path.GetFileName(fp);
                        var extension = System.IO.Path.GetExtension(fp);

                        var item = new FsItem
                        {
                            Guid = fp.ToMD5(),
                            Name = name,
                            Path = fp,
                            IsFile = true
                        };
                        list.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.e("", ex.Message);
            }
            return list;
        }
    }
}
