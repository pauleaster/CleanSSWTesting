using Application.Features.Products.DTOs;
using Domain.Models;

namespace Application.Features.Products.Mappings;

public static class ProductMappings
{
    public static ProductDto ToProductDto(this Product product)
    {
        return new ProductDto
        {
            Description = product.Description,
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }
    
}