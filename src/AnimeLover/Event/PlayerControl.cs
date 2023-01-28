using AnimeLover.Model;

namespace AnimeLover.Event
{
    public static class PlayerControl
    {

        public delegate void PlayHandler(Anime anime, AnimeVideo video);

        public static event PlayHandler PlayEvent;


        /// <summary>
        /// 播放视频
        /// </summary>
        public static void PlayVideo(Anime anime, AnimeVideo video)
        {
            PlayEvent(anime, video);
        }

        /// <summary>
        /// 绑定播放事件
        /// </summary>
        /// <param name="handler"></param>
        public static void BindPlay(PlayHandler handler)
        {
            PlayEvent += handler;
        }



    }
}
