using HAB.Auditing.AspnetCore.Implementations;

namespace HAB.Auditing.WebApiSample.AuditingCustoms;

public class CustomAuditInfoProvider(IHttpContextAccessor http) : BaseAuditInfoProvider
{
    public override string GetUserId()
    {
        var ctx = http.HttpContext;
        var sub = ctx?.Request.Headers["X-User-Id"].ToString();
        return string.IsNullOrEmpty(sub) ? "" : sub;
    }

    public override string GetReason() =>
        GetRequestPath();

    public override string GetChangedBy()
    {
        var ctx = http.HttpContext;
        var name = ctx?.Request.Headers["X-User-Name"].ToString();
        return string.IsNullOrEmpty(name) ? "" : name;
    }

    public override string? GetClientIp() =>
        http.HttpContext?.Connection.RemoteIpAddress?.ToString();

    public override string? GetBrowserInfo() =>
        http.HttpContext?.Request.Headers["User-Agent"].ToString();

    private string GetRequestPath() =>
        http.HttpContext?.Request.Path.Value ?? string.Empty;
}
