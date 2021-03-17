using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        public static void Main()
        {
            var numbers = MyLinq.Ramdom().Where(r => r > 0 && r < 1).Take(8);
            
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            var movies = new List<Movie>()
            {
                new Movie { Title = "The Dark Knight", Rating = 8.9f, Year = 2008 },
                new Movie { Title = "The King's Speech", Rating = 8.0f, Year = 2010 },
                new Movie { Title = "Casablanca", Rating = 8.5f, Year = 1942 },
                new Movie { Title = "Star Wars V", Rating = 8.7f, Year = 1980 }
            };

            var query = movies.Where(m => m.Year > 2000)
                              .OrderBy(m => m.Rating);

            var enumerator = query.GetEnumerator();
            while (enumerator.MoveNext() == true)
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }
    }
}