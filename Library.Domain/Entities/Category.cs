using System.Collections.Generic;
using Library.Domain.Entities.Abstract;
using Library.Domain.Enums;

namespace Library.Domain.Entities
{
    public class Category : BaseEntity, IAggregateRoot
    {
        public Category(string name)
        {
            Name = name;
            Status = Status.Added;
        }

        public string Name { get; private set; }
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();

        public void UpdateCategory(string name)
        {
            Name = name;
            Status = Status.Updated;
        }

        public void RemoveCategory()
        {
            Status = Status.Deleted;
        }
    }
}