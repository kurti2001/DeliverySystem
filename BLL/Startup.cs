using BLL.Services;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.Extensions.DependencyInjection;
using BLL.Singleton;

namespace BLL
{
    public static class Startup
    {
        public static void RegisterBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IPostalOfficeService, PostalOfficeService>();
            services.AddScoped<IGeneratePackageService, GeneratePackageService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IEmailsService, EmailsService>();

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddSingleton<ILoggerService, LoggerService>();

        }

    }
}