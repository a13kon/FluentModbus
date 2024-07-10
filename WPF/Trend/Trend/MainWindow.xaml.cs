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

            

            DispatcherTimer t = new DispatcherTimer();
            t.Tick += (o, e) => Click_2(o, e, ref dataX, ref dataY, ref counter);
            t.Interval = TimeSpan.FromSeconds(1);
            t.Start();


        }

        private void Click_2(object o, EventArgs e, ref List<double> dataX, ref List<double> dataY, ref int counter)
        {
            Random r = new Random();
            dataY.Add(r.NextDouble() * 10);
            dataX.Add(counter++);
            WpfPlot1.Plot.Add.Scatter(dataX.ToArray(), dataY.ToArray());
            WpfPlot1.Refresh();
           // throw new NotImplementedException();

           
        }



    }
}