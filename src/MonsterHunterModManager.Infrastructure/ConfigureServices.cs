using Microsoft.Extensions.DependencyInjection;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Infrastructure.Persistence;
using MonsterHunterModManager.Infrastructure.Services;

namespace MonsterHunterModManager.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IPickerService, PickerService>();
        serviceCollection.AddSingleton<IPhysicalFileService, PhysicalFileService>();
        
        serviceCollection.AddScoped<IApplicationPersistenceContext, ApplicationPersistenceContext>();
        
        return serviceCollection;
    }
}