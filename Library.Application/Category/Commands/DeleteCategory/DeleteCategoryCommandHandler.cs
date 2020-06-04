using Library.Application.Exceptions;
using Library.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Category.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : BaseCommandHandler, IRequestHandler<DeleteCategoryCommand>
    {
        private readonly LibraryDbContext _context;

        public DeleteCategoryCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            var categoryInUse = _context.Books.Any(b => b.BookCategories.Any(c => c.CategoryId == request.Id));
            if (categoryInUse)
            {
                throw new DeleteFailureException(nameof(Category), request.Id, "There are existing books associated with this category.");
            }
            category.RemoveCategory();
            SetDomainState(category);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}