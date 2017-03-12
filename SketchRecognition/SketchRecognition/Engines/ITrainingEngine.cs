using Accord;
using SketchRecognition.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
namespace SketchRecognition.Engines
{
    public interface ISampleCollector
    {
        void TrainModel(int states = 5, int iterations = 0, double tolerance = 0.01, bool rejection = false);
        void Clear();
        void AddSample(System.Drawing.Point[] points, string label);
        void DeleteSample(Guid seqID);

        DataStore DataStore { get; set; }

        void SaveSamplesToXML(Stream fs);
    }
}
