using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Configurations
{
    public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(bc => new { bc.BookId, bc.CategoryId });

            builder.HasOne(bc => bc.Category)
                .WithMany(b => b.)
                .HasForeignKey(bc => bc.BookId);

            builder.HasOne(bc => bc.Book)
                .WithMany(c => c.Categories)
                .HasForeignKey(bc => bc.CategoryId);
        }
    }
}