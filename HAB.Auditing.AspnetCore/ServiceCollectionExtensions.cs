using HAB.Auditing.Abstractions;
using HAB.Auditing.AspnetCore.Implementations;
using HAB.Auditing.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace HAB.Auditing.AspnetCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuditing(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<AuditsSaveChangesInterceptor>();
        services.AddScoped<IAuditInfoProvider, DefaultHttpContextAuditInfoProvider>();
        return services;
    }
}
