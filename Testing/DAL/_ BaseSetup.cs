using DAL;
using BLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Testing.DAL;
public class BaseTest
{
    protected IUnitOfWork _unitOfWork;
    protected IServiceProvider _serviceProvider;

    public BaseTest()
    {
        var myConfiguration = new Dictionary<string, string>
        { 
            { "ConnectionStrings:DeliverySystemConnectionString", "Data Source=WINDOWS-OJQVH83;Initial Catalog=DeliverySystem;Integrated Security=True"},
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();
        var services = new ServiceCollection();
        services.RegisterDALServices(configuration);
        _serviceProvider = services.BuildServiceProvider();
        _unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
    }
}
