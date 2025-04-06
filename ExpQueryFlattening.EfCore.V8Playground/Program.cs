using ExpQueryFlattening.EfCore.Data;


var db1 = new SalesDbContext();
Console.WriteLine("---------------------------------------------");
Console.WriteLine("EF Core 8.x - Flattened");
Console.WriteLine("---------------------------------------------");
var result1 = db1.Sales
    .GroupBy(d => new { d.Year, d.Quarter })
    .Select(d => new
    {
        d.Key.Year,
        d.Key.Quarter,
        MinProductCategoryName = d.Min(e => e.Product.Category.Name),
        MaxProductCategoryName = d.Max(e => e.Product.Category.Name)
    }).ToArray();

Console.WriteLine("---------------------------------------------");
Console.WriteLine("EF Core 8.x - Non-flattened");
Console.WriteLine("---------------------------------------------");
var db2 = new SalesDbContext();
var result2 = db2.Sales
    .GroupBy(d => new { d.Year, d.Quarter })
    .Select(d => new
    {
        d.Key.Year,
        d.Key.Quarter,
        MinProductCategoryName = d.Min(e => e.Product.Category.Name),
        MaxProductCategoryName = d.Max(e => e.Product.Category.Name)
    }).ToArray();