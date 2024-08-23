using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ScottPlot;
using ScottPlot.TickGenerators.TimeUnits;
using ScottPlot.TickGenerators;
using ScottPlot.AxisPanels;

namespace Trend
{


    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
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
