using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationPicture.ImageClass
{
    class SwitchColorFilter : IImageFilter
    {
        private String[] colorOrder = new string[3];

        public SwitchColorFilter(String[] _colorOrder)
        {
            colorOrder[0] = _colorOrder[0];
            colorOrder[1] = _colorOrder[1];
            colorOrder[2] = _colorOrder[2];
        }
        /// <summary>
        /// name
        /// </summary>
        public string Name
        {
            get
            {
                return "SwitchColorFilter";
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
                    newBitmap.SetPixel(i, j, Color.FromArgb(GetSelectColor(oldPixel,0),
                        GetSelectColor(oldPixel,1)
                        , GetSelectColor(oldPixel,2)));
                }

            }
            return newBitmap;
        }
        /// <summary>
        /// get pixel color
        /// </summary>
        /// <param name="_oldPixel"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private byte GetSelectColor(Color _oldPixel, int index)
        {
            switch (colorOrder[index])
            {
                case "RED":return _oldPixel.R;
                case "GREEN": return _oldPixel.G;
                case "BLUE": return _oldPixel.B;
            }
            return 0;
        }
    }
}
