using POSystem.Domain.Entities;

namespace POSystem.Application.Interfaces;

public interface ISupplierService
{
    Task<List<Supplier>> GetAllAsync();
}
