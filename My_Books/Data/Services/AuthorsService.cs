using My_Books.Data.Models;
using My_Books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Books.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public Author AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
            return _author;
        }
    }
}
