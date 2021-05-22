using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.Common;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _context.Books.OrderBy(x => x.Id);

            List<BooksViewModel> obj = _mapper.Map<List<BooksViewModel>>(bookList);
            // List<BooksViewModel> vm = new List<BooksViewModel>();

            // foreach (var item in bookList)
            // {
            //     vm.Add(new BooksViewModel()
            //     {
            //         Title = item.Title,
            //         Genre = ((GenreEnum)item.GenreId).ToString(),
            //         PageCount = item.PageCount,
            //         PublishDate = item.PublishDate.Date.ToString("dd/MM/yyyy")
            //     });
            // }

            return obj;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}