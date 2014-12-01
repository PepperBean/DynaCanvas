using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data.Shapefile.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BoundingBox
    {
        public BoundingBox(
            double xMin,
            double yMin,
            double xMax,
            double yMax)
        {

            Xmin = xMin;
            Ymin = yMin;
            Xmax = xMax;
            Ymax = yMax;
        }
        public double Xmin;
        public double Ymin;
        public double Xmax;
        public double Ymax;
    }
}
