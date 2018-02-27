using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Threading;

namespace WpfApplicationPicture.ImageClass
{
    static public class ImageConverter
    {
        public static BitmapSource GetImageSourceFromBitmap(Bitmap _chosenBitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                _chosenBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }

        }
        /// <summary>
        /// Convert Image
        /// </summary>
        /// <param name="_chosenBitmap"></param>
        /// <returns></returns>
        public static BitmapSource GetImageSourceFromBitmap1(Bitmap _chosenBitmap)
        {

            BitmapImage bi = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi.BeginInit();
            //bi.UriSource = new Uri(@"/sampleImages/cherries_larger.jpg", UriKind.RelativeOrAbsolute);
            //bi.StreamSource=
            bi.EndInit();
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    var image = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap
                        (
                        _chosenBitmap.GetHbitmap(),
                        IntPtr.Zero,
                        System.Windows.Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions()
                        );

                    return image;

                }
                catch (SystemException ex)
                {
                    if (i == 4)
                    {
                        throw ex;
                    }
                    Thread.Sleep(20);
                }
            }
            return null;
        }
        /// <summary>
        /// Convert to BitMap
        /// </summary>
        /// <param name="_bmpSource"></param>
        /// <returns></returns>
        public static Bitmap ConvertToBitMap(BitmapSource _bmpSource)
        {
            Bitmap bmp = new Bitmap
            (
                _bmpSource.PixelWidth,
                _bmpSource.PixelHeight,
                System.Drawing.Imaging.PixelFormat.Format32bppPArgb
            );
            {

                System.Drawing.Imaging.BitmapData data = bmp.LockBits
                (
                    new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppPArgb
                );

                _bmpSource.CopyPixels
                (
                  System.Windows.Int32Rect.Empty,
                  data.Scan0,
                  data.Height * data.Stride,
                  data.Stride
                );

                bmp.UnlockBits(data);

                return bmp;
            }
        }
    }
}
