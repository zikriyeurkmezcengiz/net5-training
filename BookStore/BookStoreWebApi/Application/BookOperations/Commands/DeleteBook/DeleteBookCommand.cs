using System;
using System.Linq;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.Application.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }

        private readonly IBookStoreDbContext _context;
        public DeleteBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == BookId);

            if (book is null)
                throw new InvalidOperationException("Kitap Bulunamadı");

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}