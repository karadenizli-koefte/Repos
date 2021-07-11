using Microsoft.ML;
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
            var _trainingDataView = Class1.LoadData(@"D:\_Repos\Machine_and_Deep_Learning\TurkishPoliticalOpinionsPrediction\TurkishPoliticalViewsPredictor\Data\data.csv");

            MLContext mlContext = new MLContext();
            var list = mlContext.Data.CreateEnumerable<PoliticalPersonRaw>(_trainingDataView, reuseRowObject: true);

            var pipeline = Class1.ProcessData();

            var trainingPipeline = Class1.BuildAndTrainModel(_trainingDataView, pipeline);

            Class1.Evaluate(_trainingDataView.Schema);

            Class1.PredictIssue();
            
        }
    }
}
