using Library.Common;
using Library.Common.Models.Book;
using MediatR;
using System;

namespace Library.Application.Book.Commands.LendBook
{
    public class LendBookCommand : BaseItem, IRequest<LendBookModel>
    {
        public Guid LenderId { get; set; }
    }
}