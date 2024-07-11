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
using ScottPlot.TickGenerators.TimeUnits;
using ScottPlot.TickGenerators;

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

            Chart.Plot.SavePng("demo.png", 400, 300);
            Chart.Refresh();

            List<DateTime> dataX = new List<DateTime>();
            List<double> dataY = new List<double>();

            bool start = true;
            bool follow = true;
            int counter = 0;

            DateTime[] now = [DateTime.Now];
            double[] startPoint = [0];
            Chart.Plot.Add.ScatterLine(now, startPoint, ScottPlot.Color.FromHex("#000000"));
            Chart.Refresh();


            //Chart.Plot.FigureBackground.Color = ScottPlot.Color.FromHex("#07263b");
            Chart.Plot.Axes.SetLimits(0, 50, 0, 20);
            Chart.Plot.Axes.AutoScale();
            //Chart.Plot.ShowLegend();
            //Chart.Plot.Layout.Frameless();
            Chart.Plot.Axes.DateTimeTicksBottom();


            DispatcherTimer t = new DispatcherTimer();
            t.Tick += (o, e) => TimerTick(o, e, ref follow, ref dataX, ref dataY, ref counter);
            t.Interval = TimeSpan.FromMilliseconds(1000);

            button_start.Click += (o, e) => {
                if (start)
                {
                    Chart.Plot.Axes.DateTimeTicksBottom();
                    t.Start();
                    button_start.Content = "Stop";
                }
                else
                {
                    t.Stop();
                    button_start.Content = "Start";
                }

                start = !start;
            }; //Button_Stop(o, e, ref t, ref start);

            button_clear.Click += (o, e) => Button_Clear(o, e, ref dataX, ref dataY);

            checkbox_follow.Checked += (o, e) => follow = true;
            checkbox_follow.Unchecked += (o, e) => follow = false;



        }

       
        private void TimerTick(object o, EventArgs e, ref bool follow, ref List<DateTime> dataX, ref List<double> dataY, ref int counter)
        {
            Random r = new Random();

            dataY.Add(r.NextDouble() * counter++ + counter);
            dataX.Add(DateTime.Now);
            //dataZ.Add(r.NextDouble() + 2);

            //Chart.Plot.Axes.SetLimits(0, 5000, 0, 20);
            //Chart.Plot.Axes.AutoScale();
            Chart.Plot.Clear();



            
            var sig = Chart.Plot.Add.Scatter(dataX.ToArray(), dataY.ToArray(), ScottPlot.Color.FromHex("#000000"));

            if (follow) Chart.Plot.Axes.DateTimeTicksBottom();

            //var dtAx = Chart.Plot.Axes.DateTimeTicksBottom();
            //sig.LegendText = "Preassure";

            //var sig2 = Chart.Plot.Add.ScatterLine(dataX.ToArray() ,dataZ.ToArray());
            //sig2.LegendText = "Temperature";

            //sig.LineColor = ScottPlot.Color.FromHex("#000000");


            //Chart.Plot.SavePng("demo.png", 400, 300);
            Chart.Refresh();
           // throw new NotImplementedException();

           
        }

        private void Button_Stop(object sender, RoutedEventArgs e, ref DispatcherTimer t, ref bool start)
        {

            if (start)
            {
                Chart.Plot.Axes.DateTimeTicksBottom();
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
        private void Button_Clear(object sender, RoutedEventArgs e, ref List<DateTime> dataX, ref List<double> dataY)
        {
            dataX.Clear();
            dataY.Clear();
            Chart.Plot.Clear();
            Chart.Refresh();


        }

 

        private void Button_Follow(object sender, RoutedEventArgs e, ref bool follow)
        {
            //if (follow) button_follow.Content = "follow";
            //else button_follow.Content = "not follow";
            //follow = !follow;
            //Chart.Plot.Axes.DateTimeTicksBottom();
            //Chart.Plot.Axes.SetLimits(0, 5000, 0, 20);
            //Chart.Plot.Axes.AutoScale();
        }
    }
}