using AnimeLover.Busi;
using AnimeLover.Event;
using AnimeLover.Model;
using LibVLCSharp.Shared;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimeLover
{
    /// <summary>
    /// 动漫播放器
    /// </summary>
    public partial class FormPlayer : Form, IDisposable
    {
        #region 构造函数与参数
        /// <summary>
        /// vlc库
        /// </summary>
        private readonly LibVLC libvlc;

        /// <summary>
        /// 多媒体文件
        /// </summary>
        private Media media;

        /// <summary>
        /// 动漫信息
        /// </summary>
        private readonly Anime anime;

        /// <summary>
        /// 剧集信息
        /// </summary>
        private AnimeVideo video;

        /// <summary>
        /// main窗口
        /// </summary>
        public Form Opener { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="anime"></param>
        /// <param name="video"></param>
        public FormPlayer(Anime anime, AnimeVideo video)
        {
            this.video = video;
            this.anime = anime;
            libvlc = new LibVLC();
            InitializeComponent();
        }
        #endregion

        #region event

        /// <summary>
        /// 窗口初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPlayer_Load(object sender, EventArgs e)
        {
            (this.Width, this.Height) = Tool.GetFitRect(this);
            InitPlayer();
            PlayWithRecord();
        }

        /// <summary>
        /// 关闭窗口前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetPlayTime();

            var result = BeginInvoke(() =>
            {
                videoView1.MediaPlayer.Pause();
            });

            EndInvoke(result);

            //释放
            videoView1.MediaPlayer?.Dispose();

            media?.Dispose();

            libvlc?.Dispose();

            Opener?.Show();
        }

        /// <summary>
        /// 播放时间变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimeChange(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            if(!this.Disposing)
            {
                BeginInvoke(() =>
                {
                    if (videoView1.MediaPlayer.IsPlaying && btnPlay.Symbol != 61516)
                    {
                        btnPlay.Symbol = 61516;
                    }

                    var total = videoView1.MediaPlayer?.Media?.Duration ?? 0;
                    var tsAll = TimeSpan.FromMilliseconds(total);
                    var tsCurrent = TimeSpan.FromMilliseconds(e.Time);
                    var text = tsCurrent.ToString(@"hh\:mm\:ss") + "/" + tsAll.ToString(@"hh\:mm\:ss");

                    uiLabel1.Text = text;
                    videoTimeTracker.Value = (int)Math.Round((double)(e.Time * 100 / total));

                });
            }
        }

        /// <summary>
        /// 按下键盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPlayer_KeyDown(object sender, KeyEventArgs e)
        {
            if (null != videoView1.MediaPlayer)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!videoView1.MediaPlayer.Fullscreen)
                    {
                        FullScreen();
                    }
                    else
                    {
                        ExitFullScreen();
                    }
                }

                if (e.KeyCode == Keys.Escape)
                {
                    if (videoView1.MediaPlayer.Fullscreen)
                        ExitFullScreen();
                }

                //更新封面
                if(e.KeyCode == Keys.P && e.Control)
                {
                    if(WindowState == FormWindowState.Maximized)
                    {
                        if (videoView1.MediaPlayer?.Media?.State == VLCState.Playing)
                        {
                            videoView1.MediaPlayer.Pause();
                        }

                        if (videoView1.MediaPlayer?.Media?.State == VLCState.Paused)
                        {
                            var bitmap = new Bitmap(this.Width, this.Height);
                            var graph = Graphics.FromImage(bitmap);
                            graph.CopyFromScreen(0, 0, 0, 0, new Size(this.Width, this.Height));
                            Clipboard.SetImage(bitmap);
                            anime.Cover = Util.BitmapToBytes(bitmap);
                            anime.CoverName = "cover.jpg";
                            BlazorApp.Database.Updateable(anime).ExecuteCommand();
                            videoView1.MediaPlayer.Play();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 改变音量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VolumeTracker_ValueChanged(object sender, EventArgs e)
        {
            if (null != videoView1.MediaPlayer)
                videoView1.MediaPlayer.Volume = volumeTracker.Value;
        }

        /// <summary>
        /// 改变播放进度条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoTimeTracker_MouseUp(object sender, MouseEventArgs e)
        {
            var player = videoView1.MediaPlayer;
            var seconds = Math.Floor((double)videoTimeTracker.Value / 100 * (player?.Media?.Duration ?? 0));
            var ts = TimeSpan.FromMilliseconds(seconds);
            player?.Pause();
            player?.SeekTo(ts);
            player?.Play();
        }

        /// <summary>
        /// 全屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            if (!videoView1.MediaPlayer.Fullscreen)
            {
                FullScreen();
            }
            else
            {
                ExitFullScreen();
            }
        }

        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            BeginInvoke(() =>
            {
                if (videoView1.MediaPlayer.IsPlaying)
                {
                    videoView1.MediaPlayer.Pause();
                }
                else
                {
                    videoView1.MediaPlayer.Play();
                }
            });
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// 快退（弃用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMoveForward_Click(object sender, EventArgs e)
        {
            BeginInvoke(() =>
            {
                if (null != videoView1.MediaPlayer)
                {
                    if (videoView1.MediaPlayer.Time >= 2000)
                    {
                        var seconds = videoView1.MediaPlayer.Time - 20000;
                        var ts = TimeSpan.FromMilliseconds(seconds);
                        videoView1.MediaPlayer?.Pause();
                        videoView1.MediaPlayer?.SeekTo(ts);
                        videoView1.MediaPlayer?.Play();
                    }
                }
            });

        }

        /// <summary>
        /// 快进（弃用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMoveNext_Click(object sender, EventArgs e)
        {
            BeginInvoke(() =>
            {
                if (null != videoView1.MediaPlayer)
                {
                    var total = videoView1.MediaPlayer?.Media?.Duration ?? 0;
                    var now = videoView1.MediaPlayer?.Time ?? 0;
                    if (total - now >= 2000)
                    {
                        var seconds = now + 20000;
                        var ts = TimeSpan.FromMilliseconds(seconds);
                        videoView1.MediaPlayer?.Pause();
                        videoView1.MediaPlayer?.SeekTo(ts);
                        videoView1.MediaPlayer?.Play();
                    }
                }
            });
        }

        /// <summary>
        /// 上一集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Prev_Button_Click(object sender, EventArgs e)
        {
            Jump(-1);
        }

        /// <summary>
        /// 下一集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_Button_Click(object sender, EventArgs e)
        {
            Jump(1);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化播放器
        /// </summary>
        private void InitPlayer()
        {
            media = new Media(libvlc, new Uri(video.PhysicalPath));

            videoView1.MediaPlayer = new MediaPlayer(libvlc);
            videoView1.MediaPlayer.Media = media;
            volumeTracker.Value = videoView1.MediaPlayer.Volume;
            videoView1.MediaPlayer.TimeChanged += OnTimeChange;
            videoView1.MediaPlayer.Paused += (sender, e) =>
            {
                btnPlay.Symbol = 61515;
            };
            videoView1.MediaPlayer.Playing += (sender, e) =>
            {
                btnPlay.Symbol = 61516;
            };

            videoView1.MediaPlayer.EndReached += (sender, e) =>
            {
                Stop();
            };
            
        }

        private void BindCombox()
        {
            media.Parse().Wait();

            var sounds = media.Tracks.Where(m => m.TrackType == TrackType.Audio).Select(m => new TrackInfo { Id = m.Id, Name = m.Description}).ToList();
            selSoudtrack.Visible = sounds.Count > 1;
            selSoudtrack.Clear();
            selSoudtrack.DataSource = sounds;
            selSoudtrack.SelectedValue = videoView1.MediaPlayer.AudioTrack;

            var subtitles = media.Tracks.Where(m => m.TrackType == TrackType.Text).Select(m => new TrackInfo { Id = m.Id, Name = m.Description }).ToList();
            selSubtitle.Visible = subtitles.Count > 1;
            selSubtitle.Clear();
            selSubtitle.DataSource = subtitles;
            selSubtitle.SelectedValue = videoView1.MediaPlayer.Spu;
            var chineseSubtitle = media.Tracks.FirstOrDefault(m => m.TrackType == TrackType.Text && m.Description == "Chinese (Simplified)");
            if (chineseSubtitle.Id > 0)
            {
                videoView1.MediaPlayer.SetSpu(chineseSubtitle.Id);
                selSubtitle.SelectedValue = chineseSubtitle.Id;
            }

        }

        /// <summary>
        /// 播放并记录
        /// </summary>
        private void PlayWithRecord()
        {
            //标题
            Text = $"{anime.Name} 第{video.Episode}集";

            videoView1.MediaPlayer.Play();

            //小于才跳转
            if (video.LastPlayTime != null)
            {
                videoView1.MediaPlayer.Pause();
                videoView1.MediaPlayer.SeekTo(video.LastPlayTime.Value);
                videoView1.MediaPlayer.Play();
            }
            else
            {
                videoView1.MediaPlayer.Play();
            }

            //btnPlay.Symbol = 61516;

            BindCombox();

            anime.LastPlayVideo = video.Id;
            BlazorApp.Database.Updateable(anime).ExecuteCommand();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="offset"></param>
        private void Jump(int offset)
        {

            var episodeNum = Convert.ToInt32(video.Episode) + offset;
            if (episodeNum > 0)
            {
                var episode = episodeNum.ToString().PadLeft(2, '0');

                var nextVideo = BlazorApp.Database.Queryable<AnimeVideo>().First(m => m.Episode == episode && m.AnimeId == anime.Id);

                if (!string.IsNullOrEmpty(nextVideo?.PhysicalPath))
                {
                    Stop();

                    SetPlayTime();

                    var old = media;

                    video = nextVideo;

                    media = new Media(libvlc, new Uri(video.PhysicalPath));

                    videoView1.MediaPlayer.Media = media;

                    old?.Dispose();

                    PlayWithRecord();
                }
            }
        }

        /// <summary>
        /// 全屏
        /// </summary>
        private void FullScreen()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Rectangle ret = Screen.GetWorkingArea(this);

            videoView1.ClientSize = new Size(ret.Width, ret.Height);
            videoView1.Parent = this;
            videoView1.Dock = DockStyle.Fill;
            videoView1.BringToFront();

            if (null != videoView1.MediaPlayer)
                videoView1.MediaPlayer.Fullscreen = true;
        }

        /// <summary>
        /// 退出全屏
        /// </summary>
        private void ExitFullScreen()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
            videoView1.Parent = splitContainer1.Panel1;
            videoView1.SendToBack();

            if (null != videoView1.MediaPlayer)
                videoView1.MediaPlayer.Fullscreen = false;
        }

        /// <summary>
        /// 记录播放时间
        /// </summary>
        private void SetPlayTime()
        {
            if(videoView1.MediaPlayer.Time > 0)
            {
                var time = TimeSpan.FromMilliseconds(videoView1.MediaPlayer.Time);

                if(videoView1.MediaPlayer.Time == videoView1.MediaPlayer.Media?.Duration)
                {
                    video.LastPlayTime = null;
                }
                else
                {
                    video.LastPlayTime = time;
                }
                BlazorApp.Database.Updateable(video).ExecuteCommand();
            }
        }

        /// <summary>
        /// 安全地停止播放
        /// </summary>
        private void Stop()
        {
            BeginInvoke(() =>
            {
                btnPlay.Symbol = 61515;
                videoTimeTracker.Value = 0;
            });

            ThreadPool.QueueUserWorkItem(StopProc);
        }

        private void StopProc(Object stateInfo) 
        {
            videoView1.MediaPlayer.Stop();
        }

        #endregion

        private void selSoudtrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = Convert.ToInt32(selSoudtrack.SelectedValue);
            if(value > 0)
            {
                videoView1.MediaPlayer.SetAudioTrack(value);
            }
        }

        private void selSubtitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = Convert.ToInt32(selSubtitle.SelectedValue);
            if(value > 0)
            {
                videoView1.MediaPlayer.SetSpu(value);
            }
        }
    }

    public class TrackInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
