using LaYumba.Functional;
using System;
using static LaYumba.Functional.F;

namespace FPLibrary
{
    public static class EitherExtensions
    {
        public static Option<R> ToOption<L, R>(this Either<L, R> either)
            => either.Match(
                l => None,
                r => Some(r));

        public static Either<L, R> ToEither<L, R>(this Option<R> opt, L left)
            => opt.Match<Either<L, R>>(
                () => Left(left),
                r => Right(r));

        public static Either<L, R> ToEither<L, R>(this Option<R> opt, Func<L> left)
            => opt.Match<Either<L, R>>(
                () => left(),
                r => r);

        public static Option<RR> Bind<L, R, RR>(this Either<L, R> either, Func<R, Option<RR>> func)
            => either.Match(
                Left: l => None,
                Right: r => func(r));

        public static Option<RR> Bind<L, R, RR>(this Option<R> opt, Func<R, Either<L, RR>> func)
            => opt.Match(
                None: () => None,
                Some: s => func(s).ToOption());
    }
}
