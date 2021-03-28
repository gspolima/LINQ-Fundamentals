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
                        group car by car.Manufacturer.ToUpper() into manufacturer
                        orderby manufacturer.Key ascending
                        select manufacturer;

            var query2 = cars.GroupBy(c => c.Manufacturer)
                             .OrderBy(g => g.Key);

            foreach (var group in query2)
            {
                Console.WriteLine($"{group.Key}");
                foreach (var car in group.OrderByDescending(r => r.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
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