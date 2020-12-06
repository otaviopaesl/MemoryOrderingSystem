using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemoryOrderingSystem.Data;
using MemoryOrderingSystem.Models;
using MemoryOrderingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MemoryOrderingSystem.Services
{
    public class OrderItemService : IServiceBase<OrderItem>
    {

        private readonly MemoryOrderingContext _context;


        public OrderItemService(MemoryOrderingContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<OrderItem>> Get()
        {
            return await _context.OrderItem.ToListAsync();
        }

        public async Task<OrderItem> Get(int id)
        {
            return await _context.OrderItem.FindAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.OrderItem.AnyAsync(e => e.Id == id);
        }

        public async Task Insert(OrderItem obj)
        {
            try
            {
                _context.OrderItem.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task Remove(OrderItem obj)
        {
            try
            {
                _context.OrderItem.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(OrderItem obj)
        {
            bool hasAny = await _context.OrderItem.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
            {
                throw new Exception("OrderItem not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
