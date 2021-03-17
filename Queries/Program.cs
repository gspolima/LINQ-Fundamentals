using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        public static void Main()
        {
            var movies = new List<Movie>()
            {
                new Movie { Title = "The Dark Knight", Rating = 8.9f, Year = 2008 },
                new Movie { Title = "The King's Speech", Rating = 8.0f, Year = 2010 },
                new Movie { Title = "Casablanca", Rating = 8.5f, Year = 1942 },
                new Movie { Title = "Star Wars V", Rating = 8.7f, Year = 1980 }
            };
            // building the structure, but it won't do anything until
            // some code triggers LINQ to perform the query.
            var query = movies.Where(m => m.Year > 2000).ToList();

            Console.WriteLine($"{query.Count()} <- Movies that match the condition");

            var enumerator = query.GetEnumerator();
            while (enumerator.MoveNext() == true)
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }
    }
}