using Library.Common;
using Library.Common.Models.Book;
using MediatR;

namespace Library.Application.Book.Commands.ReturnBook
{
    public class ReturnBookCommand : BaseItem, IRequest<ReturnBookModel>
    {
    }
}