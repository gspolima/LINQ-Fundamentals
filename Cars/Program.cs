using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace Cars
{
    public class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());
            InsertData();
            QueryData();
        }

        private static void QueryData()
        {
            var db = new CarDb();
            var query = db.Cars.OrderByDescending(c => c.Combined)
                               .ThenBy(c => c.Name)
                               .Take(10)
                               .Select(c => new
                               {
                                   Name = c.Name,
                                   Manufacturer = c.Manufacturer,
                                   Combined = c.Combined
                               });

            db.Database.Log = Console.WriteLine;

            foreach (var car in query)
            {
                Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined}");
            }
        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDb();

            db.Database.Log = Console.WriteLine;
            
            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
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