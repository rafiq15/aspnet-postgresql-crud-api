using System;
using Microsoft.EntityFrameworkCore;
using PgCrudApi.Models;

namespace PgCrudApi.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
}
