using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");

            var query = cars.Where(c => c.Manufacturer.ToLower() == "toyota" && c.Year == 2016)
                            .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name)
                            .Select(c => new { c.Name, 
                                               c.Manufacturer,
                                               c.Year,
                                               c.Combined });
            //---
            var top = cars.Where(c => c.Manufacturer.ToLower() == "ford" && c.Combined > 20)
                          .FirstOrDefault();

            var contains = cars.Contains(top);
            Console.WriteLine($"Contains? {contains}");
            if (top != null)
            {
                Console.WriteLine($"{top.Name} : {top.Combined}");
            }
            //---
            var result = cars.SelectMany(c => c.Name)
                             .OrderBy(c => c);

            foreach (var character in result)
            {
                Console.WriteLine(character);
            }
            //---
            foreach (var car in query.Take(5))
            {
                Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined}");
            }
            Console.WriteLine($"Cars inspected -> {cars.Count()}");
        }

        private static List<Car> ProcessFile(string path)
        {
            var query =
                File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .ToCar();

            return query.ToList();
        }
    }
}