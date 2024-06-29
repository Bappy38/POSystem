using System.ComponentModel.DataAnnotations;

namespace POSystem.Domain.DTOs;

public class UpdateOrderDto : CreateOrderDto
{
    [Required]
    public int Id { get; set; }
}
