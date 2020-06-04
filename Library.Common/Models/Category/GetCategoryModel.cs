using System;
using System.Collections.Generic;

namespace Library.Common.Models.Category
{
    public class GetCategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<GetCategoryBookModel> Books { get; set; } = new List<GetCategoryBookModel>();
    }

    public class GetCategoryBookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
    }
}