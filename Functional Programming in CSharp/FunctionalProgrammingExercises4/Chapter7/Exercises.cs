using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LaYumba.Functional.F;
using Unit = System.ValueTuple;

namespace Chapter7
{
    /*
     * 1. Partial application with a binary arithmetic function:
     *      1. Write a function, Remainder, that calculates the remainder of integer division (and
     *      works for negative input values!). Notice how the expected order of parameters isn’t 
     *      the one that’s most likely to be required by partial application (you’re more likely to
     *      partially apply the divisor).
     *      
     *      2. Write an ApplyR function that gives the rightmost parameter to a given binary
     *      function. (Try to do so without looking at the implementation for Apply.) Write the
     *      signature of ApplyR in arrow notation, both in curried and non-curried forms.
     *      
     *      3. Use ApplyR to create a function that returns the remainder of dividing any number by 5.
     *      
     *      4. Write an overload of ApplyR that gives the rightmost argument to a ternary function.
     * 
     * 2. Ternary functions:
     *      1. Define a PhoneNumber class with three fields: number type (home, mobile, ...), country
     *      code (‘it’, ‘uk’, ...), and number. CountryCode should be a custom type with implicit
     *      conversion to and from string.
     *      
     *      2. Define a ternary function that creates a new number, given values for these fields.
     *      What’s the signature of your factory function?
     *      
     *      3. Use partial application to create a binary function that creates a UK number, and
     *      then again to create a unary function that creates a UK mobile.
     *      
     * 3. Functions everywhere. You may still have a feeling that objects are ultimately more
     *    powerful than functions. Surely a logger object should expose methods for related
     *    operations such as Debug, Info, and Error? To see that this is not necessarily so, challenge
     *    yourself to write a very simple logging mechanism (logging to the console is fine) that
     *    doesn’t require any classes or structs. You should still be able to inject a Log value into a
     *    consumer class or function, exposing the operations Debug, Info, and Error, like so:
     *    void ConsumeLog(Log log)
     *    => log.Info("look! no classes!");
     *    
     * 4. Open exercise: in your day-to-day coding, start paying more attention to the signatures of
     *    the functions you write and consume. Does the order of arguments make sense; that is, do
     *    they go from general to specific? Is there some argument that you always invoke with the
     *    same value, so that you could partially apply it? Do you sometimes write similar variations
     *    of the same code, and could these be generalized into a parameterized function?
     * 
     * 5. Implement Map, Where, and Bind for IEnumerable in terms of Aggregate.
     */

    public static class Exercises
    {
        public static Func<int, int, int> Remainder = (dividend, divisor)
         => dividend - ((dividend / divisor) * divisor);

        public static Func<T1, R> ApplyR<T1, T2, R>(this Func<T1, T2, R> func, T2 t2)
            => (t1) => func(t1, t2);

        public static Func<T1, T2, R> ApplyR<T1, T2,T3, R>(this Func<T1, T2, T3, R> func, T3 t3)
            => (t1, t2) => func(t1, t2, t3);

        public static Unit RunExercise1()
        {
            var remainderBy5 = Remainder.Apply(5);
            Console.WriteLine("remainderBy5(15) = " + remainderBy5(15));
            Console.WriteLine("remainderBy5(22) = " + remainderBy5(22));
            Console.WriteLine("remainderBy5(3) = " + remainderBy5(3));
            Console.WriteLine("remainderBy5(-3) = " + remainderBy5(-3));

            return new Unit();
        }

        public static Unit RunExercise2()
        {
            var remainderBy5 = Remainder.Apply(5);
            Console.WriteLine("remainderBy5(15) = " + remainderBy5(15));
            Console.WriteLine("remainderBy5(22) = " + remainderBy5(22));
            Console.WriteLine("remainderBy5(3) = " + remainderBy5(3));
            Console.WriteLine("remainderBy5(-3) = " + remainderBy5(-3));

            return new Unit();
        }
    }
}
