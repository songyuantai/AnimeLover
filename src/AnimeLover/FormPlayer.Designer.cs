﻿namespace AnimeLover
{
    partial class FormPlayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPlayer));
            this.videoView1 = new LibVLCSharp.WinForms.VideoView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.volumeTracker = new Sunny.UI.UITrackBar();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.btnFullScreen = new Sunny.UI.UISymbolButton();
            this.btnStop = new Sunny.UI.UISymbolButton();
            this.btnMoveNext = new Sunny.UI.UISymbolButton();
            this.btnMoveForward = new Sunny.UI.UISymbolButton();
            this.btnPlay = new Sunny.UI.UISymbolButton();
            this.videoTimeTracker = new Sunny.UI.UITrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // videoView1
            // 
            this.videoView1.BackColor = System.Drawing.Color.Black;
            this.videoView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoView1.Location = new System.Drawing.Point(0, 0);
            this.videoView1.MediaPlayer = null;
            this.videoView1.Name = "videoView1";
            this.videoView1.Size = new System.Drawing.Size(1344, 747);
            this.videoView1.TabIndex = 0;
            this.videoView1.Text = "videoView1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.videoView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.volumeTracker);
            this.splitContainer1.Panel2.Controls.Add(this.uiLabel1);
            this.splitContainer1.Panel2.Controls.Add(this.btnFullScreen);
            this.splitContainer1.Panel2.Controls.Add(this.btnStop);
            this.splitContainer1.Panel2.Controls.Add(this.btnMoveNext);
            this.splitContainer1.Panel2.Controls.Add(this.btnMoveForward);
            this.splitContainer1.Panel2.Controls.Add(this.btnPlay);
            this.splitContainer1.Panel2.Controls.Add(this.videoTimeTracker);
            this.splitContainer1.Size = new System.Drawing.Size(1344, 821);
            this.splitContainer1.SplitterDistance = 747;
            this.splitContainer1.TabIndex = 1;
            // 
            // volumeTracker
            // 
            this.volumeTracker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.volumeTracker.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.volumeTracker.Location = new System.Drawing.Point(759, 24);
            this.volumeTracker.MinimumSize = new System.Drawing.Size(1, 1);
            this.volumeTracker.Name = "volumeTracker";
            this.volumeTracker.Size = new System.Drawing.Size(102, 34);
            this.volumeTracker.TabIndex = 7;
            this.volumeTracker.Text = "uiTrackBar2";
            this.volumeTracker.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.volumeTracker.ValueChanged += new System.EventHandler(this.VolumeTracker_ValueChanged);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiLabel1.Location = new System.Drawing.Point(3, 24);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(151, 35);
            this.uiLabel1.TabIndex = 6;
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFullScreen.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnFullScreen.Location = new System.Drawing.Point(1284, 24);
            this.btnFullScreen.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(48, 34);
            this.btnFullScreen.Symbol = 61522;
            this.btnFullScreen.TabIndex = 5;
            this.btnFullScreen.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnFullScreen.Click += new System.EventHandler(this.btnFullScreen_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnStop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStop.Location = new System.Drawing.Point(534, 24);
            this.btnStop.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(48, 34);
            this.btnStop.Symbol = 61517;
            this.btnStop.TabIndex = 4;
            this.btnStop.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // btnMoveNext
            // 
            this.btnMoveNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnMoveNext.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMoveNext.Location = new System.Drawing.Point(696, 24);
            this.btnMoveNext.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnMoveNext.Name = "btnMoveNext";
            this.btnMoveNext.Size = new System.Drawing.Size(48, 34);
            this.btnMoveNext.Symbol = 61518;
            this.btnMoveNext.TabIndex = 3;
            this.btnMoveNext.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnMoveNext.Click += new System.EventHandler(this.ButtonMoveNext_Click);
            // 
            // btnMoveForward
            // 
            this.btnMoveForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnMoveForward.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMoveForward.Location = new System.Drawing.Point(588, 24);
            this.btnMoveForward.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnMoveForward.Name = "btnMoveForward";
            this.btnMoveForward.Size = new System.Drawing.Size(48, 34);
            this.btnMoveForward.Symbol = 61514;
            this.btnMoveForward.TabIndex = 2;
            this.btnMoveForward.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnMoveForward.Click += new System.EventHandler(this.ButtonMoveForward_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnPlay.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPlay.Location = new System.Drawing.Point(642, 24);
            this.btnPlay.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(48, 34);
            this.btnPlay.Symbol = 61515;
            this.btnPlay.TabIndex = 1;
            this.btnPlay.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnPlay.Click += new System.EventHandler(this.ButtonPlay_Click);
            // 
            // videoTimeTracker
            // 
            this.videoTimeTracker.Dock = System.Windows.Forms.DockStyle.Top;
            this.videoTimeTracker.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.videoTimeTracker.Location = new System.Drawing.Point(0, 0);
            this.videoTimeTracker.Margin = new System.Windows.Forms.Padding(0);
            this.videoTimeTracker.MinimumSize = new System.Drawing.Size(1, 1);
            this.videoTimeTracker.Name = "videoTimeTracker";
            this.videoTimeTracker.Radius = 3;
            this.videoTimeTracker.Size = new System.Drawing.Size(1344, 21);
            this.videoTimeTracker.TabIndex = 0;
            this.videoTimeTracker.Text = "uiTrackBar1";
            this.videoTimeTracker.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.videoTimeTracker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.videoTimeTracker_MouseUp);
            // 
            // FormPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1344, 821);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "播放器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPlayer_FormClosing);
            this.Load += new System.EventHandler(this.FormPlayer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormPlayer_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private LibVLCSharp.WinForms.VideoView videoView1;
        private SplitContainer splitContainer1;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UISymbolButton btnFullScreen;
        private Sunny.UI.UISymbolButton btnStop;
        private Sunny.UI.UISymbolButton btnMoveNext;
        private Sunny.UI.UISymbolButton btnMoveForward;
        private Sunny.UI.UISymbolButton btnPlay;
        private Sunny.UI.UITrackBar videoTimeTracker;
        private Sunny.UI.UITrackBar volumeTracker;
    }
}