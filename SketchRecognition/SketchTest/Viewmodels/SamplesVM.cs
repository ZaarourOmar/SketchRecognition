using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchTest.Viewmodels
{
    public class SamplesVM
    {
        public SamplesVM()
        {
            Samples = new ObservableCollection<Sample>();
        }
        ObservableCollection<Sample> _samples;
        public ObservableCollection<Sample> Samples
        {
            get
            {
                return _samples;
            }

            set
            {
                _samples = value;
            }
        }
    }

    public class Sample
    {
        Guid _sampleID = Guid.Empty;
        Point[] _pathPoints = null;
        string _label = "";
        string _tag = "";


        public Sample(Guid ID, Point[] pathPoints, string label, string tag)
        {
            this.SampleID = ID;
            this.PathPoints = pathPoints;
            this.Label = label;
            this.Tag = tag;
        }

        public Guid SampleID
        {
            get
            {
                return _sampleID;
            }

            set
            {
                _sampleID = value;
            }
        }

        public Point[] PathPoints
        {
            get
            {
                return _pathPoints;
            }

            set
            {
                _pathPoints = value;
            }
        }

        public string Label
        {
            get
            {
                return _label;
            }

            set
            {
                _label = value;
            }
        }

        public string Tag
        {
            get
            {
                return _tag;
            }

            set
            {
                _tag = value;
            }
        }

    }
}
