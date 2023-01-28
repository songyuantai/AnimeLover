using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using AnimeLover.Event;
using AnimeLover.Page;
using AnimeLover.Busi;
using AnimeLover.Model;
using LibVLCSharp.Shared;
using System.Runtime.InteropServices;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace AnimeLover
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        public FormMain()
        {
            
            InitializeComponent();
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddBlazorWebViewDeveloperTools();
            services.AddBlazorise(options =>
                     {
                         options.Immediate = true;
                     })
                    .AddBootstrapProviders()
                    .AddFontAwesomeIcons();

            webview.HostPage = "wwwroot\\index.html";
            webview.RootComponents.Add<App>("#app");

            webview.Services = services.BuildServiceProvider();

            webview.UrlLoading += (sender, urlLoadingEventArgs) =>
            {
                if (urlLoadingEventArgs.Url.Host != "0.0.0.0")
                {
                    urlLoadingEventArgs.UrlLoadingStrategy = UrlLoadingStrategy.OpenInWebView;
                }
            };

            PlayerControl.BindPlay(OpenPlayer);

            DbManager.Merge();
            var lib = Path.Combine(AppContext.BaseDirectory, "vlclib");
            Core.Initialize(lib);

            MouseControl.LayoutMouseDown += C_MouseDown;

            (this.Width, this.Height) = Tool.GetFitRect(this);

            SelftHostHelper.Start();

            SysControl.OnFullScreenToggle += Toggle;
        }

        private System.Windows.Forms.Form player;
        private void OpenPlayer(Anime anime, AnimeVideo video)
        {
            player = new FormPlayer(anime, video)
            {
                Opener = this
            };
            Hide();
            player.Show();
        }

        private void Toggle()
        {
            if(this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        private void C_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ReleaseCapture();
            //SendMessage(Handle, 274, 61440 + 9, 0);
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void webview_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new(System.Drawing.Color.FromArgb(200, 0, 121, 107));
            e.Graphics.DrawRectangle(p, 0f, 0f, this.Width - 0.5f, this.Height - 0.5f);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        public static void OpenPath(string path)
        {
            var processStartInfo = new ProcessStartInfo(path);
            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }
    }
}