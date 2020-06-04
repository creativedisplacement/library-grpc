using System;
using System.Collections.Generic;

namespace Library.Common.Models.Categories
{
    public class GetCategoriesModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
    }

    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}