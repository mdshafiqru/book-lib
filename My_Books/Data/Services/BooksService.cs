using My_Books.Data.Models;
using My_Books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context; 
        }

        public Book AddBookWithAuthors(BookVM book)
        {
            var _book = new Book() 
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };
            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var id in book.AuthorIds)
            {
                var _book_Author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.Books_Authors.Add(_book_Author);
                _context.SaveChanges();
            }

            return _book;
        }
        public List<Book> GetAllBooks() => _context.Books.ToList();
        public BookWithAuthorsVM GetBookById(int bookId) 
        {
            var _bookWithAuthors = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAuthorsVM() 
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl, 
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n=> n.Author.FullName).ToList()
            }).FirstOrDefault();
            return _bookWithAuthors;          
        }

        public Book UpdateBookById(int bookId, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if (book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead : null;
                _book.Rate = book.IsRead ? book.Rate : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
                _context.SaveChanges();
            }
            return _book;
        }

        public void DeleteBookById(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
        
    }
}
