using POSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSystem.Application.Interfaces;

public interface ISupplierService
{
    Task<List<Supplier>> GetSuppliersAsync();
}
