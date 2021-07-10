using LanguageExt;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using TurkishPoliticalViewsPredictor.Model;

namespace TurkishPoliticalViewsPredictor
{
    public static class Class1
    {
        public static IEnumerable<PoliticalPersonRaw> LoadData()
        {
            //Create MLContext
            MLContext mlContext = new MLContext();

            //Load Data
            IDataView data = mlContext.Data.LoadFromTextFile<PoliticalPersonRaw>(@"Dataset/data.csv", separatorChar: ',', hasHeader: true);

            return mlContext.Data.CreateEnumerable<PoliticalPersonRaw>(data, reuseRowObject: true);
        }
    }
}
