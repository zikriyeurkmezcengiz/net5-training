
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.Application.GenreOperations.GetGenres
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var bookList = _context.Genres.OrderBy(x => x.Id);

            List<GenresViewModel> obj = _mapper.Map<List<GenresViewModel>>(bookList);
            return obj;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public int Name { get; set; }
    }

}