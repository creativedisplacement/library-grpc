using System.Collections.Generic;
using Library.Domain.Entities.Abstract;
using Library.Domain.Enums;

namespace Library.Domain.Entities
{
    public class Person : BaseEntity, IAggregateRoot
    {

        public Person(string name, string email, bool? isAdmin)
        {
            Name = name;
            Email = email;
            IsAdmin = isAdmin;
            Status = Status.Added;
        }


        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool? IsAdmin { get; private set; }
        public ICollection<Book> Books { get; set; }

        public void UpdatePerson(string name, string email, bool? isAdmin)
        {
            Name = name;
            Email = email;
            IsAdmin = isAdmin;
            Status = Status.Updated;
        }

        public void RemovePerson()
        {
            Status = Status.Deleted;
        }

        public void UpdateName(string name)
        {
            Name = name;
            Status = Status.Updated;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
            Status = Status.Updated;
        }

        public void GiveAdminPermissions()
        {
            IsAdmin = true;
            Status = Status.Updated;
        }

        public void RemoveAdminPermissions()
        {
            IsAdmin = false;
            Status = Status.Updated;
        }
    }
}