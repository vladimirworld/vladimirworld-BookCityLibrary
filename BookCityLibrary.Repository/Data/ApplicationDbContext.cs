using System.Reflection;
using BookLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace BookCityLibrary.Repository.Data;

public sealed class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _config;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config,
        DbSet<Book> books, DbSet<Author> authors) : base(options)
    {
        _config = config;
        Books = books;
        Authors = authors;
        Database.EnsureCreated();
    }

    public DbSet<Book> Books { get; }
    public DbSet<Author> Authors { get; }

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