using System.Collections.Generic;

namespace Library.Common.Models.Book
{
    public class GetBookModel : BaseTitleItem
    {
        public virtual ICollection<GetBookModelCategory> Categories { get; set; }
    }

    public class GetBookModelCategory : BaseNameItem
    { 
    }
}