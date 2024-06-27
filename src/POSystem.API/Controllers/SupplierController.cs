using Microsoft.AspNetCore.Mvc;
using POSystem.Application.Interfaces;

namespace POSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var suppliers = await _supplierService.GetSuppliersAsync();
        return Ok(suppliers);
    }
}
