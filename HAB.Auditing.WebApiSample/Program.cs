using HAB.Auditing.Abstractions;
using HAB.Auditing.AspnetCore;
using HAB.Auditing.EntityFramework;
using HAB.Auditing.WebApiSample;
using HAB.Auditing.WebApiSample.AuditingCustoms;
using HAB.Auditing.WebApiSample.Context;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment env = builder.Environment;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Adding auditing services
builder.Services.AddAuditing<CustomAuditInfoProvider>();


//Adding context
builder.Services.AddDbContext<SampleDbContext>((sp, options) =>
{
    var interceptor = sp.GetRequiredService<AuditsSaveChangesInterceptor>();
    options.UseSqlServer(configuration.GetConnectionString("Default"))
        .AddInterceptors(interceptor);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

InitializeDatabase(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void InitializeDatabase(WebApplication webApplication)
{
    using var scope = webApplication.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SampleDbContext>();
    context.Database.Migrate();
}
