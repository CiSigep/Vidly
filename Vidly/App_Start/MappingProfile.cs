using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Models;
using Vidly.DTO;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDTO>();
            Mapper.CreateMap<Movie, MoviesDTO>();
            Mapper.CreateMap<MembershipType, MembershipTypeDTO>();
            Mapper.CreateMap<Genre, GenreDTO>();

            Mapper.CreateMap<MoviesDTO, Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<CustomerDTO, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<MembershipTypeDTO, MembershipType>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<GenreDTO, Genre>().ForMember(g => g.Id, opt => opt.Ignore());
        }
    }
}