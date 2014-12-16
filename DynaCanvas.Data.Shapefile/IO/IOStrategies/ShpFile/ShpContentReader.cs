using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile.Shapes;
using GeoAPI.Geometries;

namespace DynaCanvas.Data.Shapefile.IO
{
    public class ShpContentReader : IShpContentReadStrategy
    {
        private string _FilePath;
        private const int HEADER_LENGTH = 8;
        public ShpContentReader(string filePath)
        {
            _FilePath = filePath;
        }

        public FeatureLite ReaderShape(int recHeaderPos, int recCLength)
        {
            using (var stream = MMFCache.GetFileReadItems(_FilePath)
                .MMF.CreateViewStream(recHeaderPos, recCLength + HEADER_LENGTH))
            {
                using (var brReader = new BinaryReader(stream))
                {
                    FeatureLite fl;

                    int fID = Util.SwapByteOrder(brReader.ReadInt32());
                    int clength = Util.SwapByteOrder(brReader.ReadInt32());

                    ShapeType shapeType = (ShapeType)brReader.ReadInt32();  // 4

                    if (shapeType == ShapeType.Point)
                    {
                        fl = new FeatureLite(fID, shapeType
                            , brReader.ReadDouble(), brReader.ReadDouble());
                    }
                    else
                    {
                        double xMin = brReader.ReadDouble();
                        double yMin = brReader.ReadDouble();
                        double xMax = brReader.ReadDouble();
                        double yMax = brReader.ReadDouble();// 8*4=32

                        Envelope mbr = new Envelope(xMin, xMax, yMin, yMax);

                        var shapeBytes = new byte[clength - 36];  // 4+32=36
                        brReader.BaseStream.Seek(36, 0);
                        brReader.Read(shapeBytes, 0, clength - 36);
                        fl = new FeatureLite(fID, shapeType, mbr, shapeBytes);
                    }
                    return fl;
                }
            }
        }
    }
}
