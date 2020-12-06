using System;
using MemoryOrderingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoryOrderingSystem.Data
{
    public class MemoryOrderingContext : DbContext
    {
        public MemoryOrderingContext(DbContextOptions<MemoryOrderingContext> options) : base(options)
        {
        }

        public DbSet<Seller> Seller { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
    }
}
