using BookCityLibrary.Repository.Data;
using BookCityLibrary.Repository.DataAccess;
using BookLibrary.Data.Interfaces;

namespace BookCityLibrary.Api.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ISqlDataAccess, SqliteDataAccess>();

        services.AddDbContext<ApplicationDbContext>();

        services.AddScoped<IBookRepository, BookSqlRepository>();
        services.AddScoped<IAuthorRepository, AuthorSqlRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}