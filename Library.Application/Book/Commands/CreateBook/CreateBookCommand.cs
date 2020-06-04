using Library.Common;
using Library.Common.Models.Book;
using MediatR;
using System.Collections.Generic;

namespace Library.Application.Book.Commands.CreateBook
{
    public class CreateBookCommand : BaseTitleItem, IRequest<GetBookModel>
    {
        public ICollection<CreateBookModelCategory> Categories { get; set; }
    }
}