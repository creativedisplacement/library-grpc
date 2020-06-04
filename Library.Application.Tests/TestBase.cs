using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Application.Tests
{
    public abstract class TestBase
    {
        protected LibraryDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new LibraryDbContext(options);

            context.Database.EnsureCreated();

            SeedCategories(context);
            SeedPeople(context);
            SeedBooks(context);
            SeedBookCategories(context);

            return context;
        }

        protected async Task AddCategory(LibraryDbContext context, Guid id, string name)
        {
            var category = new Domain.Entities.Category(name) { Id = id, Status = Status.Added };
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        private void SeedBookCategories(LibraryDbContext context)
        {
            var categories = context.Categories;
            var books = context.Books;

            context.BookCategories.AddRange(
            new BookCategory { Book = books.First(), Category = categories.Skip(5).Take(1).First() },
            new BookCategory { Book = books.Skip(1).Take(1).First(), Category = categories.Skip(1).Take(1).First() },
            new BookCategory { Book = books.Skip(1).Take(1).First(), Category = categories.Skip(4).Take(1).First() },
            new BookCategory { Book = books.Skip(2).Take(1).First(), Category = categories.Skip(4).Take(1).First() },
            new BookCategory { Book = books.Skip(2).Take(1).First(), Category = categories.Skip(1).Take(1).First() },
            new BookCategory { Book = books.Skip(2).Take(1).First(), Category = categories.First() });
            context.SaveChanges();
        }

        private void SeedBooks(LibraryDbContext context)
        {
            var lender = context.Persons.First();
            var books = new[]
            {
                new Domain.Entities.Book("Docker on Windows", new List<BookCategory>(), lender),
                new Domain.Entities.Book("Open"),
                new Domain.Entities.Book("This is going to hurt")
            };
            context.Books.AddRange(books);
            context.SaveChanges();
        }

        private void SeedCategories(LibraryDbContext context)
        {
            var categories = new[]
            {
                new Domain.Entities.Category("Humour"),
                new Domain.Entities.Category("Drama"),
                new Domain.Entities.Category("Action"),
                new Domain.Entities.Category("Thriller"),
                new Domain.Entities.Category("Biographical"),
                new Domain.Entities.Category("Technical"),
                new Domain.Entities.Category("Comic")
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private void SeedPeople(LibraryDbContext context)
        {
            var people = new[]
            {
                new Person("Victor","v@v.com", true),
                new Person("Tunde","t@t.com", false),
                new Person("Hamed","h@h.com", false)
            };
            context.Persons.AddRange(people);
            context.SaveChanges();
        }
    }
}