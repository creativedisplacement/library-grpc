using Library.Domain.Entities;
using System.Linq;

namespace Library.Persistence
{
    public class LibraryInitialiser
    {

        public static void Initialise(LibraryDbContext context)
        {
            var initialiser = new LibraryInitialiser();
            initialiser.SeedEverything(context);
        }

        public void SeedEverything(LibraryDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Books.Any())
            {
                return; // Db has been seeded
            }

            SeedCategories(context);
            SeedPeople(context);
            SeedBooks(context);
            SeedBookCategories(context);
        }

        public void SeedBookCategories(LibraryDbContext context)
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

        public void SeedBooks(LibraryDbContext context)
        {
            var books = new[]
            {
                new Book("Docker on Windows"),
                new Book("Open"),
                new Book("This is going to hurt")
            };
            context.Books.AddRange(books);
            context.SaveChanges();
        }

        public void SeedCategories(LibraryDbContext context)
        {
            var categories = new[]
            {
                new Category("Humour"),
                new Category("Drama"),
                new Category("Action"),
                new Category("Thriller"),
                new Category("Biographical"),
                new Category("Technical"),
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        public void SeedPeople(LibraryDbContext context)
        {
            var people = new[]
            {
                new Person("Victor","victor@victor.com", true),
                new Person("Tunde","tunde@tunde.com", false),
            };
            context.Persons.AddRange(people);
            context.SaveChanges();
        }
    }
}
