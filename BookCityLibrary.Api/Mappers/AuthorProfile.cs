using AutoMapper;
using BookCityLibrary.Api.Dtos;
using BookLibrary.Data.Entities;

namespace BookCityLibrary.Api.Mappers;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<AuthorDto, Author>().IgnoreAllVirtual();
        
        CreateMap<AuthorDto, Author>()
            .ForAllMembers(opts => 
                opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}