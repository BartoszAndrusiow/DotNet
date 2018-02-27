using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationPicture.filterArraySet
{
    public static class AveragingArray
    {
        public static int[,] arrayAve = new int[3, 3]
        {
         {1,2,1 },
         {2,4,2 },
         {1,2,1 }
        };
        public static int[,] arraySharp = new int[3, 3]
        {
         {0,-2,0 },
         {-2,11,-2 },
         {0,-2,0 }
        };
    }
}
