using DevWorld.LaContessa.Domain.Entities.Activities;
using DevWorld.LaContessa.Domain.Entities.Banners;
using DevWorld.LaContessa.Domain.Entities.Bookings;
using DevWorld.LaContessa.Domain.Entities.Subscriptions;
using DevWorld.LaContessa.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DevWorld.LaContessa.Persistance;

public class LaContessaDbContext : DbContext
{
    private readonly LaContessaDbContextOptions _options;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected LaContessaDbContext()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    internal LaContessaDbContext(DbContextOptions<LaContessaDbContext> options) : base(options)
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public LaContessaDbContext(LaContessaDbContextOptions options)
    {
        _options = options;
    }

    public DbSet<User> Users { get; private set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Banner> Banners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (!optionsBuilder.IsConfigured)
        {
            if (_options.UseInMemoryProvider)
                optionsBuilder
                    .UseInMemoryDatabase(_options.DatabaseName ?? Guid.NewGuid().ToString())
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .EnableSensitiveDataLogging();
            else
                optionsBuilder.UseNpgsql(
                    _options.ConnectionStringTemplate != null
                        ? string.Format(_options.ConnectionStringTemplate, _options.DatabaseName)
                        : null,
                    options =>
                    {
                        options.CommandTimeout(_options.CommandTimeout);
                        options.MigrationsAssembly(_options.MigrationsAssembly.GetName().Name);
                    }
                );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.OwnsMany(a => a.ServiceList);
            entity.OwnsMany(a => a.DateList, date => { date.OwnsMany(d => d.TimeSlotList); });
            entity.OwnsMany(a => a.ActivityVariants);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateRecordDates();

        return base.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task ApplyMigrationAsync(CancellationToken cancellationToken)
    {
        await Database.MigrateAsync(cancellationToken);
    }

    public void UpdateRecordDates()
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entityEntry in ChangeTracker.Entries())
        {
            if (entityEntry.State == EntityState.Added &&
                entityEntry.Properties.Any(x => x.Metadata.Name == "InsertRecordDateTimeUtc")
               )
                entityEntry.Property("InsertRecordDateTimeUtc").CurrentValue = utcNow;

            if (entityEntry.State == EntityState.Modified &&
                entityEntry.Properties.Any(x => x.Metadata.Name == "UpdateRecordDateTimeUtc")
               )
                entityEntry.Property("UpdateRecordDateTimeUtc").CurrentValue = utcNow;
        }
    }
}
