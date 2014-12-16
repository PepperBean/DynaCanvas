using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile.Shapes;
using GeoAPI.Geometries;

namespace DynaCanvas.Data.Shapefile
{
   // [StructLayout(LayoutKind.Explicit, Size = 100)]
    public struct FileHeader
    {
        public FileHeader(
           // int fileCode,
            int fileLength,
            int version,
            int shapeType,
            Envelope mBR)
        {

            //FileCode = fileCode;
            FileLength = fileLength;
            Version = version;
            ShapeType = shapeType;
            MBR = mBR;
        }
       // public readonly int         FileCode    ;
        public          int         FileLength  ;
        public readonly int         Version     ;
        public readonly int         ShapeType   ;
        public Envelope MBR;
    }
}
