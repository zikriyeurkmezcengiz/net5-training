using System;
using System.Linq;
using BookStoreWebApi.Common;
using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreWebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                // Look for any board games.
                if (context.Books.Any())
                {
                    return;   // Data was already seeded
                }

                context.Books.AddRange(
                   new Book()
                   {
                       Title = "Lean Startup",
                       GenreId = (int)GenreEnum.PersonalGrowth, // Personal Growth
                       PageCount = 200,
                       PublishDate = new DateTime(2001, 06, 12)
                   },
                    new Book()
                    {
                        Title = "Herland",
                        GenreId = (int)GenreEnum.ScienceFiction, // Science Fiction
                        PageCount = 250,
                        PublishDate = new DateTime(2002, 06, 12)
                    },
                    new Book()
                    {
                        Title = "Dune",
                        GenreId = (int)GenreEnum.ScienceFiction, // Science Fiction
                        PageCount = 540,
                        PublishDate = new DateTime(2002, 05, 23)
                    });

                context.SaveChanges();
            }
        }
    }
}