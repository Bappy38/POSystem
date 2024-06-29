namespace POSystem.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int ReferenceId { get; set; }
    public string PurchaseOrderNo { get; set; }
    public DateTime PlacedAtUtc { get; set; }
    public int SupplierId { get; set; }
    public DateTime? ExpectedDate { get; set; }
    public string Remark { get; set; }
    public ICollection<LineItem> Items { get; set; }
}
