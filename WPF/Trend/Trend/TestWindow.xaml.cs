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

        private void chk1_Checked(object sender, RoutedEventArgs e)
        {
            //var bc = new BrushConverter();
            //btn1.Background = (Brush)bc.ConvertFrom("#FF00FFFF");
            //chk1.BorderBrush = (Brush)bc.ConvertFrom("#FF00FFFF");
            //chk1.IsChecked = !chk1.IsChecked;
        }

        private void chk1_Unchecked(object sender, RoutedEventArgs e)
        {
            //var bc = new BrushConverter();
            //btn1.Background = (Brush)bc.ConvertFrom("#FF008000");
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            //var bc = new BrushConverter();
            //btn1.Background = (Brush)bc.ConvertFrom("#FF00FFFF");
            //chk1.BorderBrush = (Brush)bc.ConvertFrom("#FF00FFFF");

            

        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            MessageBox.Show(pressed.Content.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
