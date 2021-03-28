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
                        group car by manufacturer.Headquarters into countriesGroup
                        orderby countriesGroup.Key
                        select new
                        {
                            Cars = countriesGroup
                        };

            foreach (var group in query)
            {
                Console.WriteLine($"{group.Cars.Key}");
                foreach (var car in group.Cars.OrderByDescending(r => r.Combined).Take(3))
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