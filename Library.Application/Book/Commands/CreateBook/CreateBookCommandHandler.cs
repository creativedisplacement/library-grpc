using Library.Common.Models.Book;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Book.Commands.CreateBook
{
    public class CreateBookCommandHandler : BaseCommandHandler, IRequestHandler<CreateBookCommand, GetBookModel>
    {
        private readonly LibraryDbContext _context;

        public CreateBookCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GetBookModel> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        { 
            var bookCategories = await _context.Categories.Where(bc => request.Categories.Select(c => c.Id).Contains(bc.Id))
                .Select(c => new BookCategory { CategoryId = c.Id, Category = c }).ToListAsync(cancellationToken);

            var book = new Domain.Entities.Book(request.Title, bookCategories);
            SetDomainState(book);
            await _context.SaveChangesAsync(cancellationToken);

            return new GetBookModel
            {
                Id = book.Id,
                Title = book.Title,
                Categories = book.BookCategories.Select(c => new GetBookModelCategory { Id = c.CategoryId, Name = c.Category.Name }).ToList()
            };
        }
    }
}