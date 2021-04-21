using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Linq;

using static LaYumba.Functional.F;

namespace FPLibrary
{
    public static partial class FP
    {
        public static void ForEach<T>(this ISet<T> ts, Action<T> action)
            => ts.ToList().ForEach(t => action(t));

        public static ISet<R> Map<T, R>(this ISet<T> ts, Func<T, R> func)
            => ts.Select(t => func(t)).ToHashSet();

        public static Option<R> Map2<T, R>(this Option<T> opt, Func<T, R> func)
            => opt.Match(
                Some: t => Some(func(t)),
                None: () => None);

        public static Option<R> MapInTermsOfBindAndReturn<T, R>(this Option<T> opt, Func<T, R> func)
            => opt.Bind(t => Some(func(t)));

        public static IEnumerable<R> MapInTermsOfBindAndReturn<T, R>(this IEnumerable<T> list, Func<T, R> func)
            => list.Bind(t => new List<R> { func(t) });

        // This is the better solution, because above only List is handled and not other implementations of IEnumerable.
        // The method below used the layumba method List to convert func(t) to the correct IEnumerable implementation.
        //public static IEnumerable<R> Map<T, R>(this IEnumerable<T> ts, Func<T, R> f)
        // => ts.Bind(t => List(f(t)));




        // 2 Write a Lookup function that will take an IEnumerable and a predicate, and
        // return the first element in the IEnumerable that matches the predicate, or None
        // if no matching element is found. Write its signature in arrow notation:

        // bool isOdd(int i) => i % 2 == 1;
        // new List<int>().Lookup(isOdd) // => None
        // new List<int> { 1 }.Lookup(isOdd) // => Some(1)

        // Lookup : IEnumerable<T> -> (T -> bool) -> Option<T>
        public static Option<T> Lookup<T>(this IEnumerable<T> ts, Func<T, bool> pred)
        {
            foreach (T t in ts) if (pred(t)) return Some(t);
            return None;
        }

        public static Option<WorkPermit> GetWorkPermit(Dictionary<string, Employee> people, string employeeId)
            => Lookup(people, t => t.Key == employeeId).Bind(t => t.Value.WorkPermit);
    }
}
