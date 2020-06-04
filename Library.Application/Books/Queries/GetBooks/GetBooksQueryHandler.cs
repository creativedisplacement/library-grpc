using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Models.Books;

namespace Library.Application.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, GetBooksModel>
    {
        private readonly LibraryDbContext _context;

        public GetBooksQueryHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<GetBooksModel> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            //TODO: IQueryable call seems to fail on the isavailable step, changed to IList for now and need to investigate further
            IEnumerable<Domain.Entities.Book> books = await _context.Books
                .Include(i => i.BookCategories)
                .ThenInclude(c => c.Category)
                .ToListAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Title))
            {
                books = books.Where(b => b.Title.Contains(request.Title));
            }

            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                books = books.Where(b =>
                    b.BookCategories.Any(bc => request.CategoryIds.Any(c => bc.CategoryId == c)));
            }

            if (request.LenderId != Guid.Empty)
            {
                books = books.Where(b => b.Lender != null && b.Lender.Id == request.LenderId);
            }

            if (request.IsAvailable.HasValue)
            {
                books = request.IsAvailable.Value ? books.Where(bb => bb.IsAvailable) : books.Where(bb => bb.IsAvailable == false);
            }

            return new GetBooksModel
            {
                Books = books
                    .Select(b => new GetBookModel {Id = b.Id, Title = b.Title, Categories = b.BookCategories.Select(c => new GetBookModelCategory{ Id = c.CategoryId, Name = c.Category.Name})})
                    .OrderBy(b => b.Title)
                    .ToList()
            };
        }
    }
}