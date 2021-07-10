using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TurkishPoliticalViewsPredictor;
using TurkishPoliticalViewsPredictor.Model;

namespace TurkishPoliticalOpinionsPrediction
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Class1.LoadData();

            var count = data.Count();
            var row = data.ElementAt(0);
            Console.WriteLine("Number of rows = " +  count);
        }
    }
}
