using Library.Application.Exceptions;
using Library.Common.Models.Book;
using Library.Common.Models.Person;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Book.Commands.LendBook
{
    public class LendBookCommandHandler : BaseCommandHandler, IRequestHandler<LendBookCommand, LendBookModel>
    {
        private readonly LibraryDbContext _context;

        public LendBookCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LendBookModel> Handle(LendBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .Include(i => i.Lender)
                .Include(i => i.BookCategories)
                .ThenInclude(i => i.Category)
                .SingleAsync(c => c.Id == request.Id, cancellationToken);
            var lender = await _context.Persons.SingleAsync(c => c.Id == request.LenderId, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            if (lender == null)
            {
                throw new NotFoundException(nameof(Person), request.LenderId);
            }

            book.LendBook(lender);
            SetDomainState(book);
            await _context.SaveChangesAsync(cancellationToken);

            return new LendBookModel
            {
                Id = book.Id,
                Title = book.Title,
                Categories = book.BookCategories.Select(c => new GetBookModelCategory
                {
                    Id = c.CategoryId, 
                    Name = c.Category.Name
                }).ToList(),
                Lender = new GetPersonModel
                {
                    Id = book.Lender.Id,
                    Name = book.Lender.Name,
                    Email = book.Lender.Email,
                    IsAdmin = book.Lender.IsAdmin
                }
            };
        }
    }
}