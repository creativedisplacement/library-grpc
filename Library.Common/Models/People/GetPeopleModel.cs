using Library.Common.Models.Person;
using System.Collections.Generic;

namespace Library.Common.Models.People
{
    public class GetPeopleModel
    {
        public IEnumerable<GetPersonModel> People { get; set; }
    }
}