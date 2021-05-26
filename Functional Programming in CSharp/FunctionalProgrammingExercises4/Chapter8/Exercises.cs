using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaYumba.Functional;
using static LaYumba.Functional.F;
namespace Chapter8
{
    public static class Exercises
    {
        // 1.  Implement Apply for Either and Exceptional.

        private static Func<double, double, double> CalculateHypothenuse => (distA, distB) => Math.Sqrt(distA * distA + distB * distB);

        // Normal apply
        private static Func<T2, R> Apply<T1, T2, R>(this Func<T1, T2, R> func, T1 value)
            => t2 => func(value, t2);

        // Apply for Validation
        private static Validation<R> Apply<T, R>(this Validation<Func<T, R>> func, Validation<T> value)
            => func.Match(
                Valid: f => value.Match(
                    Valid: t => Valid(f(t)),
                    Invalid: errVal => Invalid(errVal)),
                Invalid: errFunc => value.Match(
                    Valid: _ => Invalid(errFunc),
                    Invalid: errVal => Invalid(errFunc.Concat(errVal))));

        // Apply for Either
        private static Either<L, R> Apply<L, T, R>(this Either<L, Func<T, R>> func, Either<L, T> value)
            => func.Match(
                Right: r => value.Match<Either<L, R>>(
                    Right: t => Right(r(t)),
                    Left: errVal => Left(errVal)),
                Left: l => value.Match<Either<L, R>>(
                    Right: (_) => Left(l),
                    Left: errVal => Left(errVal)));

        // Apply for Either
        private static Exceptional<R> Apply<T, R>(this Exceptional<Func<T, R>> func, Exceptional<T> value)
            => func.Match(
                Success: f => value.Match<Exceptional<R>>(
                    Success: t => f(t),
                    Exception: errVal => new Exception("Exception, dude!")),
                Exception: err => value.Match<Exceptional<R>>(
                    Success: _ => new Exception("Exception, dude!"),
                    Exception: errVal => new Exception("Exception, dude!")));

        public static void RunExercise1()
        {
            // 4 * 4 + 3 * 3 = 16 + 9 = 25 -> The square root and the length of the hypothenuse is therefore 5.
            var CalculateHypothenuseWithKathete5 = Exercises.CalculateHypothenuse.Apply(4);
            Console.WriteLine("CalculateHypothenuseWithKathete5(3) = " + CalculateHypothenuseWithKathete5(3));
        }
    }
}
