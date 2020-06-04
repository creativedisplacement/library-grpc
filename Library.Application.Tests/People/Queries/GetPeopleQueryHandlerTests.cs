using Library.Application.People.Queries.GetPeople;
using Library.Common.Models.People;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.People.Queries
{
    public class GetPeopleQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task Get_All_People()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetPeopleQueryHandler(context);
                var result = await handler.Handle(new GetPeopleQuery(), CancellationToken.None);

                Assert.IsType<GetPeopleModel>(result);
                Assert.Equal(context.Persons.Count(), result.People.Count());
            }
        }

        [Fact]
        public async Task Get_People_By_Name()
        {
            using (var context = GetContextWithData())
            {
                const string name = "Victor";
                var handler = new GetPeopleQueryHandler(context);
                var result = await handler.Handle(new GetPeopleQuery {Name = name}, CancellationToken.None);

                Assert.IsType<GetPeopleModel>(result);

                var person = context.Persons.First(p => p.Name == name);
                Assert.Equal(person.Name, result.People.First().Name);
                Assert.Equal(person.Email, result.People.First().Email);
            }
        }

        [Fact]
        public async Task Get_People_By_Email()
        {
            using (var context = GetContextWithData())
            {
                const string email = "v@v.com";

                var handler = new GetPeopleQueryHandler(context);
                var result = await handler.Handle(new GetPeopleQuery {Email = email},
                    CancellationToken.None);

                Assert.IsType<GetPeopleModel>(result);
                var person = context.Persons.First(p => p.Email == email);
                Assert.Equal(person.Name, result.People.First().Name);
                Assert.Equal(person.Email, result.People.First().Email);
            }
        }

        [Fact]
        public async Task Get_People_That_Are_Admins()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetPeopleQueryHandler(context);
                var result = await handler.Handle(new GetPeopleQuery {IsAdmin = true}, CancellationToken.None);

                Assert.IsType<GetPeopleModel>(result);
                Assert.Equal(context.Persons.Count(p => p.IsAdmin.Value), result.People.Count());
                Assert.Equal(context.Persons.First(p => p.IsAdmin.Value).Name, result.People.First().Name);
            }
        }

        [Fact]
        public async Task Get_People_That_Are_Not_Admins()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetPeopleQueryHandler(context);
                var result = await handler.Handle(new GetPeopleQuery {IsAdmin = false}, CancellationToken.None);

                Assert.IsType<GetPeopleModel>(result);
                Assert.Equal(context.Persons.Count(p => !p.IsAdmin.Value), result.People.Count());
                Assert.Equal(context.Persons.First(p => !p.IsAdmin.Value).Name, result.People.First().Name);
            }
        }
    }
}