using POSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace POSystem.Application.DTOs;

public class CreateOrderDto : IValidatableObject
{
    [Required]
    public string ReferenceId { get; init; }

    public DateTime PlacedAtUtc { get; init; } = DateTime.UtcNow;

    [Required]
    public int SupplierId { get; init; }

    public DateTime? ExpectedDate { get; init; }

    public string Remark { get; init; }

    [Required]
    public List<CreateLineItemDto> Items { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(ReferenceId))
        {
            yield return new ValidationResult("ReferenceId is required.", new[] { nameof(ReferenceId) });
        }

        if (SupplierId <= 0)
        {
            yield return new ValidationResult("SupplierId is required.", new[] { nameof(SupplierId) });
        }

        if (Items is null ||  Items.Count == 0)
        {
            yield return new ValidationResult("At least one line item is required.", new[] { nameof(LineItem) });
        }
    }
}
