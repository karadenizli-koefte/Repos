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
            Console.WriteLine();

            // Exercise 2 - Map for Option
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

            // Exercise 3 - Work Permit with Bind
            Console.WriteLine("Exercise 3");

            WorkPermit wp = new WorkPermit
            {
                Expiry = new DateTime(2021, 5, 1),
                Number = "10_x"
            };

            var employee1 = new Employee("Ismail", wp, new DateTime(2021, 1, 1), Some(new DateTime(2023, 10, 1)));
            var employee2 = new Employee("Emre", None, new DateTime(2021, 1, 1), Some(new DateTime(2026, 4, 1)));
            var employee3 = new Employee("Wilfried", wp, new DateTime(2021, 1, 1), Some(new DateTime(2024, 12, 1)));
            var employee4 = new Employee("Otto", wp, new DateTime(2021, 1, 1), None);
            var employeeList = new List<Employee> { employee1, employee2, employee3, employee4 };

            var employees = new Dictionary<string, Employee>();
            employees.Add("employee1", employee1);
            employees.Add("employee2", employee2);

            var workPermit = employees.GetWorkPermit("employee1");
            var wpResult = workPermit.Match(Some: t => t.Expiry + " " + t.Number, None: () => "Expired.");
            Console.WriteLine("Work Permit = " + wpResult);
            Console.WriteLine();

            var avgYears = employeeList.AverageYearsWorkedAtTheCompany();
            Console.WriteLine("Average Years = " + avgYears);

            Console.ReadLine();
        }
    }
}
