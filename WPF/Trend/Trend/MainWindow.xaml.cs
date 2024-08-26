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
    public partial class MainWindow : Window
    {

        public MainWindow()
        {


            InitializeComponent();

        }


        private void Chart_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

            if (e.Delta > 0)
            {
                scrollTrend.LineUp();
                scrollTrend.LineUp();
                scrollTrend.LineUp();
            }
            else
            {
                scrollTrend.LineDown();
                scrollTrend.LineDown();
                scrollTrend.LineDown();
            }
            e.Handled = true;

        }


    }
}