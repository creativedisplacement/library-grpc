using Library.Common;
using Library.Common.Models.Person;
using MediatR;

namespace Library.Application.People.Queries.GetPerson
{
    public class GetPersonQuery : BaseItem, IRequest<GetPersonModel>
    {
    }
}