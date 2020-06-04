using FluentValidation;
using Library.Persistence;
using System;
using System.Linq;

namespace Library.Application
{
    public abstract class BaseBookCommandValidator<T> : AbstractValidator<T> where T : class
    {
        private readonly LibraryDbContext _context;

        public BaseBookCommandValidator(LibraryDbContext context)
        {
            _context = context;
        }

        protected bool BookTitleExists(string title, Guid bookId)
        {
            var result = _context.Books.Count(c => c.Title == title && c.Id != bookId);
            return result <= 0;
        }

        protected bool BookTitleExists(string title)
        {
            var result = _context.Books.Count(c => c.Title == title);
            return result <= 0;
        }
    }
}