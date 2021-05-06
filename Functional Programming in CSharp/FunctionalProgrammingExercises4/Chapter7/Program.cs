using System;
using LaYumba.Functional;

namespace Chapter7
{
    class Program
    {
        static void Main(string[] args)
        {
            var greeting = GreeterLib.GreeterMethod("Hello", "Firdez");
            Console.WriteLine("GreeterLib.GreeterMethod('Hello', 'Firdez') = " + greeting);

            var greetingPersonalized = GreeterLib.GreetWith_1("Hello");
            Console.WriteLine("GreeterLib.GreetWith_1('hello') = " + greetingPersonalized("Hüseyin"));

            TypeInference_Delegate typeInference_Delegate = new TypeInference_Delegate();
            var greetingField = typeInference_Delegate.CreateGreetingWith("What's up", "field");
            var greetingProperty = typeInference_Delegate.CreateGreetingWith("What's up", "property");
            var greetingFactory = typeInference_Delegate.CreateGreetingWith("What's up", "factory");
            Console.WriteLine("Field = " + greetingField("Firdez"));
            Console.WriteLine("Propertty = " + greetingField("Hüseyin"));
            Console.WriteLine("Factory = " + greetingField("Ismail"));
            Console.ReadKey();
        }
    }
}
