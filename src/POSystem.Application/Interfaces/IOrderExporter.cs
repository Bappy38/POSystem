using POSystem.Domain.Entities;

namespace POSystem.Application.Interfaces;

public interface IOrderExporter
{
    Task<MemoryStream> ExportAsync(Order order);
}
