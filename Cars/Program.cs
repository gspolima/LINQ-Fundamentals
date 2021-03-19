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
                            .Select(c => c);

            var top = cars.Where(c => c.Manufacturer.ToLower() == "jeep" && c.Combined > 20)
                          .FirstOrDefault();

            var result = cars.Contains(top);
            Console.WriteLine($"Contains? {result}");
            if (top != null)
            {
                Console.WriteLine($"{top.Name} : {top.Combined}");
            }


            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Name} : {car.Combined}");
            }
            Console.WriteLine($"Cars inspected -> {cars.Count()}");
        }

        private static List<Car> ProcessFile(string path)
        {
            var query =
                File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(Car.ParseFromCsv);

            return query.ToList();
        }
    }
}