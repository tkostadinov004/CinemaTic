using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CinemaTic.Web.Areas.Identity.IdentityHostingStartup))]
namespace CinemaTic.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}