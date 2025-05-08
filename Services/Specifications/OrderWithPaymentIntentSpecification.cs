using System.Linq.Expressions;

namespace Services.Specifications;

public class OrderWithPaymentIntentSpecification( string paymentIntentId )
    : BaseSpecifications<Order>(order => order.PaymentIntentId == paymentIntentId);