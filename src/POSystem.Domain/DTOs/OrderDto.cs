using POSystem.Domain.Entities;

namespace POSystem.Domain.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public string ReferenceId { get; set; }
    public DateTime PlacedAtUtc { get; set; }
    public int SupplierId { get; set; }
    public DateTime? ExpectedDate { get; set; }
    public string Remark { get; set; }
    public List<LineItemDto> Items { get; set; }
}
