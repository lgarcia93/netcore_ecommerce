using OrderService.Entity;

namespace OrderService.Repository;

public interface IOrderRepository
{
    public Task<int> CreateOrder(Order order);
    public Task<IEnumerable<Order>> GetOrders(string userId);
}