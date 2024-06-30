using AutoMapper;
using POSystem.Application.Interfaces;
using POSystem.Domain.DTOs;
using POSystem.Domain.Entities;
using POSystem.Domain.Repositories;

namespace POSystem.Application.Services;

public class OrderService : IOrderService
{
    private const int MaxPageSize = 10;

    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task<List<GetOrderDto>> GetPagedAsync(int cursor, int pageSize)
    {
        return await _orderRepository.GetPagedAsync(cursor, Math.Min(pageSize, MaxPageSize));
    }

    public async Task<PaginatedList<GetOrderDto>> GetPagedAsync(int pageNo, int pageSize, string searchQuery)
    {
        return await _orderRepository.GetPagedAsync(pageNo, Math.Min(pageSize, MaxPageSize), searchQuery);
    }

    public async Task<bool> CreateAsync(CreateOrderDto order)
    {
        var mappedOrder = _mapper.Map<Order>(order);
        var numberOfAffectedRows = await _orderRepository.CreateAsync(mappedOrder);
        return numberOfAffectedRows > 0;
    }

    public async Task<bool> UpdateAsync(Order order)
    {
        var numberOfAffectedRows = await _orderRepository.UpdateAsync(order);
        return numberOfAffectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var numberOfAffectedRows = await _orderRepository.DeleteAsync(id);
        return numberOfAffectedRows > 0;
    }
}
