using OrderService.Entity;

namespace OrderService.Service;

public interface IOrderService
{
    public Task<int> CreateOrder(Order order);
    public Task<IEnumerable<Order>> GetOrders(string userId);
}