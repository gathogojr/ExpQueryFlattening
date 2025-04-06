using System;
using ExpQueryFlattening.EfCore.Data;

namespace ExpQueryFlattening.EfCore.V5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("EF Core 5.x - Flattened");
            Console.WriteLine("---------------------------------------------");
            QueryAnalyzer.Flattened();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("EF Core 5.x - Non-flattened");
            Console.WriteLine("---------------------------------------------");
            //QueryAnalyzer.NonFlattened();
        }
    }
}
