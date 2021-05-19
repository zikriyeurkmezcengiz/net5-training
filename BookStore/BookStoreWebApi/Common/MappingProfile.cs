using AutoMapper;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
        }
    }

}