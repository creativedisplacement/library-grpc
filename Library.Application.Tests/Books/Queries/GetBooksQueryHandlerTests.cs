using Library.Application.Books.Queries.GetBooks;
using Library.Common.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Queries
{
    public class GetBooksQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task Get_All_Books()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetBooksQueryHandler(context);
                var result = await handler.Handle(new GetBooksQuery(), CancellationToken.None);

                Assert.IsType<GetBooksModel>(result);
                Assert.Equal(context.Books.Count(), result.Books.Count());
                Assert.Equal(context.Books.OrderBy(b => b.Title).First().Title,
                    result.Books.OrderBy(b => b.Title).First().Title);
            }
        }

        [Fact]
        public async Task Get_Books_By_Title()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetBooksQueryHandler(context);
                const string title = "Open";
                var result = await handler.Handle(new GetBooksQuery {Title = title}, CancellationToken.None);

                Assert.IsType<GetBooksModel>(result);
                Assert.Equal(context.Books.First(b => b.Title == title).Title, result.Books.First().Title);
            }
        }

        [Fact]
        public async Task Get_Books_By_Category()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetBooksQueryHandler(context);
                var categoryId = context.Categories.First().Id;
                var result = await handler.Handle(new GetBooksQuery {CategoryIds = new List<Guid> {categoryId}},
                    CancellationToken.None);

                Assert.IsType<GetBooksModel>(result);
                Assert.Equal(
                    context.BookCategories.Where(bc => bc.CategoryId == categoryId).Select(b => b.Book)
                        .OrderBy(b => b.Title).First().Title, result.Books.First().Title);
            }
        }

        [Fact]
        public async Task Get_Books_By_Lender()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetBooksQueryHandler(context);
                var lenderId = context.Persons.First(p => p.Name == "Hamed").Id;
                var result = await handler.Handle(new GetBooksQuery {LenderId = lenderId}, CancellationToken.None);

                Assert.IsType<GetBooksModel>(result);
                Assert.Equal(context.Books.First(b => b.Lender.Id == lenderId).Title, result.Books.First().Title);
            }
        }

        [Fact]
        public async Task Get_Books_By_Are_Available()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetBooksQueryHandler(context);
                var result = await handler.Handle(new GetBooksQuery {IsAvailable = true}, CancellationToken.None);

                Assert.IsType<GetBooksModel>(result);
                Assert.Equal(context.Books.OrderBy(b => b.Title).ToList().First(b => b.IsAvailable).Title, result.Books.First().Title);
            }
        }

        [Fact]
        public async Task Get_Books_By_Are_Not_Available()
        {
            using (var context = GetContextWithData())
            {
                var handler = new GetBooksQueryHandler(context);
                var result = await handler.Handle(new GetBooksQuery {IsAvailable = false}, CancellationToken.None);

                Assert.IsType<GetBooksModel>(result);
                Assert.Equal(context.Books.OrderBy(b => b.Title).ToList().First(b => !b.IsAvailable).Title, result.Books.First().Title);
            }
        }
    }
}