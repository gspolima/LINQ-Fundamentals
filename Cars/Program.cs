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

            var query = from manufacturer in manufacturers
                        join car in cars
                            on manufacturer.Name equals car.Manufacturer
                        into carGroup
                        orderby manufacturer.Name ascending
                        select new
                        {
                            Manufacturer = manufacturer,
                            Cars = carGroup
                        };

            var query2 = manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer, (m, g) =>
                        new
                        {
                            Manufacturer = m,
                            Cars = g
                        })
                        .OrderBy(r => r.Manufacturer.Name);

            foreach (var group in query2)
            {
                Console.WriteLine($"{group.Manufacturer.Name} from {group.Manufacturer.Headquarters}");
                foreach (var car in group.Cars.OrderByDescending(r => r.Combined).Take(2))
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