using System;
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
    class Program
    {
        static void Main(string[] args)
        {
            Exercises.RunExercise1();
            Console.ReadLine();
        }
    }
}
