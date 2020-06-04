using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.People.Commands.DeletePerson
{
    public class DeletePersonCommandHandler : BaseCommandHandler, IRequestHandler<DeletePersonCommand>
    {
        private readonly LibraryDbContext _context;

        public DeletePersonCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.FindAsync(request.Id);

            if (person == null)
            {
                throw new NotFoundException(nameof(Person), request.Id);
            }

            var personHasBooks = _context.Books.Any(b => b.Lender.Id == request.Id);
            if (personHasBooks)
            {
                throw new DeleteFailureException(nameof(Person), request.Id, "This person has borrowed books and cannot be deleted.");
            }

            person.RemovePerson();
            SetDomainState(person);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}