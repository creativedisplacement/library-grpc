using Library.Common;
using Library.Common.Models.Book;
using MediatR;

namespace Library.Application.Book.Queries.GetBook
{
    public class GetBookQuery : BaseTitleItem, IRequest<GetBookModel>
    {
    }
}