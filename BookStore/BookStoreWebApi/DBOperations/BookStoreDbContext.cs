using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;

namespace BookStoreWebApi.DBOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     base.OnModelCreating(builder);
        //     builder.Entity<Book>(book =>
        //     {
        //         var bookId = book.Property(p => p.Id);
        //         bookId.ValueGeneratedOnAdd();
        //         // only for in-memory
        //         if (Database.IsInMemory())
        //             bookId.HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
        //     });
        // }
    }
}