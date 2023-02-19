using AnimeLover.Busi;
using AnimeLover.Model;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimeLover
{
    public partial class Player : UIForm
    {
        #region 构造函数与参数
        /// <summary>
        /// vlcView库
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
        public Player(Anime anime, AnimeVideo video)
        {
            this.video = video;
            this.anime = anime;
            libvlc = new LibVLC();
            InitializeComponent();
        }
        #endregion

        #region event
        private void Player_Load(object sender, EventArgs e)
        {
            (this.Width, this.Height) = Tool.GetFitRect(this);
            InitPlayer();
            PlayWithRecord();
            this.ShowFullScreen = true;
            this.RegisterHotKey(Sunny.UI.ModifierKeys.Control, Keys.P);
        }

        protected override void DoEnter()
        {
            if (!vlcView.MediaPlayer.Fullscreen)
            {
                FullScreen();
            }
            else
            {
                ExitFullScreen();
            }
        }
        protected override void DoEsc()
        {
            ExitFullScreen();

        }

        private void Player_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetPlayTime();

            var result = BeginInvoke(() =>
            {
                vlcView.MediaPlayer.Pause();
            });

            EndInvoke(result);

            //释放
            vlcView.MediaPlayer?.Dispose();

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
            if (!Disposing)
            {
                BeginInvoke(() =>
                {
                    if (vlcView.MediaPlayer.IsPlaying && playButton.Symbol != 61516)
                    {
                        playButton.Symbol = 61516;
                    }

                    var total = vlcView.MediaPlayer?.Media?.Duration ?? 0;
                    var tsAll = TimeSpan.FromMilliseconds(total);
                    var tsCurrent = TimeSpan.FromMilliseconds(e.Time);
                    var text = tsCurrent.ToString(@"hh\:mm\:ss") + "/" + tsAll.ToString(@"hh\:mm\:ss");

                    timeLabel.Text = text;
                    timeTracker.Value = (int)Math.Round((double)(e.Time * 100 / total));

                });
            }
        }

        /// <summary>
        /// 改变播放进度条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeTracker_MouseUp(object sender, MouseEventArgs e)
        {
            var player = vlcView.MediaPlayer;
            var seconds = Math.Floor((double)timeTracker.Value / 100 * (player?.Media?.Duration ?? 0));
            var ts = TimeSpan.FromMilliseconds(seconds);
            player?.Pause();
            player?.SeekTo(ts);
            player?.Play();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            BeginInvoke(() =>
            {
                if (vlcView.MediaPlayer.IsPlaying)
                {
                    vlcView.MediaPlayer.Pause();
                }
                else
                {
                    vlcView.MediaPlayer.Play();
                }
            });
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            Jump(-1);
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            Jump(1);
        }

        private void audioTracker_ValueChanged(object sender, EventArgs e)
        {
            if (null != vlcView.MediaPlayer)
                vlcView.MediaPlayer.Volume = audioTracker.Value;
        }

        private void fullscreenButton_Click(object sender, EventArgs e)
        {
            if (!vlcView.MediaPlayer.Fullscreen)
            {
                FullScreen();
            }
            else
            {
                ExitFullScreen();
            }
        }

        #endregion event

        #region func
        private void InitPlayer()
        {
            media = new Media(libvlc, new Uri(video.PhysicalPath));

            vlcView.MediaPlayer = new MediaPlayer(libvlc)
            {
                EnableKeyInput = false,
                EnableMouseInput = false,
                Media = media
            };
            vlcView.ContextMenuStrip = mainMenu;
            audioTracker.Value = vlcView.MediaPlayer.Volume;
            vlcView.MediaPlayer.TimeChanged += OnTimeChange;
            vlcView.MediaPlayer.Paused += (sender, e) =>
            {
                playButton.Symbol = 61515;
            };
            vlcView.MediaPlayer.Playing += (sender, e) =>
            {
                playButton.Symbol = 61516;
            };

            vlcView.MediaPlayer.EndReached += (sender, e) =>
            {
                Stop();
            };

        }


        /// <summary>
        /// 安全地停止播放
        /// </summary>
        private void Stop()
        {
            BeginInvoke(() =>
            {
                playButton.Symbol = 61515;
                timeTracker.Value = 0;
            });

            ThreadPool.QueueUserWorkItem(StopProc);
        }

        private void StopProc(Object stateInfo)
        {
            vlcView.MediaPlayer.Stop();
        }

        /// <summary>
        /// 播放并记录
        /// </summary>
        private void PlayWithRecord()
        {
            //标题
            Text = $"{anime.Name} 第{video.Episode}集";

            vlcView.MediaPlayer.Play();

            //小于才跳转
            if (video.LastPlayTime != null)
            {
                vlcView.MediaPlayer.Pause();
                vlcView.MediaPlayer.SeekTo(video.LastPlayTime.Value);
                vlcView.MediaPlayer.Play();
            }
            else
            {
                vlcView.MediaPlayer.Play();
            }

            BindCombox();

            anime.LastPlayVideo = video.Id;
            BlazorApp.Database.Updateable(anime).ExecuteCommand();
        }

        private void BindCombox()
        {
            media.Parse().Wait();
            vlcView.MediaPlayer.Pause();
            var sounds = media.Tracks.Where(m => m.TrackType == TrackType.Audio && !string.IsNullOrEmpty(m.Description)).Select(m => new TrackInfo { Id = m.Id, Name = m.Description }).ToList();
            menuAudioTrack.DropDownItemClicked -= mainMenu_ItemClicked;
            menuAudioTrack.DropDownItems.Clear();
            if (sounds.Count > 0)
            {
                foreach(var sound in sounds)
                {
                    menuAudioTrack.DropDownItems.Add(new ToolStripMenuItem
                    {
                        Text = sound.Name,
                        Tag = "sound-" + sound.Id,
                        Checked = vlcView.MediaPlayer.AudioTrack == sound.Id
                    });
                }
                menuAudioTrack.DropDownItemClicked += mainMenu_ItemClicked;
            }

            var subtitles = media.Tracks.Where(m => m.TrackType == TrackType.Text && !string.IsNullOrEmpty(m.Description)).Select(m => new TrackInfo { Id = m.Id, Name = m.Description }).ToList();
            menuSubtitle.DropDownItemClicked -= mainMenu_ItemClicked;
            menuSubtitle.DropDownItems.Clear();
            if (subtitles.Count > 0)
            {
                foreach (var subtitle in subtitles)
                {
                    menuSubtitle.DropDownItems.Add(new ToolStripMenuItem
                    {
                        Text = subtitle.Name,
                        Tag = "sub-" + subtitle.Id,
                        Checked = vlcView.MediaPlayer.Spu == subtitle.Id
                    });
                }

                menuSubtitle.DropDownItemClicked += mainMenu_ItemClicked;
            }
        }

        /// <summary>
        /// 记录播放时间
        /// </summary>
        private void SetPlayTime()
        {
            if (vlcView.MediaPlayer.Time > 0)
            {
                var time = TimeSpan.FromMilliseconds(vlcView.MediaPlayer.Time);

                if (vlcView.MediaPlayer.Time == vlcView.MediaPlayer.Media?.Duration)
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
        /// 全屏
        /// </summary>
        private void FullScreen()
        {
            ShowTitle = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Rectangle ret = Screen.GetWorkingArea(this);
            size = vlcView.ClientSize;
            vlcView.ClientSize = new Size(ret.Width, ret.Height);
            vlcView.Parent = this;
            vlcView.Dock = DockStyle.Fill;
            vlcView.BringToFront();
            vlcView.Focus();
            if (null != vlcView.MediaPlayer)
                vlcView.MediaPlayer.Fullscreen = true;
        }

        Size size;
        /// <summary>
        /// 退出全屏
        /// </summary>
        private void ExitFullScreen()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
            vlcView.Parent = topPanel;
            vlcView.SendToBack();
            vlcView.ClientSize = size;
            ShowTitle = true;
            if (null != vlcView.MediaPlayer)
                vlcView.MediaPlayer.Fullscreen = false;
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

                    vlcView.MediaPlayer.Media = media;

                    old?.Dispose();

                    PlayWithRecord();
                }
            }
        }

        #endregion
        private void Player_HotKeyEventHandler(object sender, HotKeyEventArgs e)
        {
            if (null != vlcView.MediaPlayer)
            {
                //更新封面
                if (e.hotKey.Key == Keys.P && e.hotKey.ModifierKey == Sunny.UI.ModifierKeys.Control)
                {
                    if (WindowState == FormWindowState.Maximized)
                    {
                        if (vlcView.MediaPlayer?.Media?.State == VLCState.Playing)
                        {
                            vlcView.MediaPlayer.Pause();
                        }

                        if (vlcView.MediaPlayer?.Media?.State == VLCState.Paused)
                        {
                            var bitmap = new Bitmap(this.Width, this.Height);
                            var graph = Graphics.FromImage(bitmap);
                            graph.CopyFromScreen(0, 0, 0, 0, new Size(this.Width, this.Height));
                            Clipboard.SetImage(bitmap);
                            anime.Cover = Util.BitmapToBytes(bitmap);
                            anime.CoverName = "cover.jpg";
                            BlazorApp.Database.Updateable(anime).ExecuteCommand();
                            vlcView.MediaPlayer.Play();
                        }
                    }
                }
            }
        }

        private void mainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var tag = e.ClickedItem.Tag.ToString();
            if (tag.Contains("sound-"))
            {
                var value = Convert.ToInt32(tag.Replace("sound-", ""));
                if (value > 0)
                {
                    vlcView.MediaPlayer.SetAudioTrack(value);
                }
                
                foreach(ToolStripMenuItem item in menuAudioTrack.DropDownItems)
                {
                    if(item == e.ClickedItem)
                    {
                        item.Checked = true;
                    }
                    else
                    {
                        item.Checked = false;
                    }
                }
            }

            if (tag.Contains("sub-"))
            {
                var value = Convert.ToInt32(tag.Replace("sub-", ""));
                if (value > 0)
                {
                    vlcView.MediaPlayer.SetSpu(value);
                }

                foreach (ToolStripMenuItem item in menuSubtitle.DropDownItems)
                {
                    if (item == e.ClickedItem)
                    {
                        item.Checked = true;
                    }
                    else
                    {
                        item.Checked = false;
                    }
                }
            }
        }
    }
}
