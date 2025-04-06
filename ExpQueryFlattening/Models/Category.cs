using System.Collections.Generic;

namespace ExpQueryFlattening.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
