using System.Collections.Generic;

namespace Library.Common.Models.Person
{
    public class GetPersonModel : BasePersonItem
    {
        public ICollection<GetPersonBookModel> Books { get; set; }
    }

    public class GetPersonBookModel : BasePersonItem
    {
        public new bool? IsAdmin { get; set; }
    }
}