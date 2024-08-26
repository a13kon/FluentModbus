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
using Trend.Classes;

namespace Trend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowCopy : Window
    {
  
        public MainWindowCopy()
        {

 
            InitializeComponent();

            string trendLimit = "1000";

            Trends trends = new Trends();

            ModbusTCP modbusTCP = new ModbusTCP("172.17.5.114", 503, 2);


            DispatcherTimer readWriteRegisters = new DispatcherTimer();
            readWriteRegisters.Tick += (o, e) => ReadWriteRegistersTick(o, e, modbusTCP);
            readWriteRegisters.Interval = TimeSpan.FromMilliseconds(500);
            readWriteRegisters.Start();
            

            DispatcherTimer mainLoop = new DispatcherTimer();
            mainLoop.Tick += (o, e) =>  MainLoop_Tick(o, e, modbusTCP, trends, ref trendLimit);
            mainLoop.Interval = TimeSpan.FromMilliseconds(100);
            mainLoop.Start();


            DispatcherTimer t = new DispatcherTimer();
            t.Tick += (o, e) => TimerTick(o, e, modbusTCP, trends);
            t.Interval = TimeSpan.FromMilliseconds(500);


            DateTime[] now = [DateTime.Now];
            double[] startPoint = [0];
            Chart.Plot.Add.SignalXY(now, startPoint, ScottPlot.Color.FromHex("#000000"));
            Chart.Plot.Axes.DateTimeTicksBottom();
            Chart.Plot.FigureBackground.Color = ScottPlot.Color.FromHex("#0e3d54");
            Chart.Plot.DataBackground.Color = ScottPlot.Color.FromHex("#0e3d54");


            button_start.Click += (o, e) => {
                if (trends.start)
                {
                   
                    t.Start();
                    button_start.Content = "Stop";

                }
                else
                {

                    t.Stop();
                    button_start.Content = "Start";

                }

                trends.start = !trends.start;
            }; 

            button_clear.Click += (o, e) => Button_Clear(o, e, trends);

            checkbox_follow.Checked += (o, e) => trends.follow = true;
            checkbox_follow.Unchecked += (o, e) => trends.follow = false;            

            
        }



        private async void ReadWriteRegistersTick (object o, EventArgs e, ModbusTCP modbusTCP)
        {
          

            await Task.Run(() =>
            {
                modbusTCP.connected = modbusTCP.client.IsConnected;
                if (modbusTCP.connected)
                {
   
                    try
                    {
                        //Trace.WriteLine("try connect");
                        var floatData = modbusTCP.client.ReadHoldingRegisters<float>(0xFF, 4, 2);
                        for (int i = 0; i < modbusTCP.mbFloat.Length; i++)
                        {
                            modbusTCP.mbFloat[i] = floatData[i];
                            //Trace.WriteLine(modbusTCP.result[i]);
                        }

                        //modbusTCP.client.WriteSingleCoil(0xFF, 0, true);
                        //modbusTCP.client.WriteMultipleRegisters(0xFF, 6, [55.1f]);

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

        private void MainLoop_Tick(object? sender, EventArgs e, ModbusTCP modbusTCP, Trends trends, ref string trendLimit)
        {

            if (trends.Xdate.Count >= Convert.ToInt32(trendLimit))
            {
                trends.Xdate.RemoveAt(0);
                trends.Ypreassure.RemoveAt(0);
                trends.Ytemperature.RemoveAt(0);
            }

            if (trends.follow && !trends.start)
            {

                Chart.Plot.Axes.AutoScale();
            }
            else
            {
                Chart.Plot.Axes.SetLimits();
            }

            if (modbusTCP.connected)
            {
                lbl1.Content = "Connected";
            }
            else
            {
                lbl1.Content = "Connection error";
            }

        }
        private void TimerTick(object o, EventArgs e, ModbusTCP modbusTCP, Trends trends)
        {
 
            DateTime now = DateTime.Now;

            if (modbusTCP.connected)
            {
                trends.Ypreassure.Add(modbusTCP.mbFloat[0]);
                trends.Ytemperature.Add(modbusTCP.mbFloat[1]);
                trends.Xdate.Add(now.ToOADate());
            }


            Chart.Plot.Clear();
          

            var logger1 = Chart.Plot.Add.SignalXY(trends.Xdate.ToArray(), trends.Ypreassure.ToArray(), ScottPlot.Color.FromHex("#003366"));
            var logger2 = Chart.Plot.Add.SignalXY(trends.Xdate.ToArray(), trends.Ytemperature.ToArray(), ScottPlot.Color.FromHex("#990000"));
            
            Chart.Plot.ShowLegend();
            Chart.Plot.Legend.Alignment = Alignment.LowerLeft;
            logger1.LegendText = "Давление";
            logger2.LegendText = "Температура";


            Chart.Refresh();

           
        }

        //private void Button_Stop(object sender, RoutedEventArgs e, ref DispatcherTimer t, ref bool start)
        //{

        //    if (start)
        //    {
        //        Chart.Plot.Axes.DateTimeTicksBottom();
        //        t.Start();
        //        button_start.Content = "Stop";
        //    }
        //    else
        //    {
        //        t.Stop();
        //        button_start.Content = "Start";
        //    }

        //    start = !start;               


        //}
        private void Button_Clear(object sender, RoutedEventArgs e, Trends trends)
        {
            trends.Xdate.Clear();
            trends.Ypreassure.Clear();
            trends.Ytemperature.Clear();
            Chart.Plot.Clear();
            Chart.Refresh();


        }


        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            //TestWindow testWindow = new TestWindow();
            //testWindow.Owner = this;
            //testWindow.ShowDialog(); 
        }



    }
}