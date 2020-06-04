using Library.Domain.Entities;
using Library.Domain.Enums;
using Xunit;

namespace Library.Domain.Tests
{
    public class CategoryTests
    {
        private readonly string _categoryName;

        public CategoryTests()
        {
            _categoryName = "new";
        }

        [Fact]
        public void Create_Category()
        {
            var category = new Category(_categoryName);
            Assert.Equal(_categoryName, category.Name);
            Assert.Equal(Status.Added, category.Status);
        }

        [Fact]
        public void Update_Category()
        {
            const string newCategoryName = "old";
            var category = new Category(_categoryName);
            category.UpdateCategory(newCategoryName);

            Assert.Equal(newCategoryName, category.Name);
            Assert.Equal(Status.Updated, category.Status);
        }

        [Fact]
        public void Remove_Category()
        {
            var category = new Category(_categoryName);
            category.RemoveCategory();

            Assert.Equal(Status.Deleted, category.Status);
        }
    }
}