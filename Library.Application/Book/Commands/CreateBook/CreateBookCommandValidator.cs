using FluentValidation;
using Library.Persistence;

namespace Library.Application.Book.Commands.CreateBook
{
    public class CreateBookCommandValidator : BaseBookCommandValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator(LibraryDbContext context) : base(context)
        {
            RuleFor(x => x.Title)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(x => x.Categories).NotEmpty();

            RuleFor(x => x).Must(book => BookTitleExists(book.Title))
                .WithMessage("The title for this book already exists in the database.");
        }
    }
}