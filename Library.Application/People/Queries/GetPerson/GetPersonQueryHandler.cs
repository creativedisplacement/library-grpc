using Library.Application.Exceptions;
using Library.Common.Models.Person;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.People.Queries.GetPerson
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, GetPersonModel>
    {
        private readonly LibraryDbContext _context;

        public GetPersonQueryHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<GetPersonModel> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons
                .Include(i => i.Books)
                .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (person == null)
            {
                throw new NotFoundException(nameof(Person), request.Id);
            }

            return new GetPersonModel
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                IsAdmin = person.IsAdmin,
                Books = person.Books.Select(b => new GetPersonBookModel { Id = b.Id }).ToList()
            };
        }
    }
}