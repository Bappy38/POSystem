using POSystem.Domain.Entities;

namespace POSystem.Domain.Repositories;

public interface IOrderRepository
{
    Task<int> CreateAsync(Order order);
    Task<int> UpdateAsync(Order order);
    Task<int> DeleteAsync(int id);
    Task<Order> GetByIdAsync(int id);
    Task<List<Order>> GetPagedAsync(int cursor, int pageSize);
}
