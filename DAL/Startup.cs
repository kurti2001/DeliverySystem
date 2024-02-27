using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace DAL
{
    public static class Startup
    {
        public static void RegisterDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DeliverySystemContext>(options => options.UseSqlServer(configuration.GetConnectionString("DeliverySystemConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddDefaultUI()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<DeliverySystemContext>();
        }

    }
}
