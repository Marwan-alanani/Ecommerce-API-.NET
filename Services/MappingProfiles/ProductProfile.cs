using Domain.Models;

namespace Services.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(d => d.BrandName, options
                => options.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.TypeName, options
                => options.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl,
                options =>
                    options.MapFrom<PictureUrlResolver>()) ;

        CreateMap<ProductBrand, BrandResponse>();
        CreateMap<ProductType, TypeResponse>();
    }
}

public class PictureUrlResolver : IValueResolver<Product, ProductResponse, string>
{
    public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.PictureUrl)) return "";
        return $"http://localhost:5000/{source.PictureUrl}" ;
    }
}