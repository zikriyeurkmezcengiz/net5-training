using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut.");

            book = _mapper.Map<Book>(Model);
            // book = new Book();
            // book.Title = Model.Title;
            // book.PublishDate = Model.PublishDate;
            // book.PageCount = Model.PageCount;
            // book.GenreId = Model.GenreId;

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