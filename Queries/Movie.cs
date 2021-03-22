using System;
using System.Collections.Generic;

namespace Queries
{
    public class Movie
    {
        public string Title { get; set; }
        public float Rating { get; set; }
        int _year;
        public int Year
        {   get
            {
                Console.WriteLine($"Looked at year {_year} in {Title}");
                return _year;
            } 
            set
            {
                _year = value;
            }
        }
    }
}