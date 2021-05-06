using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter7
{
    using Greeting = System.String;
    using Name = System.String;
    using PersonalizedGreeting = System.String;

    public class GreeterLib
    {
        public static PersonalizedGreeting GreeterMethod(Greeting greeting, Name name)
            => $"{greeting}, {name}";

        // Not compiling, because GreeterMethod is a method group and not a function.
        // Func<Name, PersonalizedGreeting> GreetWith(Greeting greeting) => GreeterMethod.Apply(greeting);

        // This alternative is messy and should not be used.
        public static Func<Name, PersonalizedGreeting> GreetWith_1(Greeting greeting)
            => FuncExt.Apply<Greeting, Name, PersonalizedGreeting>(GreeterMethod, greeting);
        public static Func<Name, PersonalizedGreeting> GreetWith_2(Greeting greeting)
            => new Func<Greeting, Name, PersonalizedGreeting>(GreeterMethod).Apply(greeting);
    }

    public class TypeInference_Delegate
    {
        string separator = "! ";

        // 1. field
        Func<Greeting, Name, PersonalizedGreeting> GreeterField
           = (gr, name) => $"{gr}, {name}";

        // 2. property
        Func<Greeting, Name, PersonalizedGreeting> GreeterProperty
           => (gr, name) => $"{gr}{separator}{name}";

        // 3. factory
        Func<Greeting, T, PersonalizedGreeting> GreeterFactory<T>()
           => (gr, t) => $"{gr}, {t}";

        public Func<Name, PersonalizedGreeting> CreateGreetingWith(Greeting greeting, string mode)
        {
            switch(mode)
            {
                case "field":
                    // 1. field
                    return GreeterField.Apply(greeting);
                case "property":
                    // 2. property
                    return GreeterProperty.Apply(greeting);
                case "factory":
                    // 3. factory
                    return GreeterFactory<Name>().Apply(greeting);
                default:
                    throw new NotImplementedException();
            }
        }

    }
}
