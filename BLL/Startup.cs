using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class Startup
    {
        public static void RegisterBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IPackageService, PackageService>();
        }
    }
}
