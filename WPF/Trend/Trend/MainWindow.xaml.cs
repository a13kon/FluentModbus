using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;
using ScottPlot;

namespace Trend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<double> dataX = new List<double>();
            List<double> dataY = new List<double>();
            List<double> dataZ = new List<double>();
            double counter = 0.0f;
            bool start = true;

            //Chart.Plot.FigureBackground.Color = ScottPlot.Color.FromHex("#07263b");
            Chart.Plot.Axes.SetLimits(0, 50, 0, 20);
            Chart.Plot.Axes.AutoScale();
            Chart.Plot.ShowLegend();

            

            DispatcherTimer t = new DispatcherTimer();
            t.Tick += (o, e) => TimerTick(o, e, ref dataX, ref dataY, ref dataZ, ref counter);
            t.Interval = TimeSpan.FromMilliseconds(200);
            //t.Start();

            button_start.Click += (o, e) => Button_Stop(o, e, ref t, ref start);
            button_clear.Click += (o, e) => Button_Clear(o, e, ref dataX, ref dataY, ref dataZ, ref counter);



        }

        private void TimerTick(object o, EventArgs e, ref List<double> dataX, ref List<double> dataY, ref List<double> dataZ, ref double counter)
        {
            Random r = new Random();

            dataY.Add(r.NextDouble());
            dataX.Add(counter+=3);
            dataZ.Add(r.NextDouble() + 2);

            //Chart.Plot.Axes.SetLimits(0, 5000, 0, 20);
            //Chart.Plot.Axes.AutoScale();

            Chart.Plot.Clear();

           


            var sig = Chart.Plot.Add.ScatterLine(dataX.ToArray(), dataY.ToArray(), ScottPlot.Color.FromHex("#000000"));
            sig.LegendText = "Preassure";

            var sig2 = Chart.Plot.Add.ScatterLine(dataX.ToArray() ,dataZ.ToArray());
            sig2.LegendText = "Temperature";

            //sig.LineColor = ScottPlot.Color.FromHex("#000000");


            //Chart.Plot.SavePng("demo.png", 400, 300);
            Chart.Refresh();
           // throw new NotImplementedException();

           
        }

        private void Button_Stop(object sender, RoutedEventArgs e, ref DispatcherTimer t, ref bool start)
        {

            if (start)
            {
                t.Start();
                button_start.Content = "Stop";
            }
            else
            {
                t.Stop();
                button_start.Content = "Start";
            }

            start = !start;
                //Chart.Plot.Axes.SetLimits(0, 5000, 0, 20);
                //Chart.Plot.Axes.AutoScale();
                


        }
        private void Button_Clear(object sender, RoutedEventArgs e, ref List<double> dataX, ref List<double> dataY, ref List<double> dataZ, ref double counter)
        {
            counter = 0.0f;
            dataX.Clear();
            dataY.Clear();
            dataZ.Clear();
            Chart.Plot.Clear();
            Chart.Refresh();


        }

 

        private void Button_Follow(object sender, RoutedEventArgs e)
        {

            Chart.Plot.Axes.SetLimits(0, 5000, 0, 20);
            Chart.Plot.Axes.AutoScale();
        }
    }
}