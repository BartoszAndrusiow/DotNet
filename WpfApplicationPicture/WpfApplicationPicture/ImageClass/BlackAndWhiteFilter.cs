using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationPicture.ImageClass
{
    public class BlackAndWhiteFilter : IImageFilter
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get
            {
                return "BlackAndWhiteFilter";
            }

        }
        /// <summary>
        /// filter
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
                    Color oldPixel = _bitmap.GetPixel(i, j);
                    newBitmap.SetPixel(i, j, Color.FromArgb(oldPixel.R, oldPixel.R, oldPixel.R));
                }

            }
            return newBitmap;

        }
    }
}
