using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationPicture.ImageClass
{
    class LinearFilter : IImageFilter
    {
        public enum LinearType{Max,Min,Medium };

        LinearType type;

        public LinearFilter(LinearType _type)
        {
            this.type = _type;
        }

        public string Name
        {
            get
            {
                return "Linear FIlter";
            }
        }
        /// <summary>
        /// linear filter
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
                    int redValue = GetValue(_bitmap, i, j, type,"Red",height,weight);
                    int greenValue = GetValue(_bitmap, i, j, type,"Green", height, weight);
                    int blueValue = GetValue(_bitmap,i,j, type,"Blue", height, weight);

                    newBitmap.SetPixel(i, j, Color.FromArgb(redValue, greenValue, blueValue));
                }

            }
            return newBitmap;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_bitmap"></param>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_type"></param>
        /// <param name="_color"></param>
        /// <param name="_height"></param>
        /// <param name="_weight"></param>
        /// <returns></returns>
        private int GetValue(Bitmap _bitmap, int _x, int _y, LinearType _type, string _color, int _height, int _weight)
        {
            List<int> values = new List<int>();

            if(this.CheckValue(_x,_y,_height,_weight)==true)
            {
                int valueColor = this.GetColor(_bitmap,_x,_y,_color);
                values.Add(valueColor);
            }
            else
            {
                values.Add(0);
            }
            if (this.CheckValue(_x-1, _y-1, _height, _weight) == true)
            {
                int valueColor = this.GetColor(_bitmap, _x-1, _y-1, _color);
                values.Add(valueColor);
            }
            else
            {
                values.Add(0);
            }
            if (this.CheckValue(_x - 1, _y , _height, _weight) == true)
            {
                int valueColor = this.GetColor(_bitmap, _x - 1, _y , _color);
                values.Add(valueColor);
            }
            else
            {
                values.Add(0);
            }
            if (this.CheckValue(_x - 1, _y + 1, _height, _weight) == true)
            {
                int valueColor = this.GetColor(_bitmap, _x - 1, _y + 1, _color);
                values.Add(valueColor);
            }
            else
            {
                values.Add(0);
            }


            if (this.CheckValue(_x, _y - 1, _height, _weight) == true)
            {
                int valueColor = this.GetColor(_bitmap, _x, _y - 1, _color);
                values.Add(valueColor);
            }
            else
            {
                values.Add(0);
            }

            if (this.CheckValue(_x , _y, _height, _weight) == true)
            {
                int valueColor = this.GetColor(_bitmap, _x, _y , _color);
                values.Add(valueColor);
            }
            else
            {
                values.Add(0);
            }

            if (this.CheckValue(_x, _y + 1, _height, _weight) == true)
            {
                int valueColor = this.GetColor(_bitmap, _x , _y + 1, _color);
                values.Add(valueColor);
            }
            else
            {
                values.Add(0);
            }

            if (this.CheckValue(_x + 1, _y - 1, _height, _weight) == true)
            {
                int valueColor = this.GetColor(_bitmap, _x + 1, _y - 1, _color);
                values.Add(valueColor);
            }
            else
            {
                values.Add(0);
            }


            if (this.CheckValue(_x + 1, _y , _height, _weight) == true)
            {
                int valueColor = this.GetColor(_bitmap, _x + 1, _y , _color);
                values.Add(valueColor);
            }
            else
            {
                values.Add(0);
            }

            if (this.CheckValue(_x + 1, _y + 1, _height, _weight) == true)
            {
                int valueColor = this.GetColor(_bitmap, _x + 1, _y + 1, _color);
                values.Add(valueColor);
            }
            else
            {
                values.Add(0);
            }


            if(_type==LinearType.Max)
            {
                return values.Max();
            }
            if(_type==LinearType.Min)
            {
                return values.Min();
            }
            return 0;
        }

        private int GetColor(Bitmap _bitmap, int _x, int _y, string _color)
        {
            if(_color=="Red")
            {
                return _bitmap.GetPixel(_x, _y).R;
            }
            if(_color=="Green")
            {
                return _bitmap.GetPixel(_x, _y).G;
            }
            return _bitmap.GetPixel(_x, _y).B;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_height"></param>
        /// <param name="_weight"></param>
        /// <returns></returns>
        private bool CheckValue(int _x, int _y, int _height, int _weight)
        {
            if (_x < 0 || _y < 0)
            {
                return false;
            }
            if (_x >= _weight)
            {
                return false;
            }
            if (_y >= _height)
            {
                return false;
            }
            return true;
        }
    }
}
