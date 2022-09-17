using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Event
{
    public static class ScrollControl
    {

        public delegate void Scroll(int offset);

        public static event Scroll ScrollEvent;

        [JSInvokable("Scroll")]
        public static async Task Onscroll(int scrollTop)
        {
            ScrollEvent(scrollTop);
            await Task.CompletedTask;
        }
    }
}
