using System;
using System.Threading.Tasks;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookCityLibrary.Api.Controllers;

public class AuthorsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthorsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get All Authors as a list of JSON response
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAuthors(string? search)
    {
        try
        {
            if (string.IsNullOrEmpty(search))
            {
                var authors = await _unitOfWork.AuthorRepository.FindAll();

                return Ok(authors);
            }

            var searchAuthor = await _unitOfWork.AuthorRepository.FindBySearch(search);

            return Ok(searchAuthor);
        }
        catch (Exception ex)
        {
            return InternalError($"{ex.Message} - {ex.InnerException}");
        }
    }

    /// <summary>
    /// Get a Single Author with a specific Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        try
        {
            var author = await _unitOfWork.AuthorRepository.FindById(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }
        catch (Exception ex)
        {
            return InternalError($"{ex.Message} - {ex.InnerException}");
        }
    }

    /// <summary>
    /// Create an Author
    /// </summary>
    /// <param name="author"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAuthor([FromBody] Author? author)
    {
        try
        {
            if (author == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSuccess = await _unitOfWork.AuthorRepository.Create(author);

            return !isSuccess ? InternalError($"Record creation failed") : Created("CreateAuthor", new { author });
        }
        catch (Exception ex)
        {
            return InternalError($"{ex.Message} - {ex.InnerException}");
        }
    }

    /// <summary>
    /// Update An Author
    /// </summary>
    /// <param name="id"></param>
    /// <param name="authorToUpdate"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author? authorToUpdate)
    {
        try
        {
            if (id < 1 || authorToUpdate == null || id != authorToUpdate.Id)
            {
                return BadRequest();
            }

            var ifExists = await _unitOfWork.AuthorRepository.IsExists(id);

            if (ifExists == false)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSuccess = await _unitOfWork.AuthorRepository.Update(authorToUpdate);

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
    /// Delete An Author
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        try
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var author = await _unitOfWork.AuthorRepository.FindById(id);

            if (author == null)
            {
                return NotFound();
            }

            var isSuccess = await _unitOfWork.AuthorRepository.Delete(author);

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