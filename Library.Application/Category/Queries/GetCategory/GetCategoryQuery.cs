using Library.Common;
using Library.Common.Models.Category;
using MediatR;

namespace Library.Application.Category.Queries.GetCategory
{
    public class GetCategoryQuery : BaseItem, IRequest<GetCategoryModel>
    {
    }
}