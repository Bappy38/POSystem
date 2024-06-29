using Microsoft.AspNetCore.Mvc;
using POSystem.Application.Interfaces;
using POSystem.Domain.DTOs;
using POSystem.Web.Constants;

namespace POSystem.Web.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetPagedAsync(0, 10);
        return View(orders);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOrderDto order)
    {
        if (!ModelState.IsValid)
        {
            return View(order);
        }

        var result = await _orderService.CreateAsync(order);

        var notificationType = result ? Notification.Success : Notification.Error;
        var notificationMessage = result
            ?
            OrderNotifications.CreatedSuccessfully
            :
            OrderNotifications.FailedToCreate;
        TempData[notificationType] = notificationMessage;

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var order = await _orderService.GetByIdAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(UpdateOrderDto order)
    {
        if (!ModelState.IsValid)
        {
            return View(order);
        }

        var result = await _orderService.CreateAsync(order);

        var notificationType = result ? Notification.Success : Notification.Error;
        var notificationMessage = result
            ?
            OrderNotifications.CreatedSuccessfully
            :
            OrderNotifications.FailedToCreate;
        TempData[notificationType] = notificationMessage;

        return RedirectToAction("Index");
    }
}