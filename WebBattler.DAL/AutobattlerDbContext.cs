using WebBattler.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebBattler.DAL;

public class AutobattlerDbContext : DbContext
{
    public DbSet<ArmyEntity> Armies { get; set; }
    public DbSet<UnitEntity> Units { get; set; }
    public DbSet<UnitSampleEntity> UnitSamples { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<ProvinceEntity> Provinces { get; set; }
    public DbSet<ProvinceEntity> Neighbours { get; set; }
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<BuildingEntity> Buildings { get; set; }
    public DbSet<BuildingSampleEntity> BuildingSamples { get; set; }
    public DbSet<GameSessionEntity> GameSessions { get; set; }
    public DbSet<ProductionOrderEntity> ProductionOrders { get; set; }

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
        modelBuilder.Entity<GameSessionEntity>()
            .HasIndex(g => g.GuildId)
            .IsUnique();

        modelBuilder.Entity<CountryEntity>()
            .HasOne(c => c.GameSession)
            .WithMany(g => g.Countries)
            .HasForeignKey(c => c.GameSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductionOrderEntity>()
            .HasOne(o => o.GameSession)
            .WithMany(g => g.ProductionOrders)
            .HasForeignKey(o => o.GameSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductionOrderEntity>()
            .HasOne(o => o.UnitSample)
            .WithMany()
            .HasForeignKey(o => o.UnitSampleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductionOrderEntity>()
            .HasOne(o => o.BuildingSample)
            .WithMany()
            .HasForeignKey(o => o.BuildingSampleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductionOrderEntity>()
            .HasOne(o => o.Army)
            .WithMany()
            .HasForeignKey(o => o.ArmyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductionOrderEntity>()
            .HasOne(o => o.City)
            .WithMany()
            .HasForeignKey(o => o.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CountryEntity>()
            .HasMany(c => c.Provinces)
            .WithOne(p => p.Country)
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CountryEntity>()
            .HasMany(c => c.UnitSamples)
            .WithOne(u => u.Country)
            .HasForeignKey(u => u.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CountryEntity>()
            .HasMany(c => c.BuildingSamples)
            .WithOne(u => u.Country)
            .HasForeignKey(u => u.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProvinceEntity>()
            .HasMany(p => p.Cities)
            .WithOne(c => c.Province)
            .HasForeignKey(c =>  c.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProvinceEntity>()
            .HasMany(p => p.Neighbours)
            .WithMany()
            .UsingEntity(n => n.ToTable("ProvinceNeighbours"));

        modelBuilder.Entity<CityEntity>()
            .HasMany(c => c.Buildings)
            .WithOne(b => b.City)
            .HasForeignKey(b => b.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ArmyEntity>()
            .HasMany(a => a.SubArmies)
            .WithOne(a => a.Parent)
            .HasForeignKey(a => a.ParentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ArmyEntity>()
            .HasOne(a => a.Province)
            .WithMany()
            .HasForeignKey(a => a.ProvinceId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ArmyEntity>()
            .HasOne(a => a.City)
            .WithMany()
            .HasForeignKey(a => a.CityId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ArmyEntity>()
            .HasMany(a => a.Units)
            .WithOne(u => u.Army)
            .HasForeignKey(u => u.ArmyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ArmyEntity>()
            .HasOne(a => a.Country)
            .WithMany(c => c.Armies)
            .HasForeignKey(a => a.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
