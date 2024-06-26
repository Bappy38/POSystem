namespace POSystem.Domain.Entities;

public class LineItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int Name { get; set; }
    public int Quantity { get; set; }
    public decimal Rate { get; set; }
    public Order Order { get; set; }
}
