using AnimeLover.Model;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLover.Event
{
    /// <summary>
    /// 系统事件
    /// </summary>
    public static class SysEvent
    {
        #region event
        /// <summary>
        /// 番剧播放
        /// </summary>
        public static event Action<Anime, AnimeVideo> AnimePlay;

        /// <summary>
        /// 右下角显示改变
        /// </summary>
        public static event Action<bool> CornorStateChange;

        /// <summary>
        /// 全屏切换
        /// </summary>
        public static event Action<bool?> FullScreenToggle;

        /// <summary>
        /// 滚动
        /// </summary>
        public static event Action<int> Scrolling;

        /// <summary>
        /// webview上鼠标按下
        /// </summary>
        public static event Action<object, MouseEventArgs> WebviewMouseDown;
        #endregion

        #region trigger
        /// <summary>
        /// 通知鼠标移动
        /// </summary>
        /// <param name="button"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [JSInvokable("MouseDown")]
        public static void NotifyViewMouseDown(int button, int x, int y)
        {
            var simirator = new MouseEventArgs(Enum.Parse<MouseButtons>(button.ToString()), 1, x, y, 0);
            WebviewMouseDown?.Invoke(null, simirator);
        }

        /// <summary>
        /// 通知正在滚动滚动条
        /// </summary>
        /// <param name="scrollTop"></param>
        /// <returns></returns>
        [JSInvokable("Scroll")]
        public static async Task Scroll(int scrollTop)
        {
            Scrolling?.Invoke(scrollTop);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 通知右下角显示状态已经改变
        /// </summary>
        /// <param name="display"></param>
        public static void NotifyCornorStateChanged(bool display)
        {
            CornorStateChange?.Invoke(display);
        }

        /// <summary>
        /// 通知全屏状态变了
        /// </summary>
        /// <param name="isFull"></param>
        public static void NotifyFullScreenToggle(bool? isFull = null)
        {
            FullScreenToggle?.Invoke(isFull);
        }

        /// <summary>
        /// 通知番剧播放
        /// </summary>
        /// <param name="anime"></param>
        /// <param name="video"></param>
        public static void NotifyAnimePlay(Anime anime, AnimeVideo video)
        {
            AnimePlay?.Invoke(anime, video);
        }
        #endregion
    }
}
