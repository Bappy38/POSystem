using AutoMapper;
using POSystem.Application.DTOs;
using POSystem.Application.Interfaces;
using POSystem.Domain.Entities;
using POSystem.Domain.Repositories;

namespace POSystem.Application.Services;

public class OrderService : IOrderService
{
    private const int MAX_PAGE_SIZE = 10;

    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(CreateOrderDto order)
    {
        var mappedOrder = _mapper.Map<Order>(order);
        return await _orderRepository.CreateAsync(mappedOrder);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _orderRepository.DeleteAsync(id);
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task<List<Order>> GetPagedAsync(int cursor, int pageSize)
    {
        return await _orderRepository.GetPagedAsync(cursor, Math.Min(pageSize, MAX_PAGE_SIZE));
    }

    public async Task<int> UpdateAsync(UpdateOrderDto order)
    {
        var mappedOrder = _mapper.Map<Order>(order);
        return await _orderRepository.UpdateAsync(mappedOrder);
    }
}
