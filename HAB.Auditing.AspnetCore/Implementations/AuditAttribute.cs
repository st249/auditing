using Microsoft.AspNetCore.Mvc.Filters;

namespace HAB.Auditing.AspnetCore.Implementations;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class AuditAttribute : Attribute, IFilterMetadata
{
}