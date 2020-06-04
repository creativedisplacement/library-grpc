using Library.Common;
using Library.Common.Models.Category;
using MediatR;

namespace Library.Application.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : BaseNameItem, IRequest<GetCategoryModel>
    {
    }
}