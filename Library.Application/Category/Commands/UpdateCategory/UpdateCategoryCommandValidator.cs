using FluentValidation;
using Library.Persistence;

namespace Library.Application.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : BaseCategoryCommandValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(LibraryDbContext context) : base(context)
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(x => x).Must(category => CategoryNameExists(category.Name, category.Id))
                .WithMessage("The name for this category already exists in the database.");
        }
    }
}