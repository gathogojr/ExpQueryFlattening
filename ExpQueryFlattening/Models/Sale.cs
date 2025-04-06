namespace ExpQueryFlattening.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Quarter { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public decimal Amount { get; set; }
    }
}
