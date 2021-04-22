using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public static Option<string> Lookup(this NameValueCollection @this, string key)
            => @this[key];

        public static Option<T> Lookup<K, T>(this IDictionary<K, T> dict, K key)
        {
            return dict.TryGetValue(key, out T value)
            ? Some(value) : None;
        }

        public static Option<WorkPermit> GetWorkPermit(this Dictionary<string, Employee> people, string employeeId)
            => people.Lookup(employeeId).Bind(t => t.WorkPermit);

        // 4 Use Bind to implement AverageYearsWorkedAtTheCompany, shown below(only employees who
        // have left should be included).
        public static double AverageYearsWorkedAtTheCompany(this List<Employee> employees) {
            return employees.Bind(employee => employee.LeftOn.Map(leftOn => leftOn.Year - employee.JoinedOn.Year))
                .Average();
        }
    }
}
