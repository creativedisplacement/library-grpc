using Library.Application.Category.Queries.GetCategory;
using Library.Common.Models.Category;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Categories.Queries
{
    public class GetCategoryQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task Get_Category()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetCategoryQueryHandler(context);
                var category = context.Categories.First();

                var result = await handler.Handle(new GetCategoryQuery {Id = category.Id}, CancellationToken.None);

                Assert.IsType<GetCategoryModel>(result);
                Assert.Equal(result.Id, category.Id);
                Assert.Equal(result.Name, category.Name);
            }
        }
    }
}