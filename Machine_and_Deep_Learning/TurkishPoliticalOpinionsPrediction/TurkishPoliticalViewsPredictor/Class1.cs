using LanguageExt;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TurkishPoliticalViewsPredictor.Model;

namespace TurkishPoliticalViewsPredictor
{
    public static class Class1
    {
        private static string _appPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private static string _trainDataPath => Path.Combine(_appPath, "Data", "data.csv");
        private static string _testDataPath => Path.Combine(_appPath, "Data", "datav2.csv"); 
        private static string _modelPath => Path.Combine(_appPath, "..", "..", "..", "..", "TurkishPoliticalViewsPredictor", "Models", "model.zip");

        private static MLContext _mlContext;
        private static PredictionEngine<PoliticalPersonRaw, ModelOutput> _predEngine;
        private static ITransformer _trainedModel;

        public static IDataView LoadData(string path)
        {
            //Create MLContext
            MLContext mlContext = new MLContext();

            //Load Data
            return mlContext.Data.LoadFromTextFile<PoliticalPersonRaw>(path, separatorChar: ',', hasHeader: true);
        }

        public static IEstimator<ITransformer> ProcessData()
        {
            //Create MLContext
            _mlContext = new MLContext();

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Party", outputColumnName: "Label")
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Sex", outputColumnName: "SexFeaturized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Age", outputColumnName: "AgeFeaturized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Area", outputColumnName: "AreaFeaturized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "EducationLevel", outputColumnName: "EducationLevelFeaturized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Question1", outputColumnName: "Question1Featurized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Question2", outputColumnName: "Question2Featurized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Question3", outputColumnName: "Question3Featurized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Question4", outputColumnName: "Question4Featurized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Question5", outputColumnName: "Question5Featurized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Question6", outputColumnName: "Question6Featurized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Question7", outputColumnName: "Question7Featurized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Question8", outputColumnName: "Question8Featurized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Question9", outputColumnName: "Question9Featurized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Question10", outputColumnName: "Question10Featurized"))
                .Append(_mlContext.Transforms.Concatenate("Features", "SexFeaturized", "AgeFeaturized", "AreaFeaturized", "EducationLevelFeaturized", 
                "Question1Featurized", "Question2Featurized", "Question3Featurized", "Question4Featurized", "Question5Featurized", "Question6Featurized", 
                "Question7Featurized", "Question8Featurized", "Question9Featurized", "Question10Featurized"))
                .AppendCacheCheckpoint(_mlContext);

            return pipeline;
        }

        public static IEstimator<ITransformer> BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
        {
            var trainingPipeline = pipeline.Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            _trainedModel = trainingPipeline.Fit(trainingDataView);

            _predEngine = _mlContext.Model.CreatePredictionEngine<PoliticalPersonRaw, ModelOutput>(_trainedModel);

            var person = new PoliticalPersonRaw()
            {
                // Erkek,18-30,Marmara,Lisans,Hayır,Evet,Evet,Hayır,Hayır,Hayır,Evet,Hayır,Evet,Evet,IYI PARTI
                Sex = "Erkek",
                Age = "18-30",
                Area = "Marmara",
                EducationLevel = "Lisans",
                Question1 = "Hayır",
                Question2 = "Evet",
                Question3 = "Evet",
                Question4 = "Hayır",
                Question5 = "Hayır",
                Question6 = "Hayır",
                Question7 = "Evet",
                Question8 = "Hayır",
                Question9 = "Evet",
                Question10 = "Evet"
            };


            var prediction = _predEngine.Predict(person);
            Console.WriteLine($"Prediction: {prediction.Prediction}");

            // Show score of each area ordered by score desc
            var scoreEntries = GetScoresWithLabelsSorted(_predEngine.OutputSchema, "Score", prediction.Score);
            foreach (var scoreEntry in scoreEntries)
            {
                Console.WriteLine($"Party: {scoreEntry.Key} Score: {scoreEntry.Value * 100}%");
            }

            var person2 = new PoliticalPersonRaw("Erkek", "18-30", "Marmara", "Lisans", "Evet", "Evet", "Hayır", "Evet", "Hayır", "Evet", "Hayır", "Evet", "Hayır", "Hayır"/*, "AKP"*/);
            var prediction2 = _predEngine.Predict(person2);
            Console.WriteLine($"Prediction2: {prediction2.Prediction}");

            // Show score of each area ordered by score desc
            var scoreEntries2 = GetScoresWithLabelsSorted(_predEngine.OutputSchema, "Score", prediction2.Score);
            foreach (var scoreEntry in scoreEntries2)
            {
                Console.WriteLine($"Party: {scoreEntry.Key} Score: {scoreEntry.Value * 100}%");
            }

            return trainingPipeline;
        }

        public static void Evaluate(DataViewSchema trainingDataViewSchema)
        {
            // STEP 5:  Evaluate the model in order to get the model's accuracy metrics
            Console.WriteLine($"=============== Evaluating to get model's accuracy metrics - Starting time: {DateTime.Now.ToString()} ===============");

            //Load the test dataset into the IDataView
            // <SnippetLoadTestDataset>
            var testDataView = _mlContext.Data.LoadFromTextFile<PoliticalPersonRaw>(_testDataPath, hasHeader: true);
            // </SnippetLoadTestDataset>

            //Evaluate the model on a test dataset and calculate metrics of the model on the test data.
            // <SnippetEvaluate>
            var testMetrics = _mlContext.MulticlassClassification.Evaluate(_trainedModel.Transform(testDataView));
            // </SnippetEvaluate>

            Console.WriteLine($"=============== Evaluating to get model's accuracy metrics - Ending time: {DateTime.Now.ToString()} ===============");
            // <SnippetDisplayMetrics>
            Console.WriteLine($"*************************************************************************************************************");
            Console.WriteLine($"*       Metrics for Multi-class Classification model - Test Data     ");
            Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"*       MicroAccuracy:    {testMetrics.MicroAccuracy:0.###}");
            Console.WriteLine($"*       MacroAccuracy:    {testMetrics.MacroAccuracy:0.###}");
            Console.WriteLine($"*       LogLoss:          {testMetrics.LogLoss:#.###}");
            Console.WriteLine($"*       LogLossReduction: {testMetrics.LogLossReduction:#.###}");
            Console.WriteLine($"*************************************************************************************************************");
            // </SnippetDisplayMetrics>

            // Save the new model to .ZIP file
            // <SnippetCallSaveModel>
            SaveModelAsFile(_mlContext, trainingDataViewSchema, _trainedModel);
            // </SnippetCallSaveModel>
        }

        public static void PredictIssue()
        {
            // <SnippetLoadModel>
            ITransformer loadedModel = _mlContext.Model.Load(_modelPath, out var modelInputSchema);
            // </SnippetLoadModel>

            // <SnippetAddTestIssue>
            var person = new PoliticalPersonRaw()
            {
                // Erkek,18-30,Marmara,Lisans,Hayır,Evet,Evet,Hayır,Hayır,Hayır,Evet,Hayır,Evet,Evet,IYI PARTI
                Sex = "Erkek",
                Age = "18-30",
                Area = "Marmara",
                EducationLevel = "Lisans",
                Question1 = "Hayır",
                Question2 = "Evet",
                Question3 = "Evet",
                Question4 = "Hayır",
                Question5 = "Hayır",
                Question6 = "Hayır",
                Question7 = "Evet",
                Question8 = "Hayır",
                Question9 = "Evet",
                Question10 = "Evet"
            };
            // </SnippetAddTestIssue>

            //Predict label for single hard-coded issue
            // <SnippetCreatePredictionEngine>
            _predEngine = _mlContext.Model.CreatePredictionEngine<PoliticalPersonRaw, ModelOutput>(loadedModel);
            // </SnippetCreatePredictionEngine>

            // <SnippetPredictIssue>
            var prediction = _predEngine.Predict(person);
            // </SnippetPredictIssue>

            // <SnippetDisplayResults>
            Console.WriteLine($"=============== Single Prediction - Result: {prediction.Prediction} ===============");
            // </SnippetDisplayResults>
        }

        private static void SaveModelAsFile(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
        {
            // <SnippetSaveModel>
            mlContext.Model.Save(model, trainingDataViewSchema, _modelPath);
            // </SnippetSaveModel>

            Console.WriteLine("The model is saved to {0}", _modelPath);
        }

        private static Dictionary<string, float> GetScoresWithLabelsSorted(DataViewSchema schema, string name, float[] scores)
        {
            Dictionary<string, float> result = new Dictionary<string, float>();

            var column = schema.GetColumnOrNull(name);

            var slotNames = new VBuffer<ReadOnlyMemory<char>>();
            column.Value.GetSlotNames(ref slotNames);
            var names = new string[slotNames.Length];
            var num = 0;
            foreach (var denseValue in slotNames.DenseValues())
            {
                result.Add(denseValue.ToString(), scores[num++]);
            }

            return result.OrderByDescending(c => c.Value).ToDictionary(i => i.Key, i => i.Value);
        }
    }
}
