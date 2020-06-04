using FluentValidation;

namespace Library.Application.Book.Commands.LendBook
{
    public class LendBookCommandValidator : AbstractValidator<LendBookCommand>
    {
        public LendBookCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.LenderId).NotEmpty();
        }
    }
}