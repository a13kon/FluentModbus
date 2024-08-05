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
using ScottPlot.AxisPanels;
using System.Data;
using FluentModbus;
using System.Net;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

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

            ModbusTCP modbusTCP = new ModbusTCP("172.17.5.103", 502, 2);

            

            DispatcherTimer readRegisters = new DispatcherTimer();
            readRegisters.Tick += (o, e) => ReadRegistersTick(o, e, modbusTCP);
            readRegisters.Interval = TimeSpan.FromMilliseconds(500);
            readRegisters.Start();


            Chart.Plot.SavePng("demo.png", 400, 300);
            Chart.Refresh();

            List<double> dataX = new List<double>();
            List<double> dataY = new List<double>();
            List<double> dataY2 = new List<double>();



            bool start = true;
            bool follow = true;
            ScottPlot.Plottables.SignalXY logger;

            //SignaxXY на большое количество точек (больше миллиона)
            DateTime[] now = [DateTime.Now];
            double[] startPoint = [0];
            Chart.Plot.Add.SignalXY(now, startPoint, ScottPlot.Color.FromHex("#000000"));
            
            

            Chart.Plot.Axes.DateTimeTicksBottom();
            Chart.Refresh();



            Chart.Plot.ShowLegend();
            //logger.LegendText = "Pressure";
 


            DispatcherTimer t = new DispatcherTimer();
            t.Tick += (o, e) => TimerTick(o, e, ref follow, ref dataX, ref dataY, ref dataY2, modbusTCP.result, modbusTCP.connected);
            t.Interval = TimeSpan.FromMilliseconds(500);

            button_start.Click += (o, e) => {
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
            }; 

            button_clear.Click += (o, e) => Button_Clear(o, e, ref dataX, ref dataY, ref dataY2);

            checkbox_follow.Checked += (o, e) => follow = true;
            checkbox_follow.Unchecked += (o, e) =>  follow = false;

            

            
        }

        private async void ReadRegistersTick (object o, EventArgs e, ModbusTCP modbusTCP)
        {
          

            await Task.Run(() =>
            {
                modbusTCP.connected = modbusTCP.client.IsConnected;
                if (modbusTCP.connected)
                {
   
                    try
                    {
                        //Trace.WriteLine("try connect");
                        var floatData = modbusTCP.client.ReadInputRegisters <float>(0xFF, 2, 2);
                        for (int i = 0; i < modbusTCP.result.Length; i++)
                        {
                            modbusTCP.result[i] = floatData[i];
                            //Trace.WriteLine(modbusTCP.result[i]);
                        }
                    }
                    catch
                    {
                        
                    }
                }

                else
                {
                    try
                    {
                        modbusTCP.client.Disconnect();
                        modbusTCP.client.Connect(new IPEndPoint(IPAddress.Parse(modbusTCP.ip), modbusTCP.port), ModbusEndianness.BigEndian);
                    }
                    catch {  }
                }
            });
      

        

        }

        private void TimerTick(object o, EventArgs e, ref bool follow, ref List<double> dataX, ref List<double> dataY, ref List<double> dataY2, float[] result, bool connected)
        {
            if (connected) lbl1.Content = "Connected";
            else lbl1.Content = "NOT Connected";
            //Random r = new Random();
            DateTime now = DateTime.Now;
            //dataY.Add(r.NextDouble() * 15);
            dataY.Add(result[0]);
            dataY2.Add(result[1]);
            dataX.Add(now.ToOADate());
            //dataZ.Add(r.NextDouble() + 2);

            if (dataX.Count >= Convert.ToInt32(txt1.Text)) 
            {
                dataX.RemoveAt(0);
                dataY.RemoveAt(0);
                dataY2.RemoveAt(0);
            }

            //Chart.Plot.Axes.SetLimits(0, 5000, 0, 20); 
            //Chart.Plot.Axes.AutoScale();
            Chart.Plot.Clear();
           

            //ScottPlot.Plottables.DataLogger Logger1 = Chart.Plot.Add.DataLogger();
            var logger1 = Chart.Plot.Add.SignalXY(dataX.ToArray(), dataY.ToArray(), ScottPlot.Color.FromHex("#003366"));
            var logger2 = Chart.Plot.Add.SignalXY(dataX.ToArray(), dataY2.ToArray(), ScottPlot.Color.FromHex("#990000"));

            Chart.Plot.ShowLegend();
            Chart.Plot.Legend.Alignment = Alignment.LowerLeft;
            logger1.LegendText = "Давление";
            logger2.LegendText = "Температура";

            if (follow)
            {

                //Chart.Plot.Axes.DateTimeTicksBottom();
                //Chart.Plot.Axes.SetLimits(0, 5, 0, 5); 
                Chart.Plot.Axes.AutoScale();                                              
                //Chart.Plot.Axes.SetLimitsX(10, 10); 

            } else
            {
                Chart.Plot.Axes.SetLimits();
            }



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
        private void Button_Clear(object sender, RoutedEventArgs e, ref List<double> dataX, ref List<double> dataY, ref List<double> dataY2)
        {
            dataX.Clear();
            dataY.Clear();
            dataY2.Clear();
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
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            TestWindow testWindow = new TestWindow();
            testWindow.Show();
        }
    }
}