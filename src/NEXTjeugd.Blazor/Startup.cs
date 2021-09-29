using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NEXTjeugd.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<NEXTjeugdBlazorModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.InitializeApplication();
        }
    }
}
