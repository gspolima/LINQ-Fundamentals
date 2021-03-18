using System;
using System.Collections.Generic;
using System.Linq;
namespace Cars
{
    public class Car
    {
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public double Displacement { get; set; }
        public int Cylinders { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined { get; set; }

        public static Car ParseFromCsv(string line)
        {
            var column = line.Split(',');
            return new Car
            {
                Year = int.Parse(column[0]),
                Manufacturer = column[1],
                Name = column[2],
                Displacement = double.Parse(column[3]),
                Cylinders = int.Parse(column[4]),
                City = int.Parse(column[5]),
                Highway = int.Parse(column[6]),
                Combined = int.Parse(column[7])
            };
        }
    }
}