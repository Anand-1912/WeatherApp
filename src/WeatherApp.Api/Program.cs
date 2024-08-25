using Serilog;
using Serilog.Formatting.Compact;
using WeatherApp.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console(new RenderedCompactJsonFormatter()));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRequestContextLogging();
app.UseSerilogRequestLogging();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");
app.Run();


namespace WeatherApp.Api
{
    public partial class Program { }
}