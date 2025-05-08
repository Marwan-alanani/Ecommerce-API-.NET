using Domain.Models;
using Domain.Models.Products;
using Microsoft.Extensions.Configuration;

namespace Services.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(d => d.ProductBrand, options
                => options.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, options
                => options.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl,
                options =>
                    options.MapFrom<PictureUrlResolver>()) ;

        CreateMap<ProductBrand, BrandResponse>();
        CreateMap<ProductType, TypeResponse>();
    }
}

public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductResponse, string>
{
    public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.PictureUrl)) return "";
        return $"{configuration["BaseUrl"]}/{source.PictureUrl}" ;
    }
}