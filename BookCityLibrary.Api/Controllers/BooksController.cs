using AutoMapper;
using BookCityLibrary.Api.Dtos;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookCityLibrary.Api.Controllers;

public class BooksController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BooksController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get a list of all Books as a JSON response
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBooks(string? search)
    {
        try
        {
            if (string.IsNullOrEmpty(search))
            {
                var books = await _unitOfWork.BookRepository.FindAll();

                return Ok(books);
            }

            var searchBook = await _unitOfWork.BookRepository.FindBySearch(search);

            return Ok(searchBook);
        }
        catch (Exception ex)
        {
            return InternalError($"{ex.Message} - {ex.InnerException}");
        }
    }

    /// <summary>
    /// Get a Single Book with a specific Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBook(int id)
    {
        try
        {
            var book = await _unitOfWork.BookRepository.FindById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        catch (Exception ex)
        {
            return InternalError($"{ex.Message} - {ex.InnerException}");
        }
    }

    /// <summary>
    /// Create A Book
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBook([FromBody] BookDto? book)
    {
        try
        {
            if (book == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<Book>(book);

            var isSuccess = await _unitOfWork.BookRepository.Create(model);

            return !isSuccess ? InternalError($"Record creation failed") : Created("CreateBook", new { book });
        }
        catch (Exception ex)
        {
            return InternalError($"{ex.Message} - {ex.InnerException}");
        }
    }

    /// <summary>
    /// Update A Book
    /// </summary>
    /// <param name="id"></param>
    /// <param name="bookToUpdate"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto? bookToUpdate)
    {
        try
        {
            if (id < 1 || bookToUpdate == null || id != bookToUpdate.Id)
            {
                return BadRequest();
            }

            var ifExists = await _unitOfWork.BookRepository.IsExists(id);

            if (ifExists == false)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<Book>(bookToUpdate);

            var isSuccess = await _unitOfWork.BookRepository.Update(model);

            if (!isSuccess)
            {
                return InternalError($"Update operation failed");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return InternalError($"{ex.Message} - {ex.InnerException}");
        }
    }

    /// <summary>
    /// Delete A Book
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var book = await _unitOfWork.BookRepository.FindById(id);

            if (book == null)
            {
                return NotFound();
            }

            var isSuccess = await _unitOfWork.BookRepository.Delete(book);

            if (!isSuccess)
            {
                return InternalError($"Delete operation failed");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return InternalError($"{ex.Message} - {ex.InnerException}");
        }
    }

    private ObjectResult InternalError(string message)
    {
        return StatusCode(500, $"Something went wrong. Please contact the Administrator. ErrorMessage: {message}");
    }
}