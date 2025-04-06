using System.Collections.Generic;

namespace ExpQueryFlattening.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
