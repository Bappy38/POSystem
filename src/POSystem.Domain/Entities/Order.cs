using System.ComponentModel.DataAnnotations;

namespace POSystem.Domain.Entities;

public class Order : IValidatableObject
{
    public int Id { get; set; }

    public int ReferenceId { get; set; }

    [Required]
    public string PurchaseOrderNo { get; set; }

    public DateTime PlacedAtUtc { get; set; } = DateTime.UtcNow;

    public int SupplierId { get; set; }

    public DateTime? ExpectedDate { get; set; }

    public string Remark { get; set; }

    [Required]
    public ICollection<LineItem> Items { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Id <= 0)
        {
            yield return new ValidationResult("Invalid Id");
        }

        if (ReferenceId <= 0)
        {
            yield return new ValidationResult("Reference Id is required.", new[] { nameof(ReferenceId) });
        }

        if (SupplierId <= 0)
        {
            yield return new ValidationResult("Supplier Id is required.", new[] { nameof(SupplierId) });
        }

        if (Items is null || Items.Count == 0)
        {
            yield return new ValidationResult("At least one line item is required.", new[] { nameof(LineItem) });
        }
    }
}
