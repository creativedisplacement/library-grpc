using Library.Common.Models.Person;

namespace Library.Common.Models.Book
{
    public class LendBookModel : GetBookModel
    {
        public GetPersonModel Lender { get; set; }
    }
}
