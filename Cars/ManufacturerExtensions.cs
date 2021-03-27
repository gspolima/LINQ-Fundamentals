using System;
using System.Collections.Generic;

namespace Cars
{
    public static class ManufacturerExtensions
    {
        public static IEnumerable<Manufacturer> ToManufacturer (this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var column = line.Split(',');

                yield return new Manufacturer
                {
                    Name = column[0],
                    Headquarters = column[1],
                    Year = int.Parse(column[2])
                };
            }
        }
    }
}