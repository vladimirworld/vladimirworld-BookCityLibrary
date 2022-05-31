using System;
using System.Linq;
using System.Reflection;
using BookLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace BookCityLibrary.Repository.Data;

public sealed class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _config;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config) : base(options)
    {
        _config = config;
        Database.EnsureCreated();
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        if (Database.ProviderName != "Microsoft.EntityFrameworkCore.Sqlite") return;
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
            var dateTimeProperties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(DateTimeOffset));

            foreach (var property in properties)
            {
                builder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
            }

            foreach (var property in dateTimeProperties)
            {
                builder.Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(new DateTimeOffsetToBinaryConverter());
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_config.GetConnectionString("sqlite") ?? string.Empty);
    }
}