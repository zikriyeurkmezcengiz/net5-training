using System;
using System.Collections.Generic;
using System.Linq;
using BookStoreWebApi.Common;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _context;
        public GetBookDetailQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Find(BookId);

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı.");

            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");

            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}