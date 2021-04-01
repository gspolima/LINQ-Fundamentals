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
            var query = from car in db.Cars
                        group car by car.Manufacturer into manufacturersGroup
                        select new
                        {
                            Name = manufacturersGroup.Key,
                            Cars = (from car in manufacturersGroup
                                   orderby car.Combined descending
                                   select car).Take(2)
                        };

            var query2 = db.Cars.GroupBy(c => c.Manufacturer)
                                .Select(g =>
                                    new
                                    {
                                        Name = g.Key,
                                        Cars = g.OrderByDescending(c => c.Combined)
                                                .Take(2)
                                    });

            db.Database.Log = Console.WriteLine;

            foreach (var group in query)
            {
                Console.WriteLine(group.Name);
                foreach (var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Manufacturer} {car.Name} : {car.Combined}");
                }
            }
        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDb();

            //db.Database.Log = Console.WriteLine;

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