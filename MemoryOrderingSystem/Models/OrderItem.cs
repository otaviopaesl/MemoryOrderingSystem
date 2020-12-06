namespace MemoryOrderingSystem.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }

        public OrderItem()
        {
        }

        public OrderItem(int id, string name, int quantity, decimal unitValue)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            UnitValue = unitValue;
        }
    }
}
