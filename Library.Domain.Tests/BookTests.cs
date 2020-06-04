using Library.Domain.Entities;
using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Library.Domain.Tests
{
    public class BookTests
    {
        private readonly Guid _id;
        private readonly string _title;
        private readonly ICollection<BookCategory> _categories;
        private readonly Person _lender;

        public BookTests()
        {
            _id = Guid.NewGuid();
            _title = "Book1";
            _categories = new List<BookCategory>{ new BookCategory{ BookId = _id, CategoryId = Guid.NewGuid()},
                new BookCategory { BookId = _id, CategoryId = Guid.NewGuid() } };
            _lender = new Person("John", "john@test.com", true);
        }

        [Fact]
        public void Create_Book_With_Title_And_Categories()
        {
            var book = new Book(_id, _title, _categories);

            Assert.Equal(_id, book.Id);
            Assert.Equal(_title, book.Title);
            Assert.Equal(_categories.ToList(), book.BookCategories.ToList());
            Assert.True(book.IsAvailable);
            Assert.Equal(Status.Added, book.Status);
        }

        [Fact]
        public void Create_Book_With_Title_Categories_And_Lender()
        {
            var book = new Book(_id, _title, _categories, _lender);

            Assert.Equal(_id, book.Id);
            Assert.Equal(_title, book.Title);
            Assert.Equal(_categories.ToList(), book.BookCategories.ToList());
            Assert.Equal(_lender, book.Lender);
            Assert.False(book.IsAvailable);
            Assert.Equal(Status.Added, book.Status);
        }

        [Fact]
        public void Check_Book_Is_Available()
        {
            var book = new Book(_title, _categories);
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void Check_Book_Is_Not_Available()
        {
            var book = new Book(_title, _categories, _lender);
            Assert.False(book.IsAvailable);
        }

        [Fact]
        public void Update_Book_With_Title_And_Categories()
        {
            const string newTitle = "new title";
            var newCategories = new List<BookCategory> { new BookCategory { BookId = _id, CategoryId = Guid.NewGuid() } };
            var book = new Book(_id, _title, _categories);


            book.UpdateBook(newTitle, newCategories);

            Assert.Equal(_id, book.Id);
            Assert.Equal(newTitle, book.Title);
            Assert.Equal(newCategories, book.BookCategories.ToList());
            Assert.Equal(Status.Updated, book.Status);

        }

        [Fact]
        public void Remove_Book()
        {
            var book = new Book(_id, _title, _categories, _lender);
            book.RemoveBook();

            Assert.Equal(Status.Deleted, book.Status);
        }

        [Fact]
        public void Remove_Categories()
        {
            var book = new Book(_id, _title, _categories, _lender);
            book.RemoveCategories();

            Assert.Equal(0, book.BookCategories.Count);
        }

        [Fact]
        public void Check_Lend_Book_To()
        {
            var book = new Book(_title, _categories);
            book.LendBook(_lender);
            Assert.Equal(_lender, book.Lender);
            Assert.Equal(Status.Updated, book.Status);
        }

        [Fact]
        public void Book_Is_Returned()
        {
            var book = new Book(_title, _categories, _lender);
            book.ReturnBook();
            Assert.Null(book.Lender);
            Assert.Equal(Status.Updated, book.Status);
        }
    }
}