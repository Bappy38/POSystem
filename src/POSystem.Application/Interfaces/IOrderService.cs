using POSystem.Domain.DTOs;
using POSystem.Domain.Entities;

namespace POSystem.Application.Interfaces;

public interface IOrderService
{
    Task<int> CreateAsync(OrderDto order);
    Task<int> UpdateAsync(OrderDto order);
    Task<int> DeleteAsync(int id);
    Task<OrderDto> GetByIdAsync(int id);
    Task<List<Order>> GetPagedAsync(int cursor, int pageSize);
}
