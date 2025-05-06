namespace Services;

public class ServiceManagerWithFactoryDelegate(
    Func<IProductService> productFactory,
    Func<IBasketService> basketFactory,
    Func<IAuthenticationService> authenticationFactory,
    Func<IOrderService> orderFactory)
    : IServiceManager
{
    public IProductService ProductService => productFactory.Invoke();
    public IBasketService BasketService => basketFactory.Invoke();
    public IAuthenticationService AuthenticationService => authenticationFactory.Invoke();
    public IOrderService OrderService => orderFactory.Invoke();
}