using Microsoft.AspNetCore.Mvc.Rendering;
using POSystem.Domain.DTOs;

namespace POSystem.Web.Models;

public class CreateOrderViewModel
{
    public CreateOrderDto OrderDto { get; set; }
    public List<SelectListItem> Suppliers { get; set; } = new List<SelectListItem>();
}