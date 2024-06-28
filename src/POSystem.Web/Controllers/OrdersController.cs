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

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateOrderDto order)
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