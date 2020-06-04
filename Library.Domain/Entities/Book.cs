using Library.Domain.Entities.Abstract;
using Library.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Library.Domain.Entities
{
    public class Book : BaseEntity, IAggregateRoot
    {
        public Book()
        {
            
        }

        public Book(string title)
        {
            Title = title;
        }

        public Book(Guid id, string title, ICollection<BookCategory> categories)
        {
            Id = id;
            Title = title;
            BookCategories = categories;
            Status = Status.Added;
        }

        public Book(string title, ICollection<BookCategory> categories)
        {
            Title = title;
            BookCategories = categories;
            Status = Status.Added;
        }

        public Book(Guid id, string title, ICollection<BookCategory> categories, Person lender)
        {
            Id = id;
            Title = title;
            BookCategories = categories;
            Lender = lender;
            Status = Status.Added;
        }

        public Book(string title, ICollection<BookCategory> categories, Person lender)
        {
            Title = title;
            BookCategories = categories;
            Lender = lender;
            Status = Status.Added;
        }

        public string Title { get; private set; }
        public ICollection<BookCategory> BookCategories { get;  private set; } = new List<BookCategory>();
        public Person Lender { get; private set; }

        public bool IsAvailable => Lender == null;

        public void UpdateBook(string title, ICollection<BookCategory> categories)
        {
            Title = title;
            BookCategories = categories;
            Status = Status.Updated;
        }

        public void RemoveCategories()
        {
            BookCategories.Clear();
            Status = Status.Updated;
        }

        public void RemoveBook()
        {
            Status = Status.Deleted;
        }

        public void LendBook(Person lender)
        {
            Lender = lender;
            Status = Status.Updated;
        }

        public void ReturnBook()
        {
            Lender = null;
            Status = Status.Updated;
        }
    }
}