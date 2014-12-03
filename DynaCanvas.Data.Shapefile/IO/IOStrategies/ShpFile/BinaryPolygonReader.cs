using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile.Shapes;

namespace DynaCanvas.Data.Shapefile.IO
{
    public class BinaryPolygonReader : BaseBinaryShapeReader<Polygon>
    {

        public BinaryPolygonReader(string filePath)
            : base(filePath)
        {

        }

        public override Polygon ReadShape(int recPos)
        {

            var rH = base.ReadRecordHeader(recPos);
            var brReader = FileReaderCache.GetFileReadItems(_FilePath).BinaryReader;
            brReader.BaseStream.Seek(recPos + 4, 0);
            var shapeType = brReader.ReadInt32(); // =5

            Polygon p = new Polygon();
            var x1 = brReader.ReadDouble();
            var y1 = brReader.ReadDouble();
            var x2 = brReader.ReadDouble();
            var y2 = brReader.ReadDouble();

            var mbr = new BoundingBox(x1, y1, x2, y2);

            var numParts = brReader.ReadInt32();
            var numPoints = brReader.ReadInt32();

            var parts = new int[numParts];
            for (int i = 0; i < numParts; i++)
            {
                parts[i] = brReader.ReadInt32();
            }

            var points = new Point[numPoints];
            for (int i = 0; i < numPoints; i++)
            {
                var pt = new Point();
                pt.X = brReader.ReadDouble();
                pt.Y = brReader.ReadDouble();
                points[i] = pt;
            }

            p.MBR = mbr;
            p.NumParts = numParts;
            p.NumPoints = numPoints;
            p.Parts = parts;
            p.Points = points;

            return p;
        }
    }
}
