using ECommerce.Domain.Entities.Basket;

namespace ECommerce.UseCases.DTOs.Basket;

public class BasketDto
{
    public string Id { get; set; } = default!;

    public ICollection<BasketItemDto> Items { get; set; } = [];
}
