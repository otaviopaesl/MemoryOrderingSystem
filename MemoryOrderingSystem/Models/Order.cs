using System;
using System.Collections.Generic;
using MemoryOrderingSystem.Models.Enums;

namespace MemoryOrderingSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
        public Seller Seller { get; set; }
        public int SellerId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; private set; }

        public Order()
        {
        }

        public Order(int id, Seller seller, OrderStatus orderStatus)
        {
            Id = id;
            Seller = seller;
            OrderStatus = orderStatus;
            Date = DateTime.Now;
        }

        public void AddOrderItems(IEnumerable<OrderItem> orderItems)
        {
            OrderItems.AddRange(orderItems);
        }

        public bool RemoveOrderItems(OrderItem orderItem)
        {
            return OrderItems.Remove(orderItem);
        }

        public decimal CalculateTotal()
        {
            foreach (OrderItem item in OrderItems)
            {
                Value += item.Quantity * item.UnitValue;
            }

            return Value;
        }
    }
}
