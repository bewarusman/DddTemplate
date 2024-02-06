using Application.Common.Services;
using Infrastructure.Common.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrstructure(this IServiceCollection services, IConfiguration configuration, string contentRootPath)
    {
        Console.WriteLine(contentRootPath);
        AddSettings(services, configuration);
        services.AddConventionalServices(Assembly.GetExecutingAssembly());
        return services;
    }

    private static IServiceCollection AddSettings(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStrings>(options =>
        {
            options.SecurityPortal = configuration["ConnectionStrings:PosPortal"];
        });
        services.AddSingleton<IConnectionStrings>(sp => sp.GetRequiredService<IOptions<ConnectionStrings>>().Value);
        return services;
    }

    private static IServiceCollection AddConventionalServices(this IServiceCollection services, Assembly assembly)
    {
        var serviceInterfaceType = typeof(IService);
        var singletonServiceInterfaceType = typeof(ISingletonService);
        var scopedServiceInterfaceType = typeof(IScopedService);

        var types = assembly
            .GetExportedTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t =>
            {
                return new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                };
            })
            //
            .Where(t => t.Service != null)
            .OrderBy(t => t.Service.Name)
            .ToList();

        foreach (var type in types)
        {
            if (serviceInterfaceType.IsAssignableFrom(type.Service))
            {
                services.AddTransient(type.Service, type.Implementation);
            }
            else if (singletonServiceInterfaceType.IsAssignableFrom(type.Service))
            {
                services.AddSingleton(type.Service, type.Implementation);
            }
            else if (scopedServiceInterfaceType.IsAssignableFrom(type.Service))
            {
                services.AddScoped(type.Service, type.Implementation);
            }
        }

        return services;
    }
}
