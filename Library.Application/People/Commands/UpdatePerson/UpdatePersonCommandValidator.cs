using FluentValidation;
using Library.Persistence;

namespace Library.Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommandValidator : BasePersonCommandValidator<UpdatePersonCommand>
    {

        public UpdatePersonCommandValidator(LibraryDbContext context) : base(context)
        {
            RuleFor(x => x.Id).NotEmpty();
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