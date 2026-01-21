using WebBattler.DAL;
using WebBattler.DAL.Interfaces;
using WebBattler.DAL.Repositories;

namespace WebBattler;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<AutobattlerDbContext>();
        builder.Services.AddTransient<IArmyRepository, ArmyRepository>();
        builder.Services.AddTransient<IUnitRepository, UnitRepository>();
        builder.Services.AddTransient<IBuildingRepository, BuildingRepository>();
        builder.Services.AddTransient<ICityRepository, CityRepository>();
        builder.Services.AddTransient<IProvinceRepository, ProvinceRepository>();
        builder.Services.AddTransient<ICountryRepository, CountryRepository>();

        var app = builder.Build();

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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
