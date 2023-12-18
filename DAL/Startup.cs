using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DAL.Repositories;

namespace DAL
{
    public static class Startup
    {
        public static void RegisterDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DeliverySystemContext>(options => options.UseSqlServer(configuration.GetConnectionString("DeliverySystemConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
