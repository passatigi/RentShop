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
                .ForMember(dest => dest.imgUrl, opt => opt.MapFrom(p => p.ProductImgs.FirstOrDefault().Link));

            CreateMap<Product, DetailedProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(p => p.Category.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(p => p.Category.Id))
                .ForMember(dest => dest.ProductImgsLinks, opt => opt.MapFrom(p => p.ProductImgs.Select(p => p.Link)));

            CreateMap<ProductFeature, ProductFeatureDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(f => f.Feature.Name))
                .ForMember(dest => dest.Explanation, opt => opt.MapFrom(f => f.Feature.Explanation))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(f => f.Feature.GroupName))
                .ForMember(dest => dest.FeatureId, opt => opt.MapFrom(f => f.Feature.Id));

            CreateMap<RealProduct, RealProductDto>();
            CreateMap<RealProductDto, RealProduct>();

            CreateMap<RealProduct, RealProductSchedule>()
                .ForMember(dest => dest.RealProductId, opt => opt.MapFrom(p => p.Id))
                .ForMember(dest => dest.Segments, opt => opt.MapFrom(p => p.OrderProducts.Select(op => op.Order)
                .Select(o => new ProductScheduleSegmentsDto 
                { 
                    RentStart = o.RequiredDate,
                    RentEnd = o.RequiredReturnDate 
                })));
            
            CreateMap<AddProductDto, Product>();

            CreateMap<Feature, FeatureDto>()
                .ForMember(dest => dest.FeatureId, opt => opt.MapFrom(f => f.Id));

            CreateMap<AddFeatureDto, Feature>();

            CreateMap<AddProductFeatureDto, ProductFeature>();

            CreateMap<OrderDto, Order>();

            CreateMap<Order, OrderDto>();

            CreateMap<OrderProduct, RealProductDto>()
                // .ForMember(dest => dest.Id, opt => opt.MapFrom(f => f.RealProduct.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(f => f.RealProduct.ProductId))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(f => f.RealProduct.SerialNumber))
                .ForMember(dest => dest.Condition, opt => opt.MapFrom(f => f.RealProduct.Condition))
                .ForMember(dest => dest.RentPrice, opt => opt.MapFrom(f => f.RealProduct.RentPrice));
                
            CreateMap<ProductImg, ProductImgDto>();

            CreateMap<UserUpdateDto,AppUser>();

            CreateMap<AddressDto,Address>();

            CreateMap<DeliverymanSchedule, DeliverymanScheduleDto>();

            CreateMap<AppUser, UserDto>();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.RealProducts, opt => opt.MapFrom(p => p.OrderProducts.Select(op => op.RealProduct)));

        }
    }
}