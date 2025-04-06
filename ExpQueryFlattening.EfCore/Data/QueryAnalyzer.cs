using System.Linq;

namespace ExpQueryFlattening.EfCore.Data
{
    public static class QueryAnalyzer
    {
        public static void NonFlattened()
        {
            var db = new SalesDbContext();
            var result = db.Sales
                .GroupBy(d => new { d.Year, d.Quarter })
                .Select(d => new
                {
                    d.Key.Year,
                    d.Key.Quarter,
                    AverageProductTaxRate = d.Average(e => e.Product.TaxRate),
                    SumProductTaxRate = d.Sum(e => e.Product.TaxRate)
                }).ToArray();
        }

        public static void Flattened()
        {
            var db = new SalesDbContext();
            var result = db.Sales
                .Select(d => new
                {
                    d.Year,
                    d.Quarter,
                    ProductTaxRate = d.Product.TaxRate
                })
                .GroupBy(d => new { d.Year, d.Quarter })
                .Select(d => new
                {
                    d.Key.Year,
                    d.Key.Quarter,
                    AverageProductTaxRate = d.Average(e => e.ProductTaxRate),
                    SumProductTaxRate = d.Sum(e => e.ProductTaxRate)
                }).ToArray();
        }
    }
}
