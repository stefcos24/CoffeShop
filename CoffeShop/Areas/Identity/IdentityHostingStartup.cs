using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CoffeShop.Areas.Identity.IdentityHostingStartup))]
namespace CoffeShop.Areas.Identity
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