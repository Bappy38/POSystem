namespace POSystem.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string ReferenceId { get; set; }
    public DateTime Date { get; set; }
    public Supplier Supplier { get; set; }
    public DateTime? ExpectedDate { get; set; }
    public string Remark { get; set; }
    public ICollection<LineItem> Items { get; set; }
}
