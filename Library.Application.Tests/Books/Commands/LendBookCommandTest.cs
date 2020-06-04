using FluentValidation.TestHelper;
using Library.Application.Book.Commands.LendBook;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Commands
{
    public class LendBookCommandTest : TestBase
    {
        [Fact]
        public async Task Lend_Book()
        {
            using (var context = GetContextWithData())
            {
                var handler = new LendBookCommandHandler(context);
                var command = new LendBookCommand
                {
                    Id = (await context.Books.FirstOrDefaultAsync()).Id,
                    LenderId = (await context.Persons.FirstOrDefaultAsync()).Id
                };

                await handler.Handle(command, CancellationToken.None);

                var book = await context.Books.FindAsync(command.Id);

                Assert.NotNull(book.Lender);
            }
        }

        [Fact]
        public void Lend_Book_With_No_Id_Throws_Exception()
        {
            var validator = new LendBookCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }

        [Fact]
        public void Lend_Book_With_No_Lender_Id_Throws_Exception()
        {
            var validator = new LendBookCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.LenderId, Guid.Empty);
        }
    }
}