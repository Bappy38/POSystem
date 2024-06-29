using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POSystem.Application.Interfaces;
using POSystem.Domain.DTOs;
using POSystem.Domain.Entities;
using POSystem.Web.Constants;

namespace POSystem.Web.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(int pageNo = 1, int pageSize = 10, string searchQuery = "")
    {
        ViewData["SearchQuery"] = searchQuery;

        var orders = await _orderService.GetPagedAsync(pageNo, pageSize, searchQuery);
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

        var updateOrderDto = _mapper.Map<UpdateOrderDto>(order);

        return View(updateOrderDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(UpdateOrderDto order)
    {
        if (!ModelState.IsValid)
        {
            return View(order);
        }

        var mappedOrder = _mapper.Map<Order>(order);
        var result = await _orderService.UpdateAsync(mappedOrder);

        var notificationType = result ? Notification.Success : Notification.Error;
        var notificationMessage = result
            ?
            OrderNotifications.UpdatedSuccessfully
            :
            OrderNotifications.FailedToUpdate;
        TempData[notificationType] = notificationMessage;

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
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

        var updateOrderDto = _mapper.Map<UpdateOrderDto>(order);

        return View(updateOrderDto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeletePOST(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var result = await _orderService.DeleteAsync(id);

        var notificationType = result ? Notification.Success : Notification.Error;
        var notificationMessage = result
            ?
            OrderNotifications.DeletedSuccessfully
            :
            OrderNotifications.FailedToDelete;
        TempData[notificationType] = notificationMessage;

        return RedirectToAction("Index");
    }
}