using POSystem.Application.DTOs;
using POSystem.Domain.Entities;

namespace POSystem.Application.Interfaces;

public interface IOrderService
{
    Task<bool> CreateAsync(CreateOrderDto order);
    Task<bool> UpdateAsync(UpdateOrderDto order);
    Task<bool> DeleteAsync(int id);
    Task<Order> GetByIdAsync(int id);
    Task<List<Order>> GetPagedAsync(int cursor, int pageSize);
}
