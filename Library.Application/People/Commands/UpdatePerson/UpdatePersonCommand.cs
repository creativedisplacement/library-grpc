using Library.Common;
using Library.Common.Models.Person;
using MediatR;

namespace Library.Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommand : BasePersonItem, IRequest<GetPersonModel>
    {
    }
}