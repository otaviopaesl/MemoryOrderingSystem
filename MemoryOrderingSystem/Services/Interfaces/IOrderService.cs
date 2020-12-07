using System.Collections.Generic;
using System.Threading.Tasks;
using MemoryOrderingSystem.Models;
using MemoryOrderingSystem.Models.Enums;

namespace MemoryOrderingSystem.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> Get();
        Task<Order> Get(int id);
        Task<Order> Insert(int sellerId, List<OrderItem> orderItems);
        Task<Order> UpdateStatus(int id, OrderStatus status);
    }
}
