using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationPicture.ImageClass
{
    public class ReflectionFilter : IImageFilter
    {
        /// <summary>
        /// delegat
        /// </summary>
        /// <param name="_v"></param>
        /// <returns></returns>
        delegate int delegateFilter(int _v);
        /// <summary>
        /// name
        /// </summary>
        public string Name
        {
            get
            {
                return "ReflectionFilter";
            }
        }
        /// <summary>
        /// Filter
        /// </summary>
        /// <param name="_bitmap"></param>
        /// <returns></returns>
        public Bitmap filter(Bitmap _bitmap)
        {

            int height = _bitmap.Height;
            int weight = _bitmap.Width;
            Bitmap newBitmap = new Bitmap(weight, height);

        

            for (int i = 0; i < weight; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var colPixel = _bitmap.GetPixel(i, j);

                    delegateFilter lambaConv = delegate (int _value) { return 255 - _value; }; ;

                    newBitmap.SetPixel(i, j,Color.FromArgb(lambaConv(colPixel.R), 
                        lambaConv(colPixel.G), lambaConv(colPixel.G)));
                }
            }
            return newBitmap;
        }
    }
}
