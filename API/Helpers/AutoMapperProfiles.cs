using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
               CreateMap<Category, CategoryDto>();

               CreateMap<RegisterDto, AppUser>();

               CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.imgUrl, opt => opt.MapFrom(src => src.ProductImgs.FirstOrDefault().Link));
        }
    }
}