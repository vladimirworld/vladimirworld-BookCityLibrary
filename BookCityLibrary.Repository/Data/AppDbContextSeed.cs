using BookLibrary.Data.Entities;

namespace BookCityLibrary.Repository.Data;

public static class AppDbContextSeed
{
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        if (!dbContext.Authors.Any())
        {
            var authors = new List<Author>
            {
                new()
                {
                    FirstName = "Александр",
                    LastName = "Пушкин",
                    Bio = "А. Пушкин начал писать свои первые произведения уже в семь лет."
                },
                new()
                {
                    FirstName = "Джон",
                    LastName = "Смит",
                    Bio = "Написал книгу по Entity Framework."
                },
                new()
                {
                    FirstName = "Михаил",
                    LastName = "Булгаков",
                    Bio = "Написал \"Мастера и Маргариту\"."
                }
            };

            await dbContext.Authors.AddRangeAsync(authors);
        }

        if (!dbContext.Books.Any())
        {
            var books = new List<Book>
            {
                new()
                {
                    AuthorId = 1,
                    Title = "Евгений Онегин",
                    Year = 1833,
                    Summary = "Роман в стихах русского писателя и поэта Александра Сергеевича Пушкина",
                    Isbn = "128-1617294617",
                    Price = 44.99m,
                    Image = "empty"
                },
                new()
                {
                    AuthorId = 2,
                    Title = "Entity Framework Core In Action",
                    Year = 2018,
                    Summary =
                        "Книга научит пользоваться Entity Framework",
                    Isbn = "978-1617294563",
                    Price = 39.70m,
                    Image = "empty"
                },
                new()
                {
                    AuthorId = 3,
                    Title = "Мастер и Маргарита",
                    Year = 1966,
                    Summary =
                        "Роман Михаила Афанасьевича Булгакова, работа над которым началась в декабре 1928 года и продолжалась вплоть до смерти писателя",
                    Isbn = "978-1484254394",
                    Price = 34.99m,
                    Image = "empty"
                },
                new()
                {
                    AuthorId = 3,
                    Title = "Копыто инженера",
                    Year = 1928,
                    Summary =
                        "Вторая тетрадь из сохранившихся черновиков первой редакции романа 1928—1929 годов при публикации условно названа «Копыто инженера»",
                    Isbn = "978-1484244500",
                    Price = 35.97m,
                    Image = "empty"
                }
            };

            await dbContext.Books.AddRangeAsync(books);
        }

        await dbContext.SaveChangesAsync();
    }
}