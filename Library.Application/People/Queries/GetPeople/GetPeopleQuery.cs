using Library.Common;
using Library.Common.Models.People;
using MediatR;

namespace Library.Application.People.Queries.GetPeople
{
    public class GetPeopleQuery : BasePersonItem, IRequest<GetPeopleModel>
    {
    }
}