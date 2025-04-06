using System.Collections.Generic;

namespace ExpQueryFlattening.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal TaxRate { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
