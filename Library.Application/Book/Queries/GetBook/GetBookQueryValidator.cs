using FluentValidation;

namespace Library.Application.Book.Queries.GetBook
{
    public class GetBookQueryValidator : AbstractValidator<GetBookQuery>
    {
        public GetBookQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}