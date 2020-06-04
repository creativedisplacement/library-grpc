using Library.Common.Models.Categories;
using MediatR;

namespace Library.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<GetCategoriesModel>
    {
    }
}