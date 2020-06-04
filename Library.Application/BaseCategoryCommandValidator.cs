using FluentValidation;
using Library.Persistence;
using System;
using System.Linq;

namespace Library.Application
{
    public abstract class BaseCategoryCommandValidator<T> : AbstractValidator<T> where T : class
    {
        private readonly LibraryDbContext _context;

        public BaseCategoryCommandValidator(LibraryDbContext context)
        {
            _context = context;
        }
        protected bool CategoryNameExists(string name, Guid categoryId)
        {
            var result = _context.Categories.Count(c => c.Name == name && c.Id != categoryId);
            return result <= 0;
        }
    }
}