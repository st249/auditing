using System.Security.Claims;
using HAB.Auditing.Abstractions;
using Microsoft.AspNetCore.Http;

namespace HAB.Auditing.AspnetCore.Implementations;

public class DefaultHttpContextAuditInfoProvider : BaseAuditInfoProvider, IAuditInfoProvider
{
    private readonly IHttpContextAccessor http;

    public DefaultHttpContextAuditInfoProvider(IHttpContextAccessor http)
    {
        this.http = http;
    }

    public override string GetUserId()
    {
        var ctx = http.HttpContext;
        var sub = ctx?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? ctx?.User?.FindFirst("sub")?.Value;
        return int.TryParse(sub, out var id) ? id.ToString() : "0";
    }

    public override string GetReason() =>
        GetRequestPath();

    public override string? GetChangedBy() =>
        http.HttpContext?.User?.Identity?.Name;

    public override string? GetClientIp() =>
        http.HttpContext?.Connection?.RemoteIpAddress?.ToString();

    public override string? GetBrowserInfo() =>
        http.HttpContext?.Request?.Headers["User-Agent"].ToString();

    private string GetRequestPath() =>
        http.HttpContext?.Request?.Path.Value ?? string.Empty;
}
