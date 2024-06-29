using Microsoft.AspNetCore.Mvc.Rendering;
using POSystem.Domain.DTOs;

namespace POSystem.Web.Models;

public class UpdateOrderViewModel
{
    public UpdateOrderDto OrderDto { get; set; }
    public List<SelectListItem> SupplierOptions { get; set; } = new List<SelectListItem>();
}
