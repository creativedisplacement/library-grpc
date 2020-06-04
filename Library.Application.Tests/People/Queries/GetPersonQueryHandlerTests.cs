using Library.Application.People.Queries.GetPerson;
using Library.Common.Models.Person;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.People.Queries
{
    public class GetPersonQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task Get_Person()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetPersonQueryHandler(context);
                var person = context.Persons.First();

                var result = await handler.Handle(new GetPersonQuery {Id = person.Id}, CancellationToken.None);

                Assert.IsType<GetPersonModel>(result);
                Assert.Equal(result.Id, person.Id);
                Assert.Equal(result.Name, person.Name);
                Assert.Equal(result.Email, person.Email);
                Assert.Equal(result.IsAdmin, person.IsAdmin);
            }
        }
    }
}