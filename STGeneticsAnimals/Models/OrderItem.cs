namespace STGeneticsAnimals.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int AnimalId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
