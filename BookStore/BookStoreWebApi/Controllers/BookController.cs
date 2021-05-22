using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.DeleteBook;
using BookStoreWebApi.BookOperations.GetBookDetail;
using BookStoreWebApi.BookOperations.GetBooks;
using BookStoreWebApi.BookOperations.UpdateBook;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Get All Books
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery command = new GetBooksQuery(_context, _mapper);
            var obj = command.Handle();
            return Ok(obj);
        }

        //Get Book With FromRoute
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            BookDetailViewModel obj;
            try
            {
                GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
                command.BookId = id;
                obj = command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(obj);
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
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }

        //Add Book
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            try
            {
                CreateBookCommand command = new CreateBookCommand(_context, _mapper);
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}