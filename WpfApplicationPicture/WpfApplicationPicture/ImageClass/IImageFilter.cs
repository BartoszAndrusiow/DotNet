using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationPicture.ImageClass
{
    public interface IImageFilter
    {
        String Name
        {
            get;
            
        }
        Bitmap filter(Bitmap _bitmap);
    }
}
