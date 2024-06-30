using POSystem.Domain.DTOs;
using POSystem.Domain.Entities;

namespace POSystem.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);
    Task<List<GetOrderDto>> GetPagedAsync(int cursor, int pageSize);
    Task<PaginatedList<GetOrderDto>> GetPagedAsync(int pageNo, int pageSize, string searchQuery);
    Task<int> CreateAsync(Order order);
    Task<int> UpdateAsync(Order order);
    Task<int> DeleteAsync(int id);
}
