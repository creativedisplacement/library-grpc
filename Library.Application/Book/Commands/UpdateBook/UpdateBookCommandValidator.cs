using FluentValidation;
using Library.Persistence;

namespace Library.Application.Book.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : BaseBookCommandValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator(LibraryDbContext context) : base (context)
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(x => x.Categories).NotEmpty();

            RuleFor(x => x).Must(book => BookTitleExists(book.Title, book.Id))
                .WithMessage("The title for this book already exists in the database.");
        }
    }
}