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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Drawing;
using System.IO;
using WpfApplicationPicture.ImageClass;
using WpfApplicationPicture.OtherForms;

namespace WpfApplicationPicture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// init
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// FileLoad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileLoadMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            
            if (fileDialog.ShowDialog() == true)
            {
                Bitmap chosenBitmap = new Bitmap(fileDialog.FileName);
                BitmapSource bitmapSource = WpfApplicationPicture.ImageClass.
                    ImageConverter.GetImageSourceFromBitmap(chosenBitmap);
                
                OrigPhoto.Source = bitmapSource;
                chosenBitmap.Dispose(); ;
            }
        }
        /// <summary>
        /// Save file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSaveMenu_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "png files (*.png)|*.png";
            if(NewPhoto.Source==null)
            {
                MessageBox.Show("No New Image");
                return;
            }

            if(saveFile.ShowDialog()==true)
            {
                String nameFile = saveFile.FileName;
                var bitmapImage=NewPhoto.Source as BitmapSource;
                var im=WpfApplicationPicture.ImageClass.
                    ImageConverter.ConvertToBitMap(bitmapImage);

                if(File.Exists(nameFile)==true)
                {
                    File.Delete(nameFile);
                }

                im.Save(nameFile);
                
            }
        }
        /// <summary>
        /// Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileExitMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Switch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchMenu_Click(object sender, RoutedEventArgs e)
        {
            if(OrigPhoto.Source!=null)
            {
                NewPhoto.Source = OrigPhoto.Source;
            }


        }
        /// <summary>
        /// Use filter black and white
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlackAndWhiteMenu_Click(object sender, RoutedEventArgs e)
        {
            IImageFilter bw = new BlackAndWhiteFilter();
            this.UseFilter(bw);
        }
        /// <summary>
        /// Black and White
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReflectionMenu_Click(object sender, RoutedEventArgs e)
        {
            IImageFilter bw = new ReflectionFilter();
            this.UseFilter(bw);
        }
        /// <summary>
        /// Use filter
        /// </summary>
        /// <param name="_filter"></param>
        private void UseFilter(IImageFilter _filter)
        {
            NewPhoto.Source = null;

            if (OrigPhoto.Source==null)
            {
                MessageBox.Show("No Image set");
                return;
            }
            var im = WpfApplicationPicture.ImageClass.
                ImageConverter.ConvertToBitMap((BitmapSource)OrigPhoto.Source);

            var outImage = _filter.filter(im);
            NewPhoto.Source = WpfApplicationPicture.ImageClass.
                    ImageConverter.GetImageSourceFromBitmap( outImage ) as BitmapSource;
            //Dispose image
            outImage.Dispose();
            im.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            _filter = null;
        }
        /// <summary>
        /// Set normal to orgin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchNormalMenu_Click(object sender, RoutedEventArgs e)
        {
            if (NewPhoto.Source != null)
            {
                OrigPhoto.Source = null;
                OrigPhoto.Source = NewPhoto.Source.Clone();
            }

        }
        /// <summary>
        /// Light up or down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightUpDownMenu_Click(object sender, RoutedEventArgs e)
        {
            WpfApplicationPicture.OtherForms.LightUpDownWindow light = new OtherForms.LightUpDownWindow();
             light.ShowDialog();
            
            if(light.valueIsSet==true)
            {
                IImageFilter lightUpDownFilter = new LightUpDown(light.valueOut);
                this.UseFilter(lightUpDownFilter);
            }

        }
        /// <summary>
        /// Switch Color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchColorMenu_Click(object sender, RoutedEventArgs e)
        {
            WpfApplicationPicture.OtherForms.SwitchColor switchColor = new OtherForms.SwitchColor();
            switchColor.ShowDialog();

            if(switchColor.isSelectColor==true)
            {
                IImageFilter lightUpDownFilter = new SwitchColorFilter(switchColor.chosenColor);
                this.UseFilter(lightUpDownFilter);
            }
            else
            {
                MessageBox.Show("No color select");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchNormalMenu_Click_1(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterCustomMenu_Click(object sender, RoutedEventArgs e)
        {
            CustomFilterWindow windows = new CustomFilterWindow();
            windows.ShowDialog();

            if(windows.isSelectFilter==true && windows.choseTable!=null)
            {
                IImageFilter matrixFilter = new MatrixFilter(windows.filterName,windows.choseTable);
                this.UseFilter(matrixFilter);

            }
        }
        /// <summary>
        /// Max filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maxMenuItem_Click(object sender, RoutedEventArgs e)
        {
            IImageFilter linearFilter = new LinearFilter(LinearFilter.LinearType.Max);
            this.UseFilter(linearFilter);
        }

        private void minMenuItem_Click(object sender, RoutedEventArgs e)
        {
            IImageFilter linearFilter = new LinearFilter(LinearFilter.LinearType.Min);
            this.UseFilter(linearFilter);
        }
        /// <summary>
        /// Windows size changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainPanel.Width = e.NewSize.Height;
            MainPanel.ColumnDefinitions[0].Width = new GridLength(MainPanel.Width / 2);
        }
    }
}
