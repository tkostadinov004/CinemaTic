using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Cinema.Areas.Identity.IdentityHostingStartup))]
namespace Cinema.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}