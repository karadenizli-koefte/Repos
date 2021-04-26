using System;

namespace Chapter5
{
    public class Program
    {
        public double Square(double x) => x * x;
        public double SquareRoot(double x) => Math.Sqrt(x);
        public double Double(double x) => x * 2;
        public Func<T1, R2> Compose<T1, T2, R1, R2>(Func<T2, R2> f1, Func<T1, T2> f2)
            => (t) => f1(f2(t));

        public static void Main(string[] args)
        {
            Program p = new Program();
            var x = p.Compose<double, double, double, double>(p.Double, p.SquareRoot);
            Console.WriteLine(x(4));
            Console.WriteLine(x(9));
            Console.WriteLine(x(16));
        }
    }
}
