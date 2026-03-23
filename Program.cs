using AspMarketplace.Web.Extensions;
using AspMarketplace.Web.Middleware;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, services, cfg) =>
        cfg.ReadFrom.Configuration(ctx.Configuration)
           .ReadFrom.Services(services));

    builder.Services
        .AddDatabase(builder.Configuration)
        .AddCookieAuth()
        .AddApplicationServices()
        .AddValidation()
        .AddControllersWithViews();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSerilogRequestLogging();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapAreaControllerRoute(
        name: "admin",
        areaName: "Admin",
        pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}"
    );

    app.MapAreaControllerRoute(
        name: "auth",
        areaName: "Auth",
        pattern: "auth/{controller=Account}/{action=Login}/{id?}"
    );

    app.MapAreaControllerRoute(
        name: "public",
        areaName: "Public",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Aplikasi gagal start.");
}
finally
{
    Log.CloseAndFlush();
}
