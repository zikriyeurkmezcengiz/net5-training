using System;
using System.Linq;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly BookStoreDbContext _context;
        public CreateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(CreateBookModel model)
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);

            if (book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut.");

            book = new Book();
            book.Title = model.Title;
            book.PublishDate = model.PublishDate;
            book.PageCount = model.PageCount;
             book.GenreId = model.GenreId;

            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}