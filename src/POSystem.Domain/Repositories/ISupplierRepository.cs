using POSystem.Domain.Entities;

namespace POSystem.Domain.Repositories;

public interface ISupplierRepository
{
    Task<List<Supplier>> GetAllAsync();
}
