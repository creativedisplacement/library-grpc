using Library.Application.Exceptions;
using Library.Common.Models.Book;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Book.Queries.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, GetBookModel>
    {
        private readonly LibraryDbContext _context;

        public GetBookQueryHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<GetBookModel> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .Include(i => i.BookCategories)
                .ThenInclude(c => c.Category)
                .SingleOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            return new GetBookModel
            {
                Id = book.Id,
                Title = book.Title,
                Categories = book.BookCategories.Select(c => new GetBookModelCategory{ Id = c.CategoryId, Name = c.Category.Name}).ToList()
            };
        }
    }
}