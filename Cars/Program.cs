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
                        join manufacturer in manufacturers
                            on new { car.Manufacturer, car.Year }
                            equals new
                            {
                                Manufacturer = manufacturer.Name,
                                manufacturer.Year
                            }
                        orderby car.Combined descending, car.Name ascending
                        select new
                        {
                            car.Name,
                            manufacturer.Headquarters,
                            car.Combined
                        };

            var query2 = cars.Join(
                                manufacturers,
                                (c) => new { c.Manufacturer, c.Year },
                                (m) => new { Manufacturer = m.Name, m.Year },
                                (c, m) => new
                                {
                                    c.Name,
                                    c.Combined,
                                    m.Headquarters
                                })
                            .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name);

            foreach (var line in query2.Take(10))
            {
                Console.WriteLine($"{line.Headquarters} {line.Name} : {line.Combined}");
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