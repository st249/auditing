using System.Text.Json;
using HAB.Auditing.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;


namespace HAB.Auditing.AspnetCore.Implementations;

public class HabAuditingFilter(IAuditInfoProvider provider, ILogger<HabAuditingFilter> logger) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var hasAudit = false;
        var attribute = context.Filters
            .OfType<AuditAttribute>()
            .FirstOrDefault();

        if (attribute == null || attribute == default)
            hasAudit = true;
        if (!hasAudit)
        {
            await next();
            return;
        }

        provider.ShouldAudit = true;

        // Build audit payload (keep minimal; adapt to your domain)
        var pre = new
        {
            UserId = provider.GetUserId(),
            ChangedBy = provider.GetChangedBy(),
            Reason = provider.GetReason(),
            Path = context.HttpContext?.Request?.Path.Value,
            Method = context.HttpContext?.Request?.Method,
            ClientIp = provider.GetClientIp(),
            Browser = provider.GetBrowserInfo(),
            Action = context.ActionDescriptor?.DisplayName,
            TimestampUtc = DateTime.UtcNow
        };
        logger.LogInformation("Audit start: {Audit}", JsonSerializer.Serialize(pre));

        var executed = await next();

        var post = new
        {
            Success = executed.Exception == null,
            Exception = executed.Exception?.Message,
            TimestampUtc = DateTime.UtcNow
        };

        logger.LogInformation("Audit end: {Audit}", JsonSerializer.Serialize(post));
    }
}
