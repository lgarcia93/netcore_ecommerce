using Microsoft.EntityFrameworkCore;
using OrderService.DbContext;
using OrderService.Entity;

namespace OrderService.Repository;

public class MySqlOrderRepository : IOrderRepository
{
    private readonly DataContext _context;

    public MySqlOrderRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<int> CreateOrder(Order order)
    {
        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return order.Identifier;
    }

    public async Task<IEnumerable<Order>> GetOrders(string userId)
    {
        var orders = await _context.Orders.Where(order => order.UserId == userId).ToListAsync();

        return orders;

    }
}