﻿using System.Text;
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
            int counter = 0;

            ScottPlot.Plot myPlot = new();

         

            DispatcherTimer t = new DispatcherTimer();
            t.Tick += (o, e) => Click_2(o, e, ref dataX, ref dataY, ref counter);
            t.Interval = TimeSpan.FromSeconds(1);
            t.Start();


        }

        private void Click_2(object o, EventArgs e, ref List<double> dataX, ref List<double> dataY, ref int counter)
        {
            Random r = new Random();
            dataY.Add(r.NextDouble() * 10 * counter);
            dataX.Add(counter++);
            var sig = Chart.Plot.Add.ScatterLine(dataX.ToArray(), dataY.ToArray());

            sig.LineColor = ScottPlot.Color.FromHex("#a0acb5");

            Chart.Plot.FigureBackground.Color = ScottPlot.Color.FromHex("#07263b");
            
            Chart.Plot.SavePng("demo.png", 400, 300);
            Chart.Refresh();
           // throw new NotImplementedException();

           
        }



    }
}