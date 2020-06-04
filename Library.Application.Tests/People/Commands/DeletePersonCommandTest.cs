using FluentValidation.TestHelper;
using Library.Application.People.Commands.DeletePerson;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.People.Commands
{
    public class DeletePersonCommandTest : TestBase
    {
        [Fact]
        public async Task Delete_Person()
        {
            using (var context = GetContextWithData())
            {
                var handler = new DeletePersonCommandHandler(context);
                var command = new DeletePersonCommand
                {
                    Id = (await context.Persons.Skip(1).Take(1).FirstOrDefaultAsync()).Id
                };

                await handler.Handle(command, CancellationToken.None);
                Assert.Null(await context.Persons.FindAsync(command.Id));
            }
        }

        [Fact]
        public void Delete_Person_With_No_Id_Throws_Exception()
        {
            var validator = new DeletePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }
    }
}