using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POSystem.Application.Constants;
using POSystem.Application.Interfaces;
using POSystem.Domain.DTOs;
using POSystem.Domain.Entities;
using POSystem.Web.Constants;
using POSystem.Web.Models;

namespace POSystem.Web.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IOrderExporter _orderExporter;
    private readonly ISupplierService _supplierService;
    private readonly IMapper _mapper;

    public OrdersController(
        IOrderService orderService,  
        IOrderExporter orderExporter,
        ISupplierService supplierService,
        IMapper mapper)
    {
        _orderService = orderService;
        _orderExporter = orderExporter;
        _supplierService = supplierService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(int pageNo = 1, int pageSize = 10, string searchQuery = "")
    {
        ViewData[TempDataKeys.SearchQuery] = searchQuery;

        var orders = await _orderService.GetPagedAsync(pageNo, pageSize, searchQuery);
        return View(orders);
    }

    public async Task<IActionResult> Create()
    {
        var suppliers = await _supplierService.GetSuppliersAsync();
        var orderViewModel = new CreateOrderViewModel
        {
            OrderDto = new CreateOrderDto(),
            Suppliers = suppliers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList()
        };

        return View(orderViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOrderViewModel createOrderViewModel)
    {
        if (!ModelState.IsValid)
        {
            var suppliers = await _supplierService.GetSuppliersAsync();
            createOrderViewModel.Suppliers = suppliers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            return View(createOrderViewModel);
        }

        var result = await _orderService.CreateAsync(createOrderViewModel.OrderDto);

        var notificationType = result ? Notification.Success : Notification.Error;
        var notificationMessage = result
            ?
            OrderNotifications.CreatedSuccessfully
            :
            OrderNotifications.FailedToCreate;
        TempData[notificationType] = notificationMessage;

        return RedirectToAction(Actions.Index);
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

        return RedirectToAction(Actions.Index);
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

    [HttpPost, ActionName(Actions.Delete)]
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

        return RedirectToAction(Actions.Index);
    }


    public async Task<IActionResult> Export(int id)
    {
        var order = await _orderService.GetByIdAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        var stream = await _orderExporter.ExportAsync(order);

        return File(stream.ToArray(), OrderExporterConfig.ContentType, OrderExporterConfig.ExportFileName);
    }
}