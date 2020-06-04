using FluentValidation.TestHelper;
using Library.Application.Category.Commands.DeleteCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Categories.Commands
{
    public class DeleteCategoryCommandTest : TestBase
    {

        [Fact]
        public async Task Delete_Category()
        {
            using (var context = GetContextWithData())
            {
                var handler = new DeleteCategoryCommandHandler(context);
                var command = new DeleteCategoryCommand
                {
                    Id = (await context.Categories.Skip(6).Take(1).FirstOrDefaultAsync()).Id
                };

                await handler.Handle(command, CancellationToken.None);
                Assert.Null(await context.Categories.FindAsync(command.Id));
            }
        }

        [Fact]
        public void Delete_Category_With_No_Id_Throws_Exception()
        {
            var validator = new DeleteCategoryCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }
    }
}