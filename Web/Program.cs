using Application;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Infrastructure;
using Serilog;
using Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(builder.Configuration)
              .Enrich.FromLogContext()
              .CreateLogger();

builder.Host.UseSerilog();
builder.Services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddNotyf(config => { config.DurationInSeconds = 300; config.IsDismissable = true; config.Position = NotyfPosition.TopCenter; });

builder.Services.AddWebConfiguration().AddApplication()
                .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting(); 
app.UseAuthentication();
app.UseAuthorization();

app.UseNotyf();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Area",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();