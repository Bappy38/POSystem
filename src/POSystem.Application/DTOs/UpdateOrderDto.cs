using System.ComponentModel.DataAnnotations;

namespace POSystem.Application.DTOs;

public class UpdateOrderDto : CreateOrderDto
{
    [Required]
    public int Id { get; set; }
}
