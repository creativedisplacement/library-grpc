using Library.Common.Models.Person;

namespace Library.Common.Models.Book
{
    public class ReturnBookModel : GetBookModel
    {
        public GetPersonModel Lender { get; set; }
    }
}
