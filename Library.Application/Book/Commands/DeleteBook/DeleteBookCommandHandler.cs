using Library.Application.Exceptions;
using Library.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Book.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : BaseCommandHandler, IRequestHandler<DeleteBookCommand>
    {
        private readonly LibraryDbContext _context;

        public DeleteBookCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.Id);

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            if (!book.IsAvailable)
            {
                throw new DeleteFailureException(nameof(Book), request.Id, "This book has been lent to someone and cannot be deleted.");
            }
            book.RemoveBook();
            SetDomainState(book);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}