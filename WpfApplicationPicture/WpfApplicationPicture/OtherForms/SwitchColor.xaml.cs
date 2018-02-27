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
    /// Interaction logic for SwitchColor.xaml
    /// </summary>
    public partial class SwitchColor : Window
    {
        private static readonly string blueColor = "BLUE";
        private static readonly string greenColor = "GREEN";
        private static readonly string redColor = "RED";
        private readonly String[] color = new string[] { redColor, greenColor, blueColor};
        public bool isSelectColor = false;
        public String[] chosenColor = new string[3];
        public SwitchColor()
        {
            InitializeComponent();
            RedComboBox.ItemsSource = color;
            GreenComboBox.ItemsSource = color;
            BlueComboBox.ItemsSource = color;    
        }
        /// <summary>
        /// info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClick_Click(object sender, RoutedEventArgs e)
        {
            if(RedComboBox.SelectedItem!=null && BlueComboBox.SelectedItem!=null
                && GreenComboBox.SelectedItem!=null)
            {
                isSelectColor = true;
                chosenColor[0] = RedComboBox.SelectedItem.ToString();
                chosenColor[1] = GreenComboBox.SelectedItem.ToString();
                chosenColor[2] = BlueComboBox.SelectedItem.ToString();

                if(chosenColor==color)
                {
                    //no change
                    isSelectColor = false;
                }
            }

            this.Close();
        }
    }
}
