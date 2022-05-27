using BookLibrary.Data.Interfaces;

namespace BookCityLibrary.Repository.Data;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IBookRepository bookRepo, IAuthorRepository authRepo)
    {
        BookRepository = bookRepo;
        AuthorRepository = authRepo;
    }

    public IBookRepository BookRepository { get; }

    public IAuthorRepository AuthorRepository { get; }
}