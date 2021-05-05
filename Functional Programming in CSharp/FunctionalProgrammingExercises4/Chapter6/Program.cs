using LaYumba.Functional;
using System;
using FPLibrary;
using static LaYumba.Functional.F;

namespace Chapter6
{
    public static class Program
    {
        static string SomeErrorMessage() => "Error Message";
        static Option<double> DevideBy(this double x, double y) => (y == 0) ? None : Some(x / y);
        static Option<double> Sqrt(this double number) => (number < 0) ? None : Math.Sqrt(number);
        static Either<string, int> Parse(this string s) => int.TryParse(s, out int result) ? Right(result) : Left("Parse unsuccessful.");
        static Func<string, Option<Age>> parseAge = s => Parse(s).Bind(Age.Of);
        // TryRun : (() → T) → Exceptional<T>
        static Exceptional<T> TryRun<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch(Exception ex)
            {
                return ex;
            }
        }

        static int func1() => throw new NotImplementedException(); 
        static int func2() => 5;

        // Safely : ((() → R), (Exception → L)) → Either<L, R>
        static Either<L, R> Safely<L, R>(Func<R> func, Func<Exception, L> funcEx)
        {
            try
            {
                return func();
            }
            catch(Exception ex)
            {
                return funcEx(ex);
            }
        }

        static string GetErrorMessage(Exception ex) => "Wrong: " + ex.Message;

        static void Main(string[] args)
        {
            // Exercise 1
            Option<int> some = Some(54);
            Option<int> none = None;
            Either<string, int> left = Left("Sorry, this is wrong!");
            Either<string, int> right = Right(777);

            Console.WriteLine("some.ToEither('Duck') = " + some.ToEither("Duck"));
            Console.WriteLine("none.ToEither('Duck') = " + none.ToEither("Duck"));
            Console.WriteLine("some.ToEither(SomeErrorMessage) = " + some.ToEither(SomeErrorMessage));
            Console.WriteLine("none.ToEither(SomeErrorMessage) = " + none.ToEither(SomeErrorMessage));
            Console.WriteLine("left.ToOption() = " + left.ToOption());
            Console.WriteLine("right.ToOption() = " + right.ToOption());


            // Exercise 2
            var x = 3.0.DevideBy(3).Bind(Sqrt);
            var y = (-3.0).DevideBy(3).Bind(Sqrt);
            var z = (3.0).DevideBy(0).Bind(Sqrt);

            Console.WriteLine("x (3) = " + x);
            Console.WriteLine("y (-3) = " + y);
            Console.WriteLine("z (0) = " + z);

            Console.WriteLine("Parse Age 5 = " + parseAge("5"));
            Console.WriteLine("Parse Age -5 = " + parseAge("-5"));
            Console.WriteLine("Parse Age otto = " + parseAge("otto"));

            // Exercise 3
            var result = TryRun(func1).Match(
                Success:   s => Console.WriteLine("TryRun(func1) = " + s),
                Exception: e => Console.WriteLine("TryRun(func1) = " + e.Message));

            var result2 = TryRun(func2).Match(
                Success:   s => Console.WriteLine("TryRun(func2) = " + s),
                Exception: e => Console.WriteLine("TryRun(func2) = " + e.Message));

            // Exercise 4
            var resultSafely = Safely(func1, GetErrorMessage).Match(
                Right: s => Console.WriteLine("Safely(func1, GetErrorMessage) = " + s),
                Left:  e => Console.WriteLine("Safely(func1, GetErrorMessage) = " + e));

            var resultSafely2 = Safely(func2, GetErrorMessage).Match(
                Right: s => Console.WriteLine("Safely(func2, GetErrorMessage) = " + s),
                Left:  e => Console.WriteLine("Safely(func2, GetErrorMessage) = " + e));

            Console.ReadKey();
        }
    }
}
