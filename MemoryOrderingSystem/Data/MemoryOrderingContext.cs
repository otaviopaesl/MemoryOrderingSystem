using System;
using Microsoft.EntityFrameworkCore;

namespace MemoryOrderingSystem.Data
{
    public class MemoryOrderingContext : DbContext
    {
        public MemoryOrderingContext(DbContextOptions<MemoryOrderingContext> options) : base(options)
        {
        }
    }
}
