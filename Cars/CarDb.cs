using System;
using System.Data.Entity;

namespace Cars
{
    public class CarDb : DbContext
    {
        public DbSet<Car> Cars { get; set; }
    }
}