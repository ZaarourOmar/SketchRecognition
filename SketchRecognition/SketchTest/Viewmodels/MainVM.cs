using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchTest.Viewmodels
{
    public class MainVM
    {
        SamplesVM _samplesVM = null;
        public SamplesVM SamplesVM
        {
            get
            {
                return _samplesVM;
            }

            set
            {
                _samplesVM = value;
            }
        }

        public MainVM()
        {
            SamplesVM = new SamplesVM();
        }


        public void AddSample(Sample sample)
        {
            SamplesVM.Samples.Add(sample);
        }

        public void DeleteSample(Guid sampleID)
        {
            Sample s = SamplesVM.Samples.FirstOrDefault(x => x.SampleID == sampleID);
            if (s != null)
            {
                SamplesVM.Samples.Remove(s);
            }
        }

        public void DeleteAllSamples()
        {
            SamplesVM.Samples.Clear();
        }

        public void TrainModel()
        {

        }

        public string Recognize(EntryPointNotFoundException[] pathPoints)
        {
            return "";
        }

        public string[] Recognize(EntryPointNotFoundException[] pathPoints, int topCount)
        {
            return null;
        }

    }
}
