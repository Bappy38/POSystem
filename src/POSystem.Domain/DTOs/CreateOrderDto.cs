using POSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace POSystem.Domain.DTOs;

public class CreateOrderDto : IValidatableObject
{
    public int ReferenceId { get; init; }

    [Required]
    public string PurchaseOrderNo { get; init; }

    public DateTime PlacedAtUtc { get; init; } = DateTime.UtcNow;

    public int SupplierId { get; init; }

    public DateTime? ExpectedDate { get; init; }

    public string? Remark { get; init; }

    [Required]
    public List<CreateLineItemDto> Items { get; init; } = new();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ReferenceId <= 0)
        {
            yield return new ValidationResult("Reference Id is required.", new[] { nameof(ReferenceId) });
        }

        if (SupplierId <= 0)
        {
            yield return new ValidationResult("Supplier Id is required.", new[] { nameof(SupplierId) });
        }

        if (ExpectedDate is not null && ExpectedDate < DateTime.UtcNow)
        {
            yield return new ValidationResult("Expected date must be greater than current date.", new[] { nameof(ExpectedDate) });
        }

        if (Items is null ||  Items.Count == 0)
        {
            yield return new ValidationResult("At least one line item is required.", new[] { nameof(LineItem) });
        }
    }
}
