using Microsoft.AspNetCore.Authentication.Cookies;
using WebBattler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebBattler.Services.Services;
using WebBattler.DAL.Repositories;
using WebBattler.DAL.Interfaces;
using Discord.Interactions;
using WebBattler.Services;
using Discord.WebSocket;
using WebBattler.DAL;
using Discord;

namespace WebBattler;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.AccessDeniedPath = "/Auth/Login";
            });

        builder.Services.AddDbContext<AutobattlerDbContext>();


        builder.Services.AddTransient<IArmyRepository, ArmyRepository>();
        builder.Services.AddTransient<IUnitRepository, UnitRepository>();
        builder.Services.AddTransient<IBuildingRepository, BuildingRepository>();
        builder.Services.AddTransient<ICityRepository, CityRepository>();
        builder.Services.AddTransient<IProvinceRepository, ProvinceRepository>();
        builder.Services.AddTransient<ICountryRepository, CountryRepository>();
        builder.Services.AddTransient<IBuildingSampleRepository, BuildingSampleRepository>();
        builder.Services.AddTransient<IUnitSampleRepository, UnitSampleRepository>();
        builder.Services.AddTransient<IGameSessionRepository, GameSessionRepository>();
        builder.Services.AddTransient<IProductionOrderRepository, ProductionOrderRepository>();
        builder.Services.AddTransient<IAdminAccountRepository, AdminAccountRepository>();

        builder.Services.AddScoped<IArmyService, ArmyService>();
        builder.Services.AddScoped<IBuildingSampleService, BuildingSampleService>();
        builder.Services.AddScoped<IBuildingService, BuildingService>();
        builder.Services.AddScoped<ICityService, CityService>();
        builder.Services.AddScoped<ICountryService, CountryService>();
        builder.Services.AddScoped<IProvinceService, ProvinceService>();
        builder.Services.AddScoped<IUnitSampleService, UnitSampleService>();
        builder.Services.AddScoped<IUnitService, UnitService>();
        builder.Services.AddScoped<IGameSessionService, GameSessionService>();
        builder.Services.AddScoped<IProductionOrderService, ProductionOrderService>();
        builder.Services.AddScoped<IAdminAccountService, AdminAccountService>();

        //Addint discord bot service
        builder.Services.AddSingleton(sp =>
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged
            };

            return new DiscordSocketClient(config);
        });

        builder.Services.AddSingleton(sp =>
        {
            var client = sp.GetRequiredService<DiscordSocketClient>();

            var config = new InteractionServiceConfig
            {
                LogLevel = LogSeverity.Info,
                UseCompiledLambda = true
            };

            return new InteractionService(client, config);
        });

        builder.Services.AddHostedService<DiscordBotService>();

        var app = builder.Build();

        app.UseDeveloperExceptionPage();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}