namespace BookLibrary.Data.Interfaces;

public interface IUnitOfWork
{
    IBookRepository BookRepository { get; }
    
    IAuthorRepository AuthorRepository { get; }
}
