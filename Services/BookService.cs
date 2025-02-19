using BookHandling.Models;

namespace BookHandling.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
        void AddBook(Book book);
        bool DeleteBook(Guid id);
        bool UpdateBook(Guid id, Book updatedBook);
    }

    public class BookService : IBookService
    {
        private readonly List<Book> _books = new();

        public IEnumerable<Book> GetBooks()
        {
            return _books;
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public bool DeleteBook(Guid id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null) return false;

            _books.Remove(book);
            return true;
        }
        
        public bool UpdateBook(Guid id, Book updatedBook)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return false;
            }

            existingBook.Title = updatedBook.Title.Trim();
            existingBook.Author = updatedBook.Author.Trim();
            existingBook.PublishedDate = updatedBook.PublishedDate;

            return true;
        }

    }
}