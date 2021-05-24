using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.Application.GenreOperations.GetGenres;
using BookStoreWebApi.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class GenreController : ControllerBase
    {
        public readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQuery command = new GetGenresQuery(_context, _mapper);
            var obj = command.Handle();
            return Ok(obj);
        }
    }

}
