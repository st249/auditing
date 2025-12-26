using System.Text.Json;
using HAB.Auditing.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace HAB.Auditing.AspnetCore.Attributes;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class AuditingAttribute() : TypeFilterAttribute(typeof(HabAuditingFilter))
{
    
}
public class HabAuditingFilter : IAsyncActionFilter
{
    private readonly IAuditInfoProvider _provider;
    private readonly ILogger<HabAuditingFilter> _logger;
    public HabAuditingFilter(IAuditInfoProvider auditInfoProvider, ILogger<HabAuditingFilter> logger)
    {
        _provider = auditInfoProvider;
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        _provider.ShouldAudit = true;
        try
        {
            var pre = new
            {
                UserId = _provider.GetUserId(),
                ChangedBy = _provider.GetChangedBy(),
                Reason = _provider.GetReason(),
                Path = context.HttpContext?.Request?.Path.Value,
                Method = context.HttpContext?.Request?.Method,
                ClientIp = _provider.GetClientIp(),
                Browser = _provider.GetBrowserInfo(),
                Action = context.ActionDescriptor?.DisplayName,
                TimestampUtc = DateTime.UtcNow
            };

            _logger.LogInformation("Audit start: {Audit}", System.Text.Json.JsonSerializer.Serialize(pre));

            var executed = await next();

            var post = new
            {
                Success = executed.Exception == null,
                Exception = executed.Exception?.Message,
                TimestampUtc = DateTime.UtcNow
            };

            _logger.LogInformation("Audit end: {Audit}", System.Text.Json.JsonSerializer.Serialize(post));
        }
        finally
        {
            _provider.ShouldAudit = false;
        }

    }
}