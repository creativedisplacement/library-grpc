using Library.Application.Exceptions;
using Library.Common.Models.Person;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler : BaseCommandHandler, IRequestHandler<UpdatePersonCommand, GetPersonModel>
    {
        private readonly LibraryDbContext _context;

        public UpdatePersonCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GetPersonModel> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.SingleAsync(c => c.Id == request.Id, cancellationToken);

            if (person == null)
            {
                throw new NotFoundException(nameof(Person), request.Id);
            }

            person.UpdatePerson(request.Name, request.Email, request.IsAdmin);
            SetDomainState(person);

            await _context.SaveChangesAsync(cancellationToken);

           return new GetPersonModel
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                IsAdmin = person.IsAdmin
            };
        }
    }
}