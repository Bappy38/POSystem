using POSystem.Application.Interfaces;
using POSystem.Domain.Entities;
using POSystem.Domain.Repositories;

namespace POSystem.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<List<Supplier>> GetSuppliersAsync()
    {
        return await _supplierRepository.GetSuppliersAsync();
    }
}
