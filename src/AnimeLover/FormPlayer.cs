using AnimeLover.Busi;
using AnimeLover.Event;
using AnimeLover.Model;
using LibVLCSharp.Shared;
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
    public partial class FormPlayer : Form
    {
        private LibVLC libvlc;

        private Media media;

        public Form Opener { get; set; }

        private Anime anime;

        private AnimeVideo video;

        public FormPlayer(Anime anime, AnimeVideo video)
        {
            this.video = video;
            this.anime = anime;
            InitializeComponent();
        }

        private void FormPlayer_Load(object sender, EventArgs e)
        {
            //标题
            Text = $"{anime.Name} 第{video.Episode}集";

            Core.Initialize();
            libvlc = new LibVLC();

            media = new Media(libvlc, new Uri(video.PhysicalPath));

            videoView1.MediaPlayer = new MediaPlayer(media);
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
            videoView1.MediaPlayer.EndReached += MediaPlayer_EndReached;

            videoView1.MediaPlayer.Play();

            if (video.LastPlayTime != null)
            {
                videoView1.MediaPlayer?.Pause();
                videoView1.MediaPlayer.SeekTo(video.LastPlayTime.Value);
                videoView1.MediaPlayer.Play();
            }

            anime.LastPlayVideo = video.Id;
            BlazorApp.Database.Updateable(anime).ExecuteCommand();
        }

        private void MediaPlayer_EndReached(object sender, EventArgs e)
        {
            Invoke(() =>
            {
                var total = videoView1.MediaPlayer?.Media?.Duration ?? 0;
                var tsAll = TimeSpan.FromMilliseconds(total);
                var tsCurrent = TimeSpan.FromMilliseconds(total);
                var text = tsCurrent.ToString(@"hh\:mm\:ss") + "/" + tsAll.ToString(@"hh\:mm\:ss");

                uiLabel1.Text = text;

                videoTimeTracker.Value = (int)Math.Floor((double)(total * 100 / total));
            });
        }

        private void OnTimeChange(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            Invoke(() =>
            {
                var total = videoView1.MediaPlayer?.Media?.Duration ?? 0;
                var tsAll = TimeSpan.FromMilliseconds(total);
                var tsCurrent = TimeSpan.FromMilliseconds(e.Time);
                var text = tsCurrent.ToString(@"hh\:mm\:ss") + "/" + tsAll.ToString(@"hh\:mm\:ss");

                uiLabel1.Text = text;

                videoTimeTracker.Value = (int)Math.Floor((double)(e.Time * 100 / total));
            });
        }



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

        private void ExitFullScreen()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
            videoView1.Parent = splitContainer1.Panel1;
            videoView1.SendToBack();

            if (null != videoView1.MediaPlayer)
                videoView1.MediaPlayer.Fullscreen = false;
        }

        private void VolumeTracker_ValueChanged(object sender, EventArgs e)
        {
            if (null != videoView1.MediaPlayer)
                videoView1.MediaPlayer.Volume = volumeTracker.Value;
        }

        /// <summary>
        /// 鼠标拖动的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoTimeTracker_MouseUp(object sender, MouseEventArgs e)
        {
            var player = videoView1.MediaPlayer;
            var seconds = Math.Round((double)videoTimeTracker.Value / 100 * (player?.Media?.Duration ?? 0));
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

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            Invoke(() =>
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

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            Invoke(() =>
            {
                videoView1.MediaPlayer.SeekTo(TimeSpan.FromSeconds(0));
                videoView1.MediaPlayer.Stop();
            });
        }

        private void ButtonMoveForward_Click(object sender, EventArgs e)
        {
            Invoke(() =>
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

        private void ButtonMoveNext_Click(object sender, EventArgs e)
        {
            Invoke(() =>
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

        private void FormPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoView1.MediaPlayer?.Media?.State == VLCState.Playing)
            {
                var time = TimeSpan.FromMilliseconds(videoView1.MediaPlayer.Time);
                video.LastPlayTime = time;
                BlazorApp.Database.Updateable(video).ExecuteCommand();

                Invoke(() =>
                {
                    videoView1.MediaPlayer.Stop();
                });
            }

            //释放
            videoView1.MediaPlayer?.Dispose();

            media?.Dispose();

            libvlc?.Dispose();

            Opener?.Show();
        }
    }
}
