using Microsoft.AspNetCore.Mvc;
using BookHandling.Models;
using BookHandling.Services;

namespace BookHandling.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(_bookService.GetBooks());
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = book.Title.Trim(),
                Author = book.Author.Trim(),
                PublishedDate = book.PublishedDate,
                AddedDate = DateTime.UtcNow
            };

            _bookService.AddBook(newBook);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook.Id }, newBook);
        }
        

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBook(Guid id)
        {
            bool deleted = _bookService.DeleteBook(id);
            return deleted ? NoContent() : NotFound();
        }
        
        [HttpPatch("{id}")]
        public IActionResult UpdateBook(Guid id, [FromBody] Book? updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest(new { message = "Ogiltig data: Inga uppgifter skickades." });
            }

            if (id != updatedBook.Id)
            {
                return BadRequest(new { message = "Ogiltig data: ID matchar inte." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool updated = _bookService.UpdateBook(id, updatedBook);

            return updated ? Ok(updatedBook) : NotFound(new { message = "Boken kunde inte hittas." });
        }






    }
}