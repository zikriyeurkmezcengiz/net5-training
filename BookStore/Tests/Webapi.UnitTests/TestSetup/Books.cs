using System;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(new Book[]{
                new Book(){Title = "Lean Startup",GenreId = 1,PageCount = 200,PublishDate = new DateTime(2001, 06, 12)},
                new Book(){Title = "Herland",GenreId = 2, PageCount = 250, PublishDate = new DateTime(2002, 06, 12)},
                new Book(){ Title = "Dune", GenreId = 2, PageCount = 540, PublishDate = new DateTime(2002, 05, 23)}
            });
        }
    }
}