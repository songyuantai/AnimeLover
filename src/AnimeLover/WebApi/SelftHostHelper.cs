using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AnimeLover
{
    public static class SelftHostHelper
    {

        private static IHost _Host;
        public static void Start()
        {
            _Host = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webB =>
            {
                webB.UseKestrel();
                webB.ConfigureKestrel(ii => ii.ListenAnyIP(9900));
                webB.ConfigureServices(ii =>
                {
                    ii.AddControllers();
                });
                webB.Configure(app =>
                {
                    app.UseRouting();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });

            }).Build();
            _Host.Start();

        }

        public static Task StopAsync()
        {
            return _Host.StopAsync();
        }



    }
}