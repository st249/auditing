# HAB.Auditing

HAB.Auditing is a set of lightweight and extensible libraries that help **ASP.NET Core applications audit and track entity changes** in a clean, reliable, and non-intrusive way.

The project is designed to:
- Be easy to integrate
- Require minimal configuration
- Avoid breaking existing code
- Keep auditing concerns clearly separated from business logic

---

## 🚀 Getting Started

This guide shows how to enable auditing and entity change tracking in an ASP.NET Core application using Entity Framework Core.

---

## 📦 Installation

Install the required NuGet packages (example for **.NET 10**):

```bash
dotnet add package HAB.Auditing --version 10.0.0
dotnet add package HAB.Auditing.AspnetCore --version 10.0.0
dotnet add package HAB.Auditing.EntityFramework --version 10.0.0
```

---

## 🧩 Register Auditing Services

HAB.Auditing allows you to either **use the default audit info provider** or **plug in your own custom provider**.

### Option 1: Use a custom audit info provider

Use this option if you want full control over how audit metadata is resolved (e.g. user ID, client IP, browser info, reason, etc.).

```csharp
// Adding auditing services with a custom provider
builder.Services.AddAuditing<CustomAuditInfoProvider>();
```

Your custom provider can resolve audit information from any source such as claims, headers, tokens, or external services.

---

### Option 2: Use the default HTTP context audit info provider

This option works out of the box for most ASP.NET Core applications and resolves audit information from the current `HttpContext`.

```csharp
// Adding auditing services with the default provider
builder.Services.AddAuditing<DefaultHttpContextAuditInfoProvider>();
```

This is the recommended option if you want a **quick setup with minimal configuration**.

---

## 🗄 Configure Entity Framework Core Interceptor

Auditing of entity changes is handled through an **EF Core SaveChanges interceptor**.

Register your DbContext and add the auditing interceptor:

```csharp
builder.Services.AddDbContext<YourDbContext>((sp, options) =>
{
    var interceptor = sp.GetRequiredService<AuditsSaveChangesInterceptor>();

    options.UseSqlServer(
            configuration.GetConnectionString("Default"),
            b => b.MigrationsAssembly("YourAssembly"))
        .AddInterceptors(interceptor);
});
```

### What this does

- Hooks into EF Core change tracking
- Captures entity-level and property-level changes
- Persists audit data automatically during `SaveChanges()`

---

## 🧱 Create Auditing Tables

After configuring the interceptor, create the default auditing tables in your database.

Run the following commands:

```bash
dotnet ef migrations add InitAuditing
dotnet ef database update
```

This will generate and apply migrations for:
- Change sets
- Entity changes
- Property changes

---

## 🏷 Enable Auditing on Controller Actions

Auditing is **opt-in** and enabled using an attribute.

Add the `[Auditing]` attribute to the controller actions you want to audit:

```csharp
[Auditing]
[HttpPost]
public IActionResult UpdateOrder(Order order)
{
    return Ok();
}
```

Only actions marked with `[Auditing]` will:
- Trigger audit tracking
- Record entity and property changes
- Store request and user metadata

This keeps auditing **explicit, predictable, and easy to reason about**.

---

## ✅ Summary

With just a few steps, you now have:
- Auditing services registered
- EF Core change tracking enabled
- Database tables created
- Auditing activated on selected endpoints

Your application is now ready to **audit and track entity changes**.

---

## 🛣 Roadmap

- PostgreSQL support
- MongoDB (NoSQL) provider
- Advanced filtering and masking options
- Performance optimizations

---

## 🤝 Contributing & Feedback

Contributions, suggestions, and feedback are very welcome.  
Feel free to open issues, submit pull requests, or start discussions.

GitHub repository:  
https://github.com/st249/auditing
