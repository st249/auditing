using HAB.Auditing.Abstractions;

namespace HAB.Auditing.AspnetCore.Implementations;


public abstract class BaseAuditInfoProvider : IAuditInfoProvider
{
    public bool ShouldAudit { get; set; } = false;
    
    public virtual string GetUserId()
    {
        return "";
    }

    public virtual string GetReason()
    {
        return "";
    }

    public virtual string? GetChangedBy()
    {
        return "";
    }

    public virtual string? GetClientIp()
    {
        return "";
    }

    public virtual string? GetBrowserInfo()
    {
        return "";
    }
}
