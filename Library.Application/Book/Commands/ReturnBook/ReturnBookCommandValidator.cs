using FluentValidation;

namespace Library.Application.Book.Commands.ReturnBook
{
    public class ReturnBookCommandValidator : AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}