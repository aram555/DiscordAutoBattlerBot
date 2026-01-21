using WebBattler.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebBattler.DAL;

public class AutobattlerDbContext : DbContext
{
    public DbSet<ArmyEntity> Armies { get; set; }
    public DbSet<UnitEntity> Units { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<ProvinceEntity> Provinces { get; set; }
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<BuildingEntity> Buildings { get; set; }

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

        modelBuilder.Entity<CityEntity>()
            .HasMany(c => c.Buildings)
            .WithOne(b => b.City)
            .HasForeignKey(b => b.CityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ArmyEntity>()
            .HasMany(a => a.SubArmies)
            .WithOne(a => a.Parent)
            .HasForeignKey(a => a.ParentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ArmyEntity>()
            .HasOne(a => a.Country)
            .WithMany(c => c.Armies)
            .HasForeignKey(a => a.CountryId);
    }
}
