using System;
using System.Globalization;

namespace lab1
{
    public class Program
    {
        private static string GetTriangleType(double a, double b, double c)
        {
            if (a + b <= c || a + c <= b || b + c <= a)
            {
                return "не треугольник";
            }
            else if (a != b && a != c && b != c)
            {
                return "обычный";
            }
            else if (a == b && b == c)
            {
                return "равносторонний";
            }
            else
            {
                return "равнобедренный";
            }
        }

        public static void Main(string[] args)
        {
            const string error = "неизвестная ошибка";

            if (args.Length != 3)
            {
                Console.Write(error);
            }
            else
            {
                try
                {
                    double a = double.Parse(args[0]);
                    double b = double.Parse(args[1]);
                    double c = double.Parse(args[2]);

                    if (double.IsInfinity(a + b) || double.IsInfinity(b + c) || double.IsInfinity(a + c) || 
                        a < 0 || b < 0 || c < 0)
                    {
                        Console.Write(error);
                    }
                    else
                    {
                        Console.Write(GetTriangleType(a, b, c));
                    }
                }
                catch (FormatException)
                {
                    Console.Write(error);
                }
            }
        }
    }
}