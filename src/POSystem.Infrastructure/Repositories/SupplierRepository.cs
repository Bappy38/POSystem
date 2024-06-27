using Dapper;
using POSystem.Domain.Entities;
using POSystem.Domain.Repositories;
using POSystem.Infrastructure.Data;
using System.Data;

namespace POSystem.Infrastructure.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly DbContext _context;

    public SupplierRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<List<Supplier>> GetSuppliersAsync()
    {
        using (var connection = _context.CreateConnection())
        {
            var suppliers = await connection.QueryAsync<Supplier>("spGetSuppliers", commandType: CommandType.StoredProcedure);
            return suppliers?.ToList() ?? new List<Supplier>();
        }
    }
}
