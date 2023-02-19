using Sunny.UI;

namespace AnimeLover
{
    partial class Player: UIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player));
            this.topPanel = new Sunny.UI.UIPanel();
            this.vlcView = new LibVLCSharp.WinForms.VideoView();
            this.bottomPanel = new Sunny.UI.UIPanel();
            this.timeLabel = new Sunny.UI.UILabel();
            this.audioTracker = new Sunny.UI.UITrackBar();
            this.fullscreenButton = new Sunny.UI.UISymbolButton();
            this.prevButton = new Sunny.UI.UISymbolButton();
            this.nextButton = new Sunny.UI.UISymbolButton();
            this.timeTracker = new Sunny.UI.UITrackBar();
            this.playButton = new Sunny.UI.UISymbolButton();
            this.mainMenu = new Sunny.UI.UIContextMenuStrip();
            this.menuSubtitle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAudioTrack = new System.Windows.Forms.ToolStripMenuItem();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vlcView)).BeginInit();
            this.bottomPanel.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topPanel.Controls.Add(this.vlcView);
            this.topPanel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.topPanel.Location = new System.Drawing.Point(2, 37);
            this.topPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.topPanel.MinimumSize = new System.Drawing.Size(1, 1);
            this.topPanel.Name = "topPanel";
            this.topPanel.RectColor = System.Drawing.Color.Transparent;
            this.topPanel.Size = new System.Drawing.Size(1356, 761);
            this.topPanel.Style = Sunny.UI.UIStyle.Custom;
            this.topPanel.TabIndex = 0;
            this.topPanel.Text = null;
            this.topPanel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.topPanel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // vlcView
            // 
            this.vlcView.BackColor = System.Drawing.Color.Black;
            this.vlcView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vlcView.Location = new System.Drawing.Point(0, 0);
            this.vlcView.MediaPlayer = null;
            this.vlcView.Name = "vlcView";
            this.vlcView.Size = new System.Drawing.Size(1356, 761);
            this.vlcView.TabIndex = 0;
            this.vlcView.Text = "videoView1";
            // 
            // bottomPanel
            // 
            this.bottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomPanel.Controls.Add(this.timeLabel);
            this.bottomPanel.Controls.Add(this.audioTracker);
            this.bottomPanel.Controls.Add(this.fullscreenButton);
            this.bottomPanel.Controls.Add(this.prevButton);
            this.bottomPanel.Controls.Add(this.nextButton);
            this.bottomPanel.Controls.Add(this.timeTracker);
            this.bottomPanel.Controls.Add(this.playButton);
            this.bottomPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.bottomPanel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bottomPanel.Location = new System.Drawing.Point(2, 799);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bottomPanel.MinimumSize = new System.Drawing.Size(1, 1);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.RectColor = System.Drawing.Color.Transparent;
            this.bottomPanel.Size = new System.Drawing.Size(1356, 60);
            this.bottomPanel.Style = Sunny.UI.UIStyle.Custom;
            this.bottomPanel.TabIndex = 1;
            this.bottomPanel.Text = null;
            this.bottomPanel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bottomPanel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // timeLabel
            // 
            this.timeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.timeLabel.ForeColor = System.Drawing.Color.White;
            this.timeLabel.Location = new System.Drawing.Point(1186, 24);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(141, 23);
            this.timeLabel.Style = Sunny.UI.UIStyle.Custom;
            this.timeLabel.StyleCustomMode = true;
            this.timeLabel.TabIndex = 9;
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.timeLabel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // audioTracker
            // 
            this.audioTracker.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.audioTracker.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.audioTracker.Location = new System.Drawing.Point(88, 25);
            this.audioTracker.MinimumSize = new System.Drawing.Size(1, 1);
            this.audioTracker.Name = "audioTracker";
            this.audioTracker.Size = new System.Drawing.Size(90, 24);
            this.audioTracker.Style = Sunny.UI.UIStyle.Custom;
            this.audioTracker.StyleCustomMode = true;
            this.audioTracker.TabIndex = 8;
            this.audioTracker.Text = "uiTrackBar2";
            this.audioTracker.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.audioTracker.ValueChanged += new System.EventHandler(this.audioTracker_ValueChanged);
            // 
            // fullscreenButton
            // 
            this.fullscreenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fullscreenButton.FillColor = System.Drawing.Color.Transparent;
            this.fullscreenButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fullscreenButton.Location = new System.Drawing.Point(1329, 26);
            this.fullscreenButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.fullscreenButton.Name = "fullscreenButton";
            this.fullscreenButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.fullscreenButton.Size = new System.Drawing.Size(24, 24);
            this.fullscreenButton.Style = Sunny.UI.UIStyle.Custom;
            this.fullscreenButton.StyleCustomMode = true;
            this.fullscreenButton.Symbol = 61541;
            this.fullscreenButton.TabIndex = 7;
            this.fullscreenButton.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.fullscreenButton.Click += new System.EventHandler(this.fullscreenButton_Click);
            // 
            // prevButton
            // 
            this.prevButton.FillColor = System.Drawing.Color.Transparent;
            this.prevButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.prevButton.Location = new System.Drawing.Point(31, 27);
            this.prevButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.prevButton.Name = "prevButton";
            this.prevButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.prevButton.Size = new System.Drawing.Size(24, 24);
            this.prevButton.Style = Sunny.UI.UIStyle.Custom;
            this.prevButton.StyleCustomMode = true;
            this.prevButton.Symbol = 61513;
            this.prevButton.TabIndex = 6;
            this.prevButton.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.FillColor = System.Drawing.Color.Transparent;
            this.nextButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nextButton.Location = new System.Drawing.Point(60, 27);
            this.nextButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.nextButton.Name = "nextButton";
            this.nextButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.nextButton.Size = new System.Drawing.Size(24, 24);
            this.nextButton.Style = Sunny.UI.UIStyle.Custom;
            this.nextButton.StyleCustomMode = true;
            this.nextButton.Symbol = 61520;
            this.nextButton.TabIndex = 5;
            this.nextButton.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // timeTracker
            // 
            this.timeTracker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeTracker.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.timeTracker.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.timeTracker.Location = new System.Drawing.Point(0, 0);
            this.timeTracker.MinimumSize = new System.Drawing.Size(1, 1);
            this.timeTracker.Name = "timeTracker";
            this.timeTracker.Size = new System.Drawing.Size(1356, 24);
            this.timeTracker.Style = Sunny.UI.UIStyle.Custom;
            this.timeTracker.StyleCustomMode = true;
            this.timeTracker.TabIndex = 4;
            this.timeTracker.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.timeTracker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.timeTracker_MouseUp);
            // 
            // playButton
            // 
            this.playButton.FillColor = System.Drawing.Color.Transparent;
            this.playButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playButton.Location = new System.Drawing.Point(3, 27);
            this.playButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.playButton.Name = "playButton";
            this.playButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.playButton.Size = new System.Drawing.Size(24, 24);
            this.playButton.Style = Sunny.UI.UIStyle.Custom;
            this.playButton.StyleCustomMode = true;
            this.playButton.Symbol = 61515;
            this.playButton.TabIndex = 0;
            this.playButton.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.mainMenu.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mainMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSubtitle,
            this.menuAudioTrack});
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(113, 56);
            this.mainMenu.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // menuSubtitle
            // 
            this.menuSubtitle.Name = "menuSubtitle";
            this.menuSubtitle.Size = new System.Drawing.Size(112, 26);
            this.menuSubtitle.Text = "字幕";
            // 
            // menuAudioTrack
            // 
            this.menuAudioTrack.Name = "menuAudioTrack";
            this.menuAudioTrack.Size = new System.Drawing.Size(112, 26);
            this.menuAudioTrack.Text = "声轨";
            // 
            // Player
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ClientSize = new System.Drawing.Size(1360, 860);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Player";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TopMost = true;
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.HotKeyEventHandler += new Sunny.UI.HotKeyEventHandler(this.Player_HotKeyEventHandler);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Player_FormClosing);
            this.Load += new System.EventHandler(this.Player_Load);
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vlcView)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UIPanel topPanel;
        private UIPanel bottomPanel;
        private LibVLCSharp.WinForms.VideoView vlcView;
        private UITrackBar audioTracker;
        private UISymbolButton fullscreenButton;
        private UISymbolButton prevButton;
        private UISymbolButton nextButton;
        private UITrackBar timeTracker;
        private UISymbolButton playButton;
        private UILabel timeLabel;
        private UIContextMenuStrip mainMenu;
        private ToolStripMenuItem menuSubtitle;
        private ToolStripMenuItem menuAudioTrack;
    }
}