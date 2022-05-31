using AutoMapper;
using BookCityLibrary.Api.Dtos;
using BookLibrary.Data.Entities;

namespace BookCityLibrary.Api.Mappers;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookDto, Book>().IgnoreAllVirtual();
        
        CreateMap<BookDto, Book>()
            .ForAllMembers(opts => 
                opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}