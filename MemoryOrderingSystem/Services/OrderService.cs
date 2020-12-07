using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoryOrderingSystem.Data;
using MemoryOrderingSystem.Models;
using MemoryOrderingSystem.Models.Enums;
using MemoryOrderingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MemoryOrderingSystem.Services
{
    public class OrderService : IOrderService
    {

        private readonly MemoryOrderingContext _context;
        private readonly SellerService _sellerService;
        private readonly OrderItemService _orderItemService;
        private readonly Dictionary<OrderStatus, OrderStatus[]> _orderStatus =
            new Dictionary<OrderStatus, OrderStatus[]>
            {
                { OrderStatus.AguardandoPagamento, new OrderStatus[] { OrderStatus.PagamentoAprovado, OrderStatus.Cancelada } },
                { OrderStatus.PagamentoAprovado, new OrderStatus[] { OrderStatus.EnviadoTransportadora, OrderStatus.Cancelada } },
                { OrderStatus.EnviadoTransportadora, new OrderStatus[] { OrderStatus.Entregue } },
                { OrderStatus.Entregue, new OrderStatus[] {} },
                { OrderStatus.Cancelada, new OrderStatus[] {} },
            };

        public OrderService(MemoryOrderingContext context, OrderItemService orderItemService, SellerService sellerService)
        {
            _context = context;
            _orderItemService = orderItemService;
            _sellerService = sellerService;

        }
        public async Task<IEnumerable<Order>> Get()
        {
            return await _context.Order
                .Include(obj => obj.Seller)
                .Include(obj => obj.OrderItems)
                .ToListAsync();
        }

        public async Task<Order> Get(int id)
        {
            return await _context.Order
                .Include(obj => obj.Seller)
                .Include(obj => obj.OrderItems)
                .SingleOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<Order> Insert(int sellerId, List<OrderItem> orderItems)
        {
            try
            {
                var status = OrderStatus.AguardandoPagamento;
                var seller = await _sellerService.Get(sellerId);

                if (seller == null)
                    throw new Exception("O vendedor informado não foi encontrado.");

                var order = new Order(0, seller, status);

                foreach (var item in orderItems)
                {
                    await _orderItemService.Insert(item);
                }

                order.AddOrderItems(orderItems);
                order.CalculateTotal();

                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                return await Get(order.Id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<Order> UpdateStatus(int id, OrderStatus status)
        {
            var order = await Get(id);

            if(order == null)
                throw new Exception("Não foi possivel encontrar um Pedido com o id informado!");

            if (order.OrderStatus == status)
                throw new Exception("O pedido já está com este status.");

            bool canChange = CanChangeStatus(order.OrderStatus, status);

            if (canChange)
            {
                try
                {
                    order.OrderStatus = status;
                    _context.Order.Update(order);
                    await _context.SaveChangesAsync();
                    return await Get(order.Id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            throw new Exception($"Não é possivel alterar o status do pedido para {status}.");
        }

        private bool CanChangeStatus(OrderStatus current, OrderStatus next)
        {
            return _orderStatus[current].Contains(next);
        }
    }
}
