using Library.Application.Exceptions;
using Library.Common.Models.Book;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Book.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : BaseCommandHandler, IRequestHandler<UpdateBookCommand, GetBookModel>
    {
        private readonly LibraryDbContext _context;

        public UpdateBookCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GetBookModel> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .Include(c => c.BookCategories)
                .SingleAsync(c => c.Id == request.Id, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            var bookCategories = await _context.Categories.Where(bc => request.Categories.Select(c => c.Id).Contains(bc.Id))
                .Select(c => new BookCategory { CategoryId = c.Id, Category = c, BookId = book.Id}).ToListAsync(cancellationToken);

            if (book.BookCategories.Any())
            {
                book.RemoveCategories();
                SetDomainState(book);

                await _context.SaveChangesAsync(cancellationToken);
            }

            book.UpdateBook(request.Title, bookCategories);
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