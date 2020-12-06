using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemoryOrderingSystem.Data;
using MemoryOrderingSystem.Models;
using MemoryOrderingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MemoryOrderingSystem.Services
{
    public class SellerService : IServiceBase<Seller>
    {

        private readonly MemoryOrderingContext _context;


        public SellerService(MemoryOrderingContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Seller>> Get()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task<Seller> Get(int id)
        {
            return await _context.Seller.FindAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Seller.AnyAsync(e => e.Id == id);
        }

        public async Task Insert(Seller obj)
        {
            try
            {
                _context.Seller.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task Remove(Seller obj)
        {
            try
            {
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
            {
                throw new Exception("Seller not found");
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
