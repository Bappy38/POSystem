namespace POSystem.Domain.Entities;

public class LineItem
{
    public int LineItemId { get; set; }
    public int OrderId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Rate { get; set; }
}
