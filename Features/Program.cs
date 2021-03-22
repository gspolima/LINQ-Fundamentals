using System;
using System.Collections.Generic;
//using Features.Linq;
using System.Linq;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> square = x => x * x;
            Func<int, int, int> add = (x, y) => x + y;
            Action<int> display = x => Console.WriteLine(x);

            display((square(add(3, 86))));

            var developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 2, Name = "Gustavo" },
                new Employee { Id = 3, Name = "Steve" }
            };

            var query = developers.Where(e => e.Name.Length == 5)
                                  .OrderByDescending(e => e.Name);

            var query2 = from employee in developers
                         where employee.Name.Length == 5
                         orderby employee.Name descending 
                         select employee;

            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }

            var words = new Dictionary<int, string>()
            {
                { 1, "Code" },
                { 2, "Deploy" }
            };

            var enumerator = words.GetEnumerator();
            for (bool hasItem = true; hasItem == true;)
            {
                Console.WriteLine(enumerator.Current.Value);
                hasItem = enumerator.MoveNext();
            }
        }
    }
}