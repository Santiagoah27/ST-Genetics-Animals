namespace STGeneticsAnimals.Models
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal FreightCharge { get; set; }
        public List<OrderItem> Items { get; set; }
    }

}
