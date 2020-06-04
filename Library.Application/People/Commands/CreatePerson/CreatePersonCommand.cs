using Library.Common;
using Library.Common.Models.Person;
using MediatR;

namespace Library.Application.People.Commands.CreatePerson
{
    public class CreatePersonCommand : BasePersonItem, IRequest<GetPersonModel>
    {
    }
}