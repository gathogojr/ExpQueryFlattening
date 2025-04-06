using ExpQueryFlattening.EfCore.Data;

Console.WriteLine("---------------------------------------------");
Console.WriteLine("EF Core 6.x - Flattened");
Console.WriteLine("---------------------------------------------");
QueryAnalyzer.Flattened();
Console.WriteLine("---------------------------------------------");
Console.WriteLine("EF Core 6.x - Non-flattened");
Console.WriteLine("---------------------------------------------");
QueryAnalyzer.NonFlattened();