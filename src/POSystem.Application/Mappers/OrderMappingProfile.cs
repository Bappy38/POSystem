using AutoMapper;
using POSystem.Domain.DTOs;
using POSystem.Domain.Entities;

namespace POSystem.Application.Mappers;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, CreateOrderDto>().ReverseMap();
        CreateMap<Order, UpdateOrderDto>().ReverseMap();
        CreateMap<LineItem, CreateLineItemDto>().ReverseMap();
    }
}
