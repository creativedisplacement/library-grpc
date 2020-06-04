using Library.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence
{
    public class LibraryDbContextFactory: DesignTimeDbContextFactoryBase<LibraryDbContext>
    {
        protected override LibraryDbContext CreateNewInstance(DbContextOptions<LibraryDbContext> options)
        {
            return new LibraryDbContext(options);
        }
    }
}