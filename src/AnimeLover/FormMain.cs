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

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        
    }
}