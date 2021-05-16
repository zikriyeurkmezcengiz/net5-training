
using Linq_Practices.Entities;
using Microsoft.EntityFrameworkCore;

namespace Linq_Practices.DBOperations
{
    public class LinqDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "LinqDatabase");
        }
    }
}