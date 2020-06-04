using Library.Common;
using Library.Common.Models.Book;
using MediatR;
using System.Collections.Generic;

namespace Library.Application.Book.Commands.UpdateBook
{
    public class UpdateBookCommand : BaseTitleItem, IRequest<GetBookModel>
    {
        public ICollection<GetBookModelCategory> Categories { get; set; }
    }
}