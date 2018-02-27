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

namespace WpfApplicationPicture.OtherForms
{
    /// <summary>
    /// Interaction logic for LightUpDownWindow.xaml
    /// </summary>
    public partial class LightUpDownWindow : Window
    {
        public Boolean valueIsSet = false;
        public int valueOut = 0;
        public LightUpDownWindow()
        {
            InitializeComponent();
        }

        private void SliderLightUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ValueLabel.Content = Math.Round(SliderLightUpDown.Value);
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            valueOut = (int)Math.Round(SliderLightUpDown.Value);
            valueIsSet = true;
            this.Close();
        }
    }
}
