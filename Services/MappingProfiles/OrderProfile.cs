namespace Services.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderAddress, AddressDTO>().ReverseMap();
        CreateMap<OrderItem, OrderItemDTO>()
            .ForMember(d => d.ProductName,
                o
                    => o.MapFrom(p => p.Product.ProductName))
            .ForMember(d => d.PictureUrl,
                o
                    => o.MapFrom<OrderItemPictureUrlResolver>());

        CreateMap<Order, OrderResponse>()
            .ForMember(d => d.DeliveryMethod, o
                => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.Total, o
                => o.MapFrom(s => s.DeliveryMethod.Price + s.Subtotal));

        CreateMap<DeliveryMethod, DeliveryMethodResponse>();
    }
}

internal class OrderItemPictureUrlResolver(IConfiguration configuration) :
    IValueResolver<OrderItem, OrderItemDTO, string>
{
    public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.Product.PictureUrl)) return "";
        return $"{configuration["BaseUrl"]}/{source.Product.PictureUrl}";
    }
}