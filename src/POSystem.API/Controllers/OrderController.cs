﻿using Microsoft.AspNetCore.Mvc;
using POSystem.Application.DTOs;
using POSystem.Application.Interfaces;

namespace POSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int cursor = 0,
        [FromQuery] int pageSize = 10,
        [FromQuery] string searchQuery = "")
    {
        var orders = await _orderService.GetPagedAsync(cursor, pageSize);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.GetByIdAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _orderService.CreateAsync(order);
        return result ? Created() : BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDto order)
    {
        order.Id = id;

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _orderService.UpdateAsync(order);
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _orderService.DeleteAsync(id);
        return result ? NoContent() : BadRequest();
    }
}
