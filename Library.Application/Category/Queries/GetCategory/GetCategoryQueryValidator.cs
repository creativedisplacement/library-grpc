using FluentValidation;

namespace Library.Application.Category.Queries.GetCategory
{
    public class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
    {
        public GetCategoryQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}