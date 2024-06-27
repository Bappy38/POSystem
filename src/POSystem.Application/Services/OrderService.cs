using POSystem.Application.Interfaces;
using POSystem.Domain.DTOs;
using POSystem.Domain.Entities;
using POSystem.Domain.Repositories;

namespace POSystem.Application.Services;

public class OrderService : IOrderService
{
    private const int MAX_PAGE_SIZE = 10;
    private readonly IOrderRepository orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<int> CreateAsync(OrderDto order)
    {
        return await orderRepository.CreateAsync(order);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await orderRepository.DeleteAsync(id);
    }

    public async Task<OrderDto> GetByIdAsync(int id)
    {
        return await orderRepository.GetByIdAsync(id);
    }

    public async Task<List<Order>> GetPagedAsync(int cursor, int pageSize)
    {
        return await orderRepository.GetPagedAsync(cursor, Math.Min(pageSize, MAX_PAGE_SIZE));
    }

    public async Task<int> UpdateAsync(OrderDto order)
    {
        return await orderRepository.UpdateAsync(order);
    }
}
