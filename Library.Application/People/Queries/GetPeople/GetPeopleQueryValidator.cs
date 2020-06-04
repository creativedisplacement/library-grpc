using FluentValidation;
using Library.Application.Category.Queries.GetCategory;

namespace Library.Application.People.Queries.GetPeople
{
    public class GetPeopleQueryValidator : AbstractValidator<GetCategoryQuery>
    {
        public GetPeopleQueryValidator()
        {
            //RuleFor(v => v.Id).NotEmpty();
        }
    }
}