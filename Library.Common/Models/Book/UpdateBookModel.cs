using System.Collections.Generic;

namespace Library.Common.Models.Book
{
    public class UpdateBookModel : BaseTitleItem
    {
        public virtual ICollection<UpdateBookModelCategory> Categories { get; set; } = new List<UpdateBookModelCategory>();
    }

    public class UpdateBookModelCategory : BaseNameItem
    {
    }
}