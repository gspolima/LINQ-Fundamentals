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
                        into carsGroup
                        select new
                        {
                            Manufacturer = manufacturer,
                            Cars = carsGroup
                        }
                        into result
                        group result by result.Manufacturer.Headquarters;

            var query2 =
                manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer,
                                    (m, g) =>
                                    new
                                    {
                                        Manufacturer = m,
                                        Cars = g
                                    })
                             .GroupBy(r => r.Manufacturer.Headquarters);

            foreach (var group in query2)
            {
                Console.WriteLine($"{group.Key}");

                foreach (var car in group.SelectMany(g => g.Cars)
                                         .OrderByDescending(c => c.Combined)
                                         .Take(3))
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