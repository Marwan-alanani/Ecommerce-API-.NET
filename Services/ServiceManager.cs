using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Services;

public class ServiceManager(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IBasketRespository basketRespository,
    UserManager<ApplicationUser> userManager
) : IServiceManager
{
    private readonly Lazy<IProductService> _lazyProductService =
        new (() => new ProductService(unitOfWork, mapper));

    private readonly Lazy<IBasketService> _lazyBasketService =
        new ( () => new BasketService(basketRespository, mapper));

    private readonly Lazy<IAuthenticationService> _lazyAuthenticationService =
        new ( () => new AuthenticationService(userManager , mapper));

    public IProductService ProductService => _lazyProductService.Value;
    public IBasketService BasketService => _lazyBasketService.Value;

    public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
}