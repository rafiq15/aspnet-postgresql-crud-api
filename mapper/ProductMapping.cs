using System;
using PgCrudApi.Dtos;
using PgCrudApi.Models;

namespace PgCrudApi.mapper;

public static class ProductMapping
{

    public static Product ToEntity(this CreateProductDto dto)
    {
        return new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Product ToEntity(this UpdateProductDto dto, Product existingProduct)
    {
        if (!string.IsNullOrWhiteSpace(dto.Name)) existingProduct.Name = dto.Name;
        if (!string.IsNullOrWhiteSpace(dto.Description)) existingProduct.Description = dto.Description;
        if (dto.Price.HasValue && dto.Price.Value >= 0) existingProduct.Price = dto.Price.Value;

        return existingProduct;
    }
    public static ProductDto ToDto(this Product entity)
    {
        return new ProductDto(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Price,
            entity.CreatedAt
        );
    }


}