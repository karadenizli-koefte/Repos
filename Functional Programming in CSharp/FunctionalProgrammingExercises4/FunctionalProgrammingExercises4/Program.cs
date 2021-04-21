using FPLibrary;
using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using static FPLibrary.FP;
using static LaYumba.Functional.F;

namespace FunctionalProgrammingExercises4
{
    class Program
    {
        public static int Square(int x) => x * x;
        public static IEnumerable<int> SomeStuff(int x) => new List<int> { x * 1, x * 2, x * 3 };
        public static string GetOptionString<T>(Option<T> option)
            => option.Match(
                Some: t => t.ToString(),
                None: () => "Nothing there.");

        static void Main(string[] args)
        {
            // Exercise 1 - Map for ISet
            Console.WriteLine("Exercise 1");
            ISet<int> hashSet = new HashSet<int> { 1, 2, 3, 4, 5 };
            var result = hashSet.Map(Square);

            Console.WriteLine("Immutable List");
            result.ToImmutableList().ForEach(t => Console.Write(t + " "));

            Console.WriteLine();
            Console.WriteLine("Using ForEach extension");
            result.ForEach(t => Console.Write(t + " "));

            // Exercise 2 - Map for Option
            Console.WriteLine();
            Console.WriteLine("Exercise 2");
            Option<int> opt1 = Some(3);
            Option<int> opt2 = None;

            Console.WriteLine("opt1 = " + opt1);
            Console.WriteLine("opt2 = " + opt2);
            Console.WriteLine("GetOptionString(opt1) = " + GetOptionString(opt1));
            Console.WriteLine("GetOptionString(opt2) = " + GetOptionString(opt2));

            var optSquared1 = opt1.Map2(Square);
            Console.WriteLine("optSquared1 = " + optSquared1);

            var optSquared2 = opt2.Map2(Square);
            Console.WriteLine("optSquared2 = " + optSquared2);

            Console.WriteLine("Bind SomeStuff");
            var resultBind = hashSet.Bind(SomeStuff);
            resultBind.ForEach(t => Console.Write(t + " "));
            Console.WriteLine();

            //Employee employee = new Employee
            //{
            //    Id = "Ismail",
            //    WorkPermit = Some(workPermit)
            //};

            Console.ReadLine();
        }
    }
}
