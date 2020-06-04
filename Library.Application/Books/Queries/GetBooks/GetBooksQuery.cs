using Library.Common;
using MediatR;
using System;
using System.Collections.Generic;
using Library.Common.Models.Books;

namespace Library.Application.Books.Queries.GetBooks
{
    public class GetBooksQuery : BaseTitleItem, IRequest<GetBooksModel>
    {
        public ICollection<Guid> CategoryIds { get; set; } = new List<Guid>();
        public Guid LenderId { get; set; }
        public bool? IsAvailable { get; set; }
    }
}