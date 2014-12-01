using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data.Shapefile.Shapes
{

    [StructLayout(LayoutKind.Sequential)]
    public struct MultiPoint
    {
        public MultiPoint(int numPoints)
        {
            MBR = new BoundingBox();
            NumPoints = numPoints;
            Points = new Point[NumPoints];
        }
        public BoundingBox MBR;
        public int NumPoints;
        public Point[] Points;
    }
    //[StructLayout(LayoutKind.Sequential)]
    //public struct MultiPoint
    //{
    //    MultiPoint_FixPart FixPart;
    //    Point[] Ponits;
    //}
    //[StructLayout(LayoutKind.Sequential)]
    // struct MultiPoint_FixPart
    //{
    //    BoundingBox Box;
    //    int NumPoints;
    //}
}
