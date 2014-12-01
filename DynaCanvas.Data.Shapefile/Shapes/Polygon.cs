using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data.Shapefile.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Polygon
    {
        public Polygon(int numParts,int numPoints)
        {
            MBR = new BoundingBox();
            NumParts = numParts;
            NumPoints = numPoints;
            Parts = new int[NumParts];
            Points = new Point[NumPoints];
        }
        public BoundingBox MBR;
        public int NumParts;
        public int NumPoints;
        public int[] Parts;
        public Point[] Points;
    }
    //[StructLayout(LayoutKind.Sequential)]
    //public struct Polygon
    //{
    //    Polygon_FixPart FixPart;
    //    int[] Parts;
    //    Point[] Points;
    //}
    //[StructLayout(LayoutKind.Sequential)]
    //public struct Polygon_FixPart
    //{
    //    BoundingBox Box;
    //    int NumParts;
    //    int NumPoints;
        
    //}
}
