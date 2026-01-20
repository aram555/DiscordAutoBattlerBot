using Microsoft.EntityFrameworkCore;
using WebBattler.DAL.Entities;

namespace WebBattler.DAL;

public class AutobattlerDbContext : DbContext
{
    public DbSet<UnitEntity> Units { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<ProvinceEntity> Provinces { get; set; }
    public DbSet<CityEntity> Cities { get; set; }

    public AutobattlerDbContext(DbContextOptions<AutobattlerDbContext> options) : base(options)
    {

    }

    public AutobattlerDbContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=BaseOne");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CountryEntity>()
            .HasMany(c => c.Provinces)
            .WithOne(p => p.Country)
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProvinceEntity>()
            .HasMany(p => p.Cities)
            .WithOne(c => c.Province)
            .HasForeignKey(c =>  c.ProvinceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
