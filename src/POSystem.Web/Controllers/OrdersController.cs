using Microsoft.AspNetCore.Mvc;
using POSystem.Application.DTOs;
using POSystem.Application.Interfaces;
using POSystem.Web.Constants;

namespace POSystem.Web.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<ActionResult> Index()
    {
        var orders = await _orderService.GetPagedAsync(0, 10);
        return View(orders);
    }   
}