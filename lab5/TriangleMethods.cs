using System;

namespace Lab5
{

    sealed partial class Triangle
    {
        public void PrintSides()
        {
            Console.WriteLine($"Трикутник: a={a}, b={b}, c={c}, колір={color}");
        }

        public double GetArea()
        {
            double p = (a + b + c) / 2.0;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
    }
}