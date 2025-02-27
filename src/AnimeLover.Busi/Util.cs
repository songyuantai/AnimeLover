using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;


namespace AnimeLover.Busi
{
    public class Util
    {
        /// <summary>
        /// 获取当前Season
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentSeason()
        {
            var year = DateTime.Now.Year;
            return DateTime.Now.Month switch
            {
                1 or 2 or 3 => $"{year}-01",
                4 or 5 or 6 => $"{year}-04",
                7 or 8 or 9 => $"{year}-07",
                10 or 11 or 12 => $"{year}-10",
                _ => string.Empty,
            };
        }

        /// <summary>
        /// 获取图片地址
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetPictureUri(byte[] data)
        {
            if (null == data || data.Length == 0)
            {
                return "/images/no-image.gif";
            }
            else
            {
                return "data:image/*;base64," + Convert.ToBase64String(data);
            }
        }

        public static string GetSimpleName(string fullName)
        {
            return fullName?.Length > 24 ? string.Concat(fullName.AsSpan(0, 22), "....") : fullName;
        }

        [SupportedOSPlatform("windows")]
        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {

            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            finally
            {
                ms.Close();
            }

        }
    }
}
