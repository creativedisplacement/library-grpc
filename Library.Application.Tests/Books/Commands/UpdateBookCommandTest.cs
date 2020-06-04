using FluentValidation.TestHelper;
using Library.Application.Book.Commands.UpdateBook;
using Library.Common.Models.Book;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Commands
{
    public class UpdateBookCommandTest : TestBase
    {
        [Fact]
        public async Task Update_Book()
        {
            using (var context = GetContextWithData())
            {
                var handler = new UpdateBookCommandHandler(context);

                var categoryId = Guid.NewGuid();

                await AddCategory(context, categoryId, "nothing");

                var command = new UpdateBookCommand
                {
                    Id = (await context.Books.FirstOrDefaultAsync()).Id,
                    Title = "Title2",
                    Categories = new List<GetBookModelCategory> {new GetBookModelCategory {Id = categoryId, Name = "nothing"} }
                };

                await handler.Handle(command, CancellationToken.None);

                var book = await context.Books
                    .Include(c => c.BookCategories)
                    .ThenInclude(c => c.Category)
                    .Where(b => b.Id == command.Id)
                    .FirstOrDefaultAsync();

                var bookCategories = book.BookCategories.Select(b => new GetBookModelCategory
                        {Id = b.CategoryId, Name = b.Category.Name})
                    .ToList();

                Assert.Equal(command.Title, book.Title);
                Assert.Equal(command.Categories.Count, book.BookCategories.Count);
                Assert.Equal(command.Categories.ToList().FirstOrDefault()?.Id, bookCategories.FirstOrDefault()?.Id);
                Assert.Equal(command.Categories.ToList().FirstOrDefault()?.Name, bookCategories.FirstOrDefault()?.Name);

            }
        }

        [Fact]
        public void Update_Book_With_No_Id_Throws_Exception()
        {
            using (var context = GetContextWithData())
            {
                var validator = new UpdateBookCommandValidator(context);
                validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
            }
        }

        [Fact]
        public void Update_Book_With_No_Title_Throws_Exception()
        {
            using (var context = GetContextWithData())
            {
                var validator = new UpdateBookCommandValidator(context);
                validator.ShouldHaveValidationErrorFor(x => x.Title, string.Empty);
            }
        }

        [Fact]
        public void Update_Book_With_No_Categories_Throws_Exception()
        {
            using (var context = GetContextWithData())
            {
                var validator = new UpdateBookCommandValidator(context);
                validator.ShouldHaveValidationErrorFor(x => x.Categories, new List<GetBookModelCategory>());
            }
        }

        [Fact]
        public void Update_Book_With_Title_That_Already_Exists_Throws_Exception()
        {
            using (var context = GetContextWithData())
            {
                var book = context.Books.FirstOrDefault(b => b.Title == "Open");

                if (book != null)
                {
                    book.UpdateBook("Docker on Windows", book.BookCategories);

                    var validator = new UpdateBookCommandValidator(context);
                    var result = validator.TestValidate(new UpdateBookCommand
                    {
                        
                        Id = book.Id,
                        Title = book.Title,
                        Categories = book.BookCategories.Select(c => new GetBookModelCategory{ Id = c.Category.Id, Name = c.Category.Name}).ToList()
                    });
                    result.ShouldHaveValidationErrorFor(x => x);
                }
            }
        }
    }
}