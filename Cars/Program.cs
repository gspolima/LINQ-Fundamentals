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
            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query = from car in cars
                        group car by car.Manufacturer
                        into carGroup
                        select new
                        {
                            Name = carGroup.Key,
                            Max = carGroup.Max(c => c.Combined),
                            Min = carGroup.Min(c => c.Combined),
                            Average = carGroup.Average(c => c.Combined)
                        }
                        into result
                        orderby result.Max descending
                        select result;

            var query2 =
                cars.GroupBy(c => c.Manufacturer)
                    .Select(g => 
                    {
                        var results =
                                g.Aggregate(new CarStatistics(),
                                        (acc, c) => acc.Accumulate(c),
                                        (acc) => acc.Compute());
                        return new
                        {
                            Name = g.Key,
                            Max = results.Max,
                            Min = results.Min,
                            Average = results.Average,
                        };
                    })
                    .OrderByDescending(r => r.Max);

            foreach (var result in query2)
            {
                Console.WriteLine($"{result.Name}");
                Console.WriteLine($"\t Max: {result.Max}");
                Console.WriteLine($"\t Min: {result.Min}");
                Console.WriteLine($"\t Avg: {result.Average:N1}");
            }
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                    File.ReadAllLines(path)
                        .Where(line => line.Length > 1)
                        .ToManufacturer();

            return query.ToList();
        }

        private static List<Car> ProcessCars(string path)
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