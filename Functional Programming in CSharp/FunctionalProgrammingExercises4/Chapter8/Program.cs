using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace Chapter8
{
    /*
        1.  Implement Apply for Either and Exceptional.
        2.  Implement the query pattern for Either and Exceptional. Try to write down the signatures
            for Select and SelectMany without looking at any examples. For the implementation, just
            follow the types—if it type checks, it’s probably right!
        3.  Come up with a scenario in which various Either-returning operations are chained with
            Bind. (If you’re short of ideas, you can use the favorite-dish example from chapter 6.)
            Rewrite the code using a LINQ expression.
     */
    public sealed record Person
    {
        public Person(string firstName, string lastName, ImmutableList<int> numbers)
        {
            FirstName = firstName;
            LastName = lastName;
            Numbers = numbers;
        }

        public string FirstName { get; init; }
        public string LastName { get; init; }
        public ImmutableList<int> Numbers { get; init; }

        //public bool Equals(Person? other) => (other != null 
        //                                      && this.FirstName == other.FirstName
        //                                      && this.LastName == other.LastName
        //                                      && this.Numbers.Count == other.Numbers.Count
        //                                      && this.Numbers.SelectMany((number, i) => 
        //                                            other.Numbers
        //                                                .Where((otherNumber, i2) => i == i2)
        //                                                .Select((otherNumber) => number == otherNumber)).All(t => t == true)
        //                          );
        public bool Equals(Person? other) => (other != null
                                              && this.FirstName == other.FirstName
                                              && this.LastName == other.LastName
                                              && this.Numbers.Count == other.Numbers.Count
                                              && this.Numbers.SequenceEqual(other.Numbers)
                                  );
    } 

    class Program
    {
        static void Main(string[] args)
        {
            Exercises.RunExercise1();

            Console.WriteLine();
            var numbers = ImmutableList.Create(123, 456, 789);
            var p  = new Person("Firdez", "Efe", numbers);
            var p1 = new Person("Firdez", "Efe", numbers);
            var p2 = p1 with { FirstName = "Hüseyin", Numbers = ImmutableList.Create(123, 456, 789) };

            Console.WriteLine(p.ToString());
            Console.WriteLine(p2.ToString());

            Console.WriteLine("p == p1 = " + (p == p1));
            Console.WriteLine("p.Equals(p1) = " + p.Equals(p1));

            Console.ReadLine();
        }
    }
}
