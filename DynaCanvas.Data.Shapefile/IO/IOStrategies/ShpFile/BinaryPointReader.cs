using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile.Shapes;

namespace DynaCanvas.Data.Shapefile.IO
{
    public class BinaryPointReader : BaseBinaryShapeReader<Point>
    {

        public BinaryPointReader(string filePath)
            : base(filePath)
        {
        }

        public override Point ReadShape(int recPos)
        {
            var rH = base.ReadRecordHeader(recPos);
            var brReader = FileReaderCache.GetFileReadItems(_FilePath).BinaryReader;
            brReader.BaseStream.Seek(recPos + 4, 0);
            var shapeType = brReader.ReadInt32();  // 1
            Point p = new Point();
            p.X = brReader.ReadDouble();
            p.Y = brReader.ReadDouble();
            return p;
        }
    }
}
