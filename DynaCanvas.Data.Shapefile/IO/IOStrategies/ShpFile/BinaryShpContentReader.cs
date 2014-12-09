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
    public class BinaryShpContentReader //: IShpContentReadStrategy
    {
        private string _FilePath;
        private IGeometryFactory _GeoFactory;
        private const int HEADER_LENGTH = 8;
        public BinaryShpContentReader(string filePath
            , IGeometryFactory geoFactory
            )
        {
            _FilePath = filePath;
            _GeoFactory = geoFactory;
        }

        public FeatureLite ReaderShape(int recHeaderPos, int recCLength)
        {
            using (var stream = MMFCache.GetFileReadItems(_FilePath)
                .MMF.CreateViewStream(recHeaderPos, recCLength + HEADER_LENGTH))
            {
                using (var brReader = new BinaryReader(stream))
                {
                    FeatureLite fl;

                    int fID = brReader.ReadInt32();
                    int clength = brReader.ReadInt32();

                    ShapeType shapeType = (ShapeType)brReader.ReadInt32();  // 4

                    if (shapeType == ShapeType.Point)
                    {
                        fl = new FeatureLite(fID, shapeType
                            , brReader.ReadDouble(), brReader.ReadDouble());
                    }
                    else
                    {
                        BoundingBox mbr = new BoundingBox(brReader.ReadDouble()
                            , brReader.ReadDouble()
                            , brReader.ReadDouble()
                            , brReader.ReadDouble());  // 8*4=32

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
