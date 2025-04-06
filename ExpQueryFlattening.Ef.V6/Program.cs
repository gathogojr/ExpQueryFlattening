using System;
using System.Linq;
using ExpQueryFlattening.Ef.V6.Data;

namespace ExpQueryFlattening.Ef.V6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("EF6 - Flattened");
            Console.WriteLine("---------------------------------------------");
            Flattened();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("EF6 - Non-flattened");
            Console.WriteLine("---------------------------------------------");
            NonFlattened();
        }

        static void NonFlattened()
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

        static void Flattened()
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
