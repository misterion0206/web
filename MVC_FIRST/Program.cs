using EIP.Services;
using EIP.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();
// 設定 Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var logger = Log.ForContext<Program>();
// 清除原有的 Logger 提供程序
builder.Logging.ClearProviders();

// 設定 Logger 提供程序為 Serilog
builder.Logging.AddSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<INotifyService, NotifyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
logger.Information("Hello, Serilog!");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();