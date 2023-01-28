using Blazorise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Event
{
    public class SysControl
    {

        public delegate void CornorDisplayChanged(bool display);

        public static event CornorDisplayChanged OnCornorDisplayChanged;

        public static void Display(bool display)
        {
            OnCornorDisplayChanged?.Invoke(display);
        }

        public delegate void FullScreenToggle(bool? isFull);

        public static event FullScreenToggle OnFullScreenToggle;

        public static void FullScreenChange(bool? isFull = null)
        {
            OnFullScreenToggle?.Invoke(isFull);
        }
    }
}
