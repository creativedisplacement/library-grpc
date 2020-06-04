using System.Collections.Generic;

namespace Library.Common.Models.Book
{
    public class CreateBookModel : BaseTitleItem
    {
        public virtual ICollection<CreateBookModelCategory> Categories { get; set; } = new List<CreateBookModelCategory>();
    }

    public class CreateBookModelCategory : BaseNameItem
    {
    }
}