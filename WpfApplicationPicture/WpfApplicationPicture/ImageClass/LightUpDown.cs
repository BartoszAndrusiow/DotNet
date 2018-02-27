using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationPicture.ImageClass
{
    class LightUpDown : IImageFilter
    {
        private int value = 0;
        /// <summary>
        /// init
        /// </summary>
        /// <param name="_value"></param>
        public LightUpDown(int _value)
        {
            this.value = _value;
        }
        /// <summary>
        /// name
        /// </summary>
        public string Name
        {
            get
            {
                return "LightUpDown";
            }
        }
        /// <summary>
        /// filter light up down
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
                    newBitmap.SetPixel(i, j, Color.FromArgb((byte)SetValue(oldPixel.R)
                        , (byte)SetValue(oldPixel.G),
                        (byte)SetValue(oldPixel.B)));
                }

            }
            return newBitmap;
        }
        /// <summary>
        /// set value
        /// </summary>
        /// <param name="_v"></param>
        /// <returns></returns>
        private int SetValue(byte _v)
        {
            if (_v + value < 255 && _v + value > 0)
            {
                return _v + value;
            }
            else if(_v + value > 255)
            {
                return 255;
            }
            else
            {
                return 0;
            }
        }
    }
}
