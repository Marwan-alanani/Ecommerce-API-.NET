using System.Linq.Expressions;

namespace Services.Specifications;

internal class OrderSpecifications
    : BaseSpecifications<Order>
{
    public OrderSpecifications(Guid id) : base(order => order.Id  == id)
    {
       AddInclude(x => x.DeliveryMethod);
       AddInclude(x => x.Items);
       AddOrderByDescending(x => x.OrderDate);
    }

    public OrderSpecifications(string email) : base(order => order.BuyerEmail  == email)
    {
        AddInclude(x => x.DeliveryMethod);
        AddInclude(x => x.Items);
        AddOrderByDescending(x => x.OrderDate);

    }
}