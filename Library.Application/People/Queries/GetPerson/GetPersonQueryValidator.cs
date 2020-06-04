using FluentValidation;
using Library.Common.Models.Person;

namespace Library.Application.People.Queries.GetPerson
{
    public class GetPersonQueryValidator : AbstractValidator<GetPersonModel>
    {
        public GetPersonQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}