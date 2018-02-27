using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationPicture.ImageClass
{
    public class MatrixFilter : IImageFilter
    {
        String nameOfTheFilter;
        int[,] tab;

        String redColor = "Red";
        String greenColor = "Green";
        String blueColor = "Blue";
        //TODO: CHECK FILTER

        public MatrixFilter(String _name,int[,] _tab)
        {
            this.nameOfTheFilter = _name;
            this.tab = _tab;
        }
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get
            {
                return nameOfTheFilter;
            }
        }

        public Bitmap filter(Bitmap _bitmap)
        {

            //_bitmap = new Bitmap(3, 3);
            //_bitmap.SetPixel(0, 0, Color.FromArgb(12,0,0));
            //_bitmap.SetPixel(0, 1, Color.FromArgb(2, 0, 0));
            //_bitmap.SetPixel(0, 2, Color.FromArgb(22, 0, 0));

            //_bitmap.SetPixel(1, 0, Color.FromArgb(0, 0, 0));
            //_bitmap.SetPixel(1, 1, Color.FromArgb(8, 0, 0));
            //_bitmap.SetPixel(1, 2, Color.FromArgb(55, 0, 0));


            //_bitmap.SetPixel(2, 0, Color.FromArgb(23, 0, 0));
            //_bitmap.SetPixel(2, 1, Color.FromArgb(32, 0, 0));
            //_bitmap.SetPixel(2, 2, Color.FromArgb(4, 0, 0));


            // 0,0 1,0 2,0
            // 0 1 1 1 2 1
            // 0 2 2 2 2 2
            int height = _bitmap.Height;
            int weight = _bitmap.Width;

            Bitmap newBitmap = new Bitmap(weight, height);

            int[,] baseTable = new int[this.tab.Rank + 1, this.tab.Rank + 1];
            int wage = this.getWageSum(this.tab);
            for (int i = 1; i < weight; i++)
            {
                for (int j = 1; j < height; j++)
                {
                    Color oldPixel = _bitmap.GetPixel(i, j);
                    Color[,] table = this.GetOldPixelTable(_bitmap, i, j, height, weight, this.tab);

                    int valueRed = this.MultimpleTable(table, this.tab, redColor, wage);
                    int valueGreen = this.MultimpleTable(table, this.tab, greenColor, wage);
                    int valueBlue = this.MultimpleTable(table, this.tab, blueColor, wage);
                    newBitmap.SetPixel(i, j, Color.FromArgb(valueRed, valueGreen, valueBlue));

                }

            }
            return newBitmap;
          //  return _bitmap;
        }
        /// <summary>
        /// Get Wage
        /// </summary>
        /// <param name="_tab"></param>
        /// <returns></returns>
        private int getWageSum(int[,] _tab)
        {
            int sumValue = 0;
            for (int i = 0; i < _tab.Rank + 1; i++)
            {
                for (int j = 0; j < _tab.Rank + 1; j++)
                {
                    sumValue += (_tab[i, j]);
                }
            }

            return sumValue;
        }
        /// <summary>
        /// Multi matrix
        /// </summary>
        /// <param name="_table"></param>
        /// <param name="_tab"></param>
        /// <param name="_color"></param>
        /// <returns></returns>
        private int MultimpleTable(Color[,] _table, int[,] _tab, string _color,int _wageSum)
        {
            int sumValue = 0;
            for(int i=0;i<_tab.Rank+1;i++)
            {
                for(int j=0;j<_tab.Rank+1;j++)
                {
                    sumValue += (GetColor(_table[i, j],_color) * _tab[i,j]);
               }
            }
            if(sumValue<=0)
            {
                return 0;
            }

            return ((int)(sumValue/16) % 256);
        }
        /// <summary>
        /// Get Color
        /// </summary>
        /// <param name="_color"></param>
        /// <param name="_colorChose"></param>
        /// <returns></returns>
        private int GetColor(Color _color, string _colorChose)
        {
            if(_colorChose==redColor)
            {
                return _color.R;
            }

            if (_colorChose == greenColor)
            {
                return _color.G;
            }
            if (_colorChose == blueColor)
            {
                return _color.B;
            }
            throw new Exception("Wrong Color");
        }

        /// <summary>
        /// return old table pixal
        /// </summary>
        /// <param name="_bitmap"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="tab"></param>
        /// <returns></returns>
        private Color[,] GetOldPixelTable(Bitmap _bitmap, int _i, int _j, int _height, int _weight, int[,] _tab)
        {
            Color[,] oldColorTable = new Color[_tab.Rank+1,_tab.Rank+1];
            
            if (this.CheckValuePointer(_i - 1, _j - 1,_height,_weight)==true)
            {
               oldColorTable[0,0]= _bitmap.GetPixel(_i - 1, _j - 1);
            }
            else
            {
                oldColorTable[0, 0] = Color.FromArgb(0,0,0);
            }

            if (this.CheckValuePointer(_i - 1, _j, _height, _weight) == true)
            {
                oldColorTable[0, 1] = _bitmap.GetPixel(_i - 1, _j);
            }
            else
            {
                oldColorTable[0, 1] = Color.FromArgb(0, 0, 0);
            }

            if (this.CheckValuePointer(_i - 1, _j+1, _height, _weight) == true)
            {
                oldColorTable[0, 2] = _bitmap.GetPixel(_i - 1, _j+1);
            }
            else
            {
                oldColorTable[0, 2] = Color.FromArgb(0, 0, 0);
            }

            //2 columns
            if (this.CheckValuePointer(_i , _j-1, _height, _weight) == true)
            {
                oldColorTable[1, 0] = _bitmap.GetPixel(_i , _j-1 );
            }
            else
            {
                oldColorTable[1, 0] = Color.FromArgb(0, 0, 0);
            }

            if (this.CheckValuePointer(_i , _j, _height, _weight) == true)
            {
                oldColorTable[1, 1] = _bitmap.GetPixel(_i , _j);
            }
            else
            {
                oldColorTable[1, 1] = Color.FromArgb(0, 0, 0);
            }

            if (this.CheckValuePointer(_i , _j + 1, _height, _weight) == true)
            {
                oldColorTable[1, 2] = _bitmap.GetPixel(_i, _j + 1);
            }
            else
            {
                oldColorTable[1, 2] = Color.FromArgb(0, 0, 0);
            }
            //3 columns

            if (this.CheckValuePointer(_i+1, _j - 1, _height, _weight) == true)
            {
                oldColorTable[2, 0] = _bitmap.GetPixel(_i + 1, _j - 1);
            }
            else
            {
                oldColorTable[2, 0] = Color.FromArgb(0, 0, 0);
            }

            if (this.CheckValuePointer(_i+1, _j, _height, _weight) == true)
            {
                oldColorTable[2, 1] = _bitmap.GetPixel(_i+1, _j);
            }
            else
            {
                oldColorTable[2, 1] = Color.FromArgb(0, 0, 0);
            }

            if (this.CheckValuePointer(_i + 1, _j + 1, _height, _weight) == true)
            {
                oldColorTable[2, 2] = _bitmap.GetPixel(_i + 1, _j + 1);
            }
            else
            {
                oldColorTable[2, 2] = Color.FromArgb(0, 0, 0);
            }

            return oldColorTable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="_height"></param>
        /// <param name="_weight"></param>
        /// <returns></returns>
        private bool CheckValuePointer(int v1, int v2, int _height, int _weight)
        {
            if(v1<0 || v2<0)
            {
                return false;
            }
            if(v1>=_weight)
            {
                return false;
            }
            if(v2>=_height)
            {
                return false;
            }
            return true;
        }
    }
}
