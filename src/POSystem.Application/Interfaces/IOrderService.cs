using POSystem.Domain.DTOs;
using POSystem.Domain.Entities;

namespace POSystem.Application.Interfaces;

public interface IOrderService
{
    Task<bool> CreateAsync(CreateOrderDto order);
    Task<bool> UpdateAsync(Order order);
    Task<bool> DeleteAsync(int id);
    Task<Order> GetByIdAsync(int id);
    Task<List<GetOrderDto>> GetPagedAsync(int cursor, int pageSize);
    Task<PaginatedList<GetOrderDto>> GetPagedAsync(int pageNo, int pageSize, string searchQuery);
}
