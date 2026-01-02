using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PgCrudApi.Data;
using PgCrudApi.Dtos;
using PgCrudApi.mapper;

namespace PgCrudApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Protect all endpoints with JWT
public class ProductsController : ControllerBase
{
    private readonly ProductDbContext _context;

    public ProductsController(ProductDbContext context)
    {
        _context = context;
    }
    // GET api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return products.Select(p => p.ToDto()).ToList();
    }


    // GET api/products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        return product.ToDto();
    }

    // POST api/products
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
    {
        var entity = createProductDto.ToEntity();
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduct), new { id = entity.Id }, entity.ToDto());
    }
    // PUT api/products/5
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
    {
        Console.WriteLine($"Updating product with ID: {id}");

        var existingProduct = await _context.Products.FindAsync(id);
        if (existingProduct == null) return NotFound();

        updateProductDto.ToEntity(existingProduct);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/products/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}