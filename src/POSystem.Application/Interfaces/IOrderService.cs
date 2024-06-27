using POSystem.Application.DTOs;
using POSystem.Domain.Entities;

namespace POSystem.Application.Interfaces;

public interface IOrderService
{
    Task<int> CreateAsync(CreateOrderDto order);
    Task<int> UpdateAsync(UpdateOrderDto order);
    Task<int> DeleteAsync(int id);
    Task<Order> GetByIdAsync(int id);
    Task<List<Order>> GetPagedAsync(int cursor, int pageSize);
}
