using POSystem.Domain.Entities;

namespace POSystem.Domain.DTOs;

public class GetOrderDto : Order
{
    public string SupplierName { get; set; }
}
