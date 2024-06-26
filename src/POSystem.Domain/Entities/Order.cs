namespace POSystem.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string ReferenceId { get; set; }
    public DateTime PlacedAtUtc { get; set; }
    public int SupplierId { get; set; }
    public DateTime? ExpectedDate { get; set; }
    public string Remark { get; set; }
    public ICollection<LineItem> Items { get; set; }
}
