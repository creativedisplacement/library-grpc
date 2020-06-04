using Library.Application.Categories.Queries.GetCategories;
using Library.Common.Models.Categories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Categories.Queries
{
    public class GetCategoriesQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task Get_Categories()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetCategoriesQueryHandler(context);
                var result = await handler.Handle(new GetCategoriesQuery(), CancellationToken.None);

                Assert.IsType<GetCategoriesModel>(result);
                Assert.Equal(context.Categories.Count(), result.Categories.Count());
            }
        }
    }
}