namespace HAB.Auditing.Abstractions;

public interface IAuditInfoProvider
{
    string GetUserId();
    string GetReason();
    string? GetChangedBy();
    string? GetClientIp();
    string? GetBrowserInfo();

    bool ShouldAudit { get; set; }
}
