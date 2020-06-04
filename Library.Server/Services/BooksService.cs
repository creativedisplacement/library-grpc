using Grpc.Core;
using Library.Application.Books.Queries.GetBooks;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Server.Services
{
    public class BooksService :  Books.BooksBase
    {
        private readonly ILogger<BooksService> _logger;
        private readonly IMediator _mediator;
        public BooksService(ILogger<BooksService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override async Task<BooksReply> GetBooks(GetBooksRequest request, ServerCallContext context)
        {
            var books = await _mediator.Send(new GetBooksQuery
                {
                    Title = request.Title,
                    CategoryIds = request.CategoryIds.Count > 0 ? request.CategoryIds.Select(s => new Guid(s)).ToList() : new List<Guid>{},
                    LenderId = !string.IsNullOrEmpty(request.LenderId) ? new Guid(request.LenderId) : Guid.Empty,
                    IsAvailable = request.IsAvailable
                });

                var bookReplyList = new List<BookReply>();

                foreach (var book in books.Books)
                {
                    var bookReply = new BookReply
                    {
                        Id = book.Id.ToString(),
                        Title = book.Title,
                        Lender = null
                    };
                    bookReply.CategoryIds.Add(book.Categories.Select(s => new CategoryReply{ Id = s.Id.ToString(), Name = s.Name})); 
                    bookReplyList.Add(bookReply);
                }

                var booksReply = new BooksReply();
                booksReply.Books.AddRange(bookReplyList);
                return booksReply;
        }
    }
}
