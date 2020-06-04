using Library.Common;
using Library.Common.Models.Category;
using MediatR;

namespace Library.Application.Category.Commands.CreateCategory
{
    public class CreateCategoryCommand : BaseNameItem, IRequest<GetCategoryModel>
    {
    }
}