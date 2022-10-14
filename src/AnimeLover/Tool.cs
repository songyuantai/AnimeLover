using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover
{
    public class Tool
    {
        public static (int, int) GetFitRect(Form f)
        {
            var width = f.Width;
            var height = f.Height;
            if (Screen.AllScreens[0].WorkingArea.Height < f.Height)
            {
                height = Screen.AllScreens[0].WorkingArea.Height - 30;
                width = (int)Math.Floor((double)f.Width / f.Height * height);
            }

            return (width, height);
        }
    }
}
