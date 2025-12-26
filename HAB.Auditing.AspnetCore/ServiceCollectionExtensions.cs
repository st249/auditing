using HAB.Auditing.Abstractions;
using HAB.Auditing.AspnetCore.Implementations;
using HAB.Auditing.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HAB.Auditing.AspnetCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuditing<TProvider>(this IServiceCollection services) where TProvider : class, IAuditInfoProvider
    {
        services.AddHttpContextAccessor();
        services.AddScoped<AuditsSaveChangesInterceptor>();
        services.TryAddScoped<IAuditInfoProvider, TProvider>();

        return services;
    }
}
