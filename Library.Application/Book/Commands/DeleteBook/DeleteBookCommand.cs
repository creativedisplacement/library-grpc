using MediatR;
using System;

namespace Library.Application.Book.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}