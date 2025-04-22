namespace Services;

public class ServiceManager(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IBasketRespository basketRespository) : IServiceManager
{
    private readonly Lazy<IProductService> _lazyProductService =
        new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

    private readonly Lazy<IBasketService> _lazyBasketService =
        new Lazy<IBasketService>(() => new BasketService(basketRespository, mapper));

    public IProductService ProductService => _lazyProductService.Value;
    public IBasketService BasketService => _lazyBasketService.Value;
}