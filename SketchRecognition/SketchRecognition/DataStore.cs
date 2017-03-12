using Accord;
using SketchRecognition.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SketchRecognition
{
    public class DataStore
    {

        public List<string> Labels { get; private set; }
        public List<Sequence> Samples { get; private set; }


        public DataStore()
        {
            Labels = new List<string>();
            Samples = new List<Sequence>();
        }

        public void Save(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(List<Sequence>));
            serializer.Serialize(stream, Samples);
        }

        public void Load(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(List<Sequence>));
            var samples = (List<Sequence>)serializer.Deserialize(stream);

            Labels.Clear();
            foreach (string label in samples.First().Classes)
                Labels.Add(label);

            Samples.Clear();
            foreach (Sequence sample in samples)
            {
                sample.Classes = Labels;
                Samples.Add(sample);
            }
        }

        internal void DeleteSample(Guid sampleID)
        {
            throw new NotImplementedException();
        }

        public Sequence Add(System.Drawing.Point[] sequence, string classLabel)
        {
            if (sequence == null || sequence.Length == 0)
                return null;

            if (!Labels.Contains(classLabel))
                Labels.Add(classLabel);

            int classIndex = Labels.IndexOf(classLabel);

            Sequence sample = new Sequence()
            {
                Classes = Labels,
                SourcePath = sequence,
                Output = classIndex
            };

            Samples.Add(sample);

            return sample;
        }

        public void Clear()
        {
            Labels.Clear();
            Samples.Clear();
        }

        public int SamplesPerClass()
        {
            int min = 0;
            foreach (string label in Labels)
            {
                int c = Samples.Count(p => p.OutputName == label);

                if (min == 0)
                    min = c;

                else if (c < min)
                    min = c;
            }

            return min;
        }
    }
}
