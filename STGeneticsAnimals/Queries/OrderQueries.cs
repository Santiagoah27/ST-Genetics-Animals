namespace STGeneticsAnimals.Queries
{
    public class OrderQueries
    {
        public const string CreateOrder = "INSERT INTO Orders (TotalAmount, FreightCharge) OUTPUT INSERTED.OrderId VALUES (@TotalAmount, @FreightCharge)";
        public const string CreateOrderItem = "INSERT INTO OrderItems (OrderId, AnimalId, Quantity, Price) VALUES (@OrderId, @AnimalId, @Quantity, @Price)";
    }
}
