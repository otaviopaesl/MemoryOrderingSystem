using System.Linq;
using MemoryOrderingSystem.Models;

namespace MemoryOrderingSystem.Data
{
    public class SetupService
    {

        private readonly MemoryOrderingContext _context;

        public SetupService(MemoryOrderingContext context)
        {
            _context = context;

        }

        public void Setup()
        {

            if (_context.OrderItem.Any() || _context.Seller.Any())
            {
                return;
            }

            Seller s1 = new Seller(0, "12345678900", "Seller 1", "seller1@seller.com.br", "99 999999999");
            Seller s2 = new Seller(0, "98765432100", "Seller 2", "seller2@seller.com.br", "88 888888888");
            Seller s3 = new Seller(0, "00999998800", "Seller 3", "seller3@seller.com.br", "77 777777777");

            _context.Seller.AddRange(s1, s2, s3);

            _context.SaveChanges();
        }
    }
}
