using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SketchRecognition.Models;
using System.Windows.Ink;
using System.Windows.Input;
using Accord;
using System.IO;
using System.Xml.Serialization;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Fields;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Markov.Topology;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Univariate;

namespace SketchRecognition.Engines
{
    public class SampleCollector : ISampleCollector
    {

        DataStore _dataStore = null;
        private HiddenMarkovClassifier<NormalDistribution, double> _hmm;
        private HiddenConditionalRandomField<double[]> _hcrf;

        public DataStore DataStore
        {
            get
            {
                return _dataStore;
            }

            set
            {
                _dataStore = value;
            }
        }

        public SampleCollector()
        {
            DataStore = new DataStore();
        }

        public void AddSample(System.Drawing.Point[] points, string label)
        {
            DataStore.Add(points, label);
        }

        public void TrainModel(int states = 5, int iterations = 0, double tolerance = 0.01, bool rejection = false)
        {
            var samples = DataStore.Samples;
            var labels = DataStore.Labels;

            double[][][] inputs = new double[samples.Count][][];
            int[] outputs = new int[samples.Count];

            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = samples[i].Input;
                outputs[i] = samples[i].Output;
            }

            _hmm = new HiddenMarkovClassifier<NormalDistribution, double>(labels.Count,
                new Forward(states), new NormalDistribution(2), labels.ToArray());


            // Create the learning algorithm for the ensemble classifier
            var teacher = new HiddenMarkovClassifierLearning<NormalDistribution, double>(_hmm)
            {
                // Train each model until the log-likelihood changes less than 0.001
                Learner = modelIndex => new BaumWelchLearning<NormalDistribution, double>(_hmm.Models[modelIndex])
                {
                    Tolerance = 0.0001,
                    Iterations = 0
                }
            };

            teacher.Empirical = true;
            teacher.Rejection = rejection;


            // Run the learning algorithm
            // double error = teacher.Learn(inputs, outputs);


            // Classify all training instances
            foreach (var sample in samples)
            {
                // sample.RecognizedAs = _hmm.Compute(sample.Input);
            }


        }

        public void DeleteSample(Guid sampleID)
        {
            DataStore.DeleteSample(sampleID);
        }
        public void Clear()
        {
            DataStore.Clear();
        }

        public void SaveSamplesToXML(Stream fs)
        {
            DataStore?.Save(fs);
        }
    }
}
