using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data.Shapefile.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public double X;
        public double Y;
    }
}
