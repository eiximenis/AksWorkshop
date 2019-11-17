namespace BeerBook.Order.Data
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }
    }
}