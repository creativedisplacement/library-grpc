using FluentValidation.TestHelper;
using Library.Application.Category.Commands.UpdateCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Categories.Commands
{
    public class UpdateCategoryCommandTest : TestBase
    {
        [Fact]
        public async Task Update_Category()
        {
            using (var context = GetContextWithData())
            {
                var handler = new UpdateCategoryCommandHandler(context);
                var command = new UpdateCategoryCommand
                {
                    Id = (await context.Categories.FirstOrDefaultAsync()).Id,
                    Name = "Test2"
                };

                await handler.Handle(command, CancellationToken.None);
                Assert.Equal(command.Name, (await context.Categories.FindAsync(command.Id)).Name);
            }
        }

        [Fact]
        public void Update_Category_With_No_Id_Throws_Exception()
        {
            using (var context = GetContextWithData())
            {
                var validator = new UpdateCategoryCommandValidator(context);
                validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
            }
        }

        [Fact]
        public void Update_Category_With_No_Name_Throws_Exception()
        {
            using (var context = GetContextWithData())
            {
                var validator = new UpdateCategoryCommandValidator(context);
                validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
            }
        }

        [Fact]
        public void Update_Category_With_Title_That_Already_Exists_Throws_Exception()
        {
            using (var context = GetContextWithData())
            {
                var category = context.Categories.FirstOrDefault(b => b.Name == "Action");

                if (category != null)
                {
                    category.UpdateCategory("Technical");

                    var validator = new UpdateCategoryCommandValidator(context);
                    var result = validator.TestValidate(new UpdateCategoryCommand
                    {
                       Id = category.Id,
                       Name = category.Name
                    });
                    result.ShouldHaveValidationErrorFor(x => x);
                }
            }
        }
    }
}