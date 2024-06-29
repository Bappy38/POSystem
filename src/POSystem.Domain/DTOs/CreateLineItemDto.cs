using System.ComponentModel.DataAnnotations;

namespace POSystem.Domain.DTOs;

public class CreateLineItemDto
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = $"Quantity must be greater than 0")]
    public int Quantity { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than zero.")]
    public decimal Rate { get; set; }
}
