using System;

namespace Cars
{
    public class CarStatistics
    {
        public CarStatistics()
        {
            Min = int.MaxValue;
            Max = int.MinValue;
        }

        public CarStatistics Accumulate(Car car)
        {
            Total += car.Combined;
            Count += 1;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);

            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;

            return this;
        }
        
        public int Max { get; set; }
        public int Min { get; set; }
        public double Average { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
    }
}