using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Event
{
    public class MouseControl
    {

        public delegate void MouseEventHandler(object sender, MouseEventArgs e);

        public static MouseEventHandler LayoutMouseDown;

        [JSInvokable("MouseDown")]
        public static void OnViewMouseDown(int button, int x, int y)
        {
            var simirator = new MouseEventArgs(Enum.Parse<MouseButtons>(button.ToString()), 1, x, y, 0);
            LayoutMouseDown?.Invoke(null, simirator);
        }
    }
}
