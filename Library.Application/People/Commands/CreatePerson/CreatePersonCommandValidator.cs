using FluentValidation;
using Library.Persistence;

namespace Library.Application.People.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : BasePersonCommandValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator(LibraryDbContext context) : base(context)
        {
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(20)
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email address cannot be empty")
                .EmailAddress()
                .WithMessage("Email address must be valid");

            RuleFor(x => x).Must(person => NameExists(person.Name, person.Id))
                .WithMessage("The name for this person already exists in the database.");

            RuleFor(x => x).Must(person => EmailAddressExists(person.Email, person.Id))
                .WithMessage("The email address for this person already exists in the database.");
        }
    }
}