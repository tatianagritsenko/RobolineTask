using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RobolineTestTask.Database;

public partial class RobolineContext : DbContext
{
    private readonly string connectionString;
    public RobolineContext(DbContextOptions<RobolineContext> options, IConfiguration configuration) : base(options)
    {
        connectionString = configuration.GetConnectionString("RobolineDBString");
    }

    public DbSet<ProductCategory> ProductCategories { get; set; } // таблица ProductCategories
    public DbSet<Product> Products { get; set; } // таблица Products

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlite(connectionString);
    }
       
}
