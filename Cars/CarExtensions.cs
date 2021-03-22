using System;
using System.Collections.Generic;

namespace Cars
{
    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar (this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var column = line.Split(',');
                
                yield return new Car
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
}