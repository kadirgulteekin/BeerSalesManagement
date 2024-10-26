using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using System.Reflection;
using Web.Api;
using Web.Api.Extensions;
using Web.Api.OutputCaching;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();


builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));


builder.Services.AddSwaggerGenWithAuth();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(b => b.AddPolicy<CustomPolicy>().SetCacheKeyPrefix("custom-"), true);
});
builder.Services.AddStackExchangeRedisOutputCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
    options.InstanceName = "cal-connect";
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseRequestContextLogging();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseOutputCache();
await app.RunAsync();

namespace Web.Api
{
    public partial class Program;
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
