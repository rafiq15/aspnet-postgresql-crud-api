using System;
using Microsoft.EntityFrameworkCore;
using PgCrudApi.Models;

namespace PgCrudApi.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
}
