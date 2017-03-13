using SketchRecognition.Engines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SketchTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InkCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            inkCanvas.Strokes.Clear();
        }

        List<string> labelList = new List<string>();
        ISampleCollector sampleCollector = new SampleCollector();
        private void InkCanvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            addSampleGrid.Visibility = Visibility.Visible;
        }

        private void btnAddSample_Click(object sender, RoutedEventArgs e)
        {
            string lbl = labels.Text;
            var stroke = inkCanvas.Strokes[0].Clone();
            var points = new System.Drawing.Point[stroke.StylusPoints.Count];
            int i = 0;
            foreach (StylusPoint sp in stroke.StylusPoints)
            {
                points[i++] = new System.Drawing.Point((int)sp.X, (int)sp.Y);
            }

            if (!string.IsNullOrEmpty(labels.Text) && labelList.Contains(labels.Text) == false)
                labelList.Add(labels.Text);

            inkCanvas.Strokes.Clear();
            addSampleGrid.Visibility = Visibility.Collapsed;
            labels.ItemsSource = labelList;



            sampleCollector.AddSample(points, lbl);
            if (sampleCollector.DataStore.Samples.Count > 5)
            {
                btnSaveModel.Visibility = Visibility.Visible;
            }
        }

        private void btnSaveModel_Click(object sender, RoutedEventArgs e)
        {
            var fs = new FileStream(Environment.CurrentDirectory+"//HMM.xml", FileMode.Create);
            sampleCollector.SaveSamplesToXML(fs);
            fs.Close();
            btnTrainModel.Visibility = Visibility.Visible;
        }

        private void btnTrainModel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Training.....");
        }
    }
}
