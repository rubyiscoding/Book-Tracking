using Microsoft.EntityFrameworkCore;
using BookTracker.Entities;
namespace BookTracker.Data

{
    public class BookTrackerContext : DbContext
    {
        public BookTrackerContext()
        {

        }
        public BookTrackerContext(DbContextOptions<BookTrackerContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryType> CategoryTypes { get; set; }
    }
}