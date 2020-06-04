using FluentValidation.TestHelper;
using Library.Application.Category.Commands.CreateCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Categories.Commands
{
    public class CreateCategoryCommandTest : TestBase
    {
        [Fact]
        public async Task Create_New_Category()
        {
            using (var context = GetContextWithData())
            {
                var handler = new CreateCategoryCommandHandler(context);
                var command = new CreateCategoryCommand
                {
                    Name = "Test1"
                };

                await handler.Handle(command, CancellationToken.None);
                var category = await context.Categories.SingleOrDefaultAsync(c => c.Name == command.Name);

                Assert.Equal(command.Name, category.Name);
            }
        }

        [Fact]
        public void Create_Category_With_No_Name_Throws_Exception()
        {
            using (var context = GetContextWithData())
            {
                var validator = new CreateCategoryCommandValidator(context);
                validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
            }
        }

        [Fact]
        public void Create_Category_With_Name_That_Already_Exists_Throws_Exception()
        {
            using (var context = GetContextWithData())
            {
                var validator = new CreateCategoryCommandValidator(context);
                var result = validator.TestValidate(new CreateCategoryCommand
                {
                   Id = new Guid(),
                   Name = context.Categories.FirstOrDefault()?.Name
                });
                result.ShouldHaveValidationErrorFor(x => x);
            }
        }

        [Fact]
        public void Create_Category_With_Name_That_Does_Not_Already_Exist()
        {
            using (var context = GetContextWithData())
            {
                var validator = new CreateCategoryCommandValidator(context);
                var result = validator.TestValidate(new CreateCategoryCommand
                {
                    Id = new Guid(),
                    Name = "New category"
                });
                result.ShouldNotHaveValidationErrorFor(x => x);
            }
        }
    }
}