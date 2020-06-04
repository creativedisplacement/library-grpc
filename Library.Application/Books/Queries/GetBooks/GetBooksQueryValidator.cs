using FluentValidation;
using Library.Application.Book.Queries.GetBook;

namespace Library.Application.Books.Queries.GetBooks
{
    public class GetBooksQueryValidator : AbstractValidator<GetBookQuery>
    {
        public GetBooksQueryValidator()
        {
            //RuleFor(v => v.Id).NotEmpty();
            //RuleFor(v => v.Id).NotEmpty();
        }
    }
}