using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.Application.BookOperations.CreateBook;
using BookStoreWebApi.Application.BookOperations.DeleteBook;
using BookStoreWebApi.Application.BookOperations.GetBookDetail;
using BookStoreWebApi.Application.BookOperations.GetBooks;
using BookStoreWebApi.Application.BookOperations.UpdateBook;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using FluentValidation;
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

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);

            obj = query.Handle();
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

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updateBook;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();

        }

        //Add Book
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            // try
            // {

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);

            // ValidationResult result = validator.Validate(Model);

            // if (!result.IsValid)
            //     foreach (var failure in result.Errors)
            //         Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);

            //     command.Handle();
            // }
            // catch (Exception ex)
            // {
            //     throw ex;
            //     //return BadRequest(ex.Message);
            // }
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}