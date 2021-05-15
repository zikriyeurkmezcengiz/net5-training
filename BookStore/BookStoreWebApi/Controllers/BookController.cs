using System;
using System.Collections.Generic;
using System.Linq;
using BookStoreWebApi.Common;
using BookStoreWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>(){
                new Book(){
                    Id = (int)GenreEnum.PersonalGrowth,
                    Title = "Lean Startup",
                    GenreId = 1, // Personal Growth
                    PageCount=200,
                    PublishDate = new DateTime(2001,06,12)
                },
                new Book(){
                    Id = 2,
                    Title = "Herland",
                    GenreId = (int)GenreEnum.ScienceFiction, // Science Fiction
                    PageCount=250,
                    PublishDate = new DateTime(2002,06,12)
                },
                new Book(){
                    Id = 3,
                    Title = "Dune",
                    GenreId =(int)GenreEnum.ScienceFiction, // Science Fiction
                    PageCount=540,
                    PublishDate =new DateTime(2002,05,23)
                },
          };
        //Get All Books
        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        //Get Book With FromRoute
        [HttpGet("{id}")]
        public Book GetBookById(int id)
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        //Get Book With FromQuery
        // [HttpGet]
        // public Book GetBookById2([FromQuery] string id)
        // {
        //     var book = this.BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        //Update Book
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);

            if (book is null)
                return NotFound();


            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            return Ok();
        }

        //Add Book
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == newBook.Id);

            if (book is not null)
                return BadRequest();

            BookList.Add(newBook);
            return Ok();
        }

    }
}