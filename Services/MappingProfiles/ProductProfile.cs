using Domain.Models;

namespace Services.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(d => d.BrandName, options
                => options.MapFrom(s => s.ProductBrand.Name));

        CreateMap<Product, ProductResponse>()
            .ForMember(d => d.TypeName, options
                => options.MapFrom(s => s.ProductType.Name));

        CreateMap<ProductBrand, BrandResponse>();
        CreateMap<ProductType, TypeResponse>();
    }
}