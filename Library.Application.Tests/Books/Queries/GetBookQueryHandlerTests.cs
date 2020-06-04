using Library.Application.Book.Queries.GetBook;
using Library.Common.Models.Book;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Queries
{
    public class GetBookQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task Get_Book()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetBookQueryHandler(context);
                var book = context.Books.Include(i => i.BookCategories).First();

                var result = await handler.Handle(new GetBookQuery {Id = book.Id}, CancellationToken.None);

                Assert.IsType<GetBookModel>(result);
                Assert.Equal(result.Id, book.Id);
                Assert.Equal(result.Title, book.Title);
                Assert.Equal(result.Categories.Select(c => c.Name).OrderBy(c => c).ToList(),
                    book.BookCategories.Select(c => c.Category.Name).OrderBy(c => c).ToList());
            }
        }
    }
}