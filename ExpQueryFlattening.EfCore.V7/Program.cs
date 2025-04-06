using ExpQueryFlattening.EfCore.Data;

Console.WriteLine("---------------------------------------------");
Console.WriteLine("EF Core 7.x - Flattened");
Console.WriteLine("---------------------------------------------");
QueryAnalyzer.Flattened();
Console.WriteLine("---------------------------------------------");
Console.WriteLine("EF Core 7.x - Non-flattened");
Console.WriteLine("---------------------------------------------");
QueryAnalyzer.NonFlattened();