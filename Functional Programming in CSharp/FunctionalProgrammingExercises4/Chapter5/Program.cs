using System;

namespace Chapter5
{
    public class Program
    {
        static double Square(double x) => x * x;
        static double SquareRoot(double x) => Math.Sqrt(x);
        static double Double(double x) => x * 2;
        static Func<T1, R> Compose<T1, T2, R>(Func<T2, R> f1, Func<T1, T2> f2)
            => (t) => f1(f2(t));

        public static void Main(string[] args)
        {
            var x = Compose<double, double, double>(Double, SquareRoot);
            Console.WriteLine(x(4));
            Console.WriteLine(x(9));
            Console.WriteLine(x(16));
        }
    }
}
