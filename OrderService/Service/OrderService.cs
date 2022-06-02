using OrderService.Entity;

namespace OrderService.Service;

public class OrderService : IOrderService
{
    public Task<int> CreateOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrders(string userId)
    {
        throw new NotImplementedException();
    }
}