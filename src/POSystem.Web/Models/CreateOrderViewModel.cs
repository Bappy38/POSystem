using Microsoft.AspNetCore.Mvc.Rendering;
using POSystem.Domain.DTOs;
using POSystem.Domain.Entities;

namespace POSystem.Web.Models;

public class CreateOrderViewModel
{
    public CreateOrderDto OrderDto { get; set; }
    public List<SelectListItem> Suppliers { get; set; } = new List<SelectListItem>();
}