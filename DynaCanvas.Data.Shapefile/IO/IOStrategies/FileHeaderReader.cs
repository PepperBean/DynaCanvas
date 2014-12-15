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
    public class FileHeaderReader : IFileHeaderReadStrategy
    {
        // private BinaryReader brShapeIndex;
        //private int _RecordCount;
        // private FileHeader _FileHeader;
        private string _FilePath;
        public FileHeaderReader(string filePath)
        {
            Contract.Requires(File.Exists(filePath));
            _FilePath = filePath;
        }
        public FileHeader ReadHeader()
        {
            using (var stream = MMFCache.GetFileReadItems(_FilePath)
                .MMF.CreateViewStream(0,100))
            {
                using (var brShapeIndex =new BinaryReader(stream))
                {
                    brShapeIndex.BaseStream.Seek(0, 0);
                    //Check file header
                    if (brShapeIndex.ReadInt32() != 170328064) //File Code is actually 9994, but in Little Endian Byte Order this is '170328064'
                        throw (new ApplicationException("Invalid Shapefile Index (.shx)"));

                    brShapeIndex.BaseStream.Seek(24, 0); //seek to File Length
                    int IndexFileSize = Util.SwapByteOrder(brShapeIndex.ReadInt32()); //Read filelength as big-endian. The length is based on 16bit words
                    //int IndexFileSize = brShapeIndex.ReadInt32(); //Read filelength as big-endian. The length is based on 16bit words
                    //_RecordCount = (2 * IndexFileSize - 100) / 8; //Calculate FeatureCount. Each feature takes up 8 bytes. The header is 100 bytes

                    var version = brShapeIndex.ReadInt32();


                    // brShapeIndex.BaseStream.Seek(32, 0); //seek to ShapeType
                    var shapeType = brShapeIndex.ReadInt32();

                    //Read the spatial bounding box of the contents
                    brShapeIndex.BaseStream.Seek(36, 0); //seek to box


                    double x1, x2, y1, y2;
                    x1 = brShapeIndex.ReadDouble();
                    y1 = brShapeIndex.ReadDouble();
                    x2 = brShapeIndex.ReadDouble();
                    y2 = brShapeIndex.ReadDouble();

                    var mBR = new BoundingBox(x1, y1, x2, y2);
                    var fileHeader = new FileHeader(IndexFileSize, version, shapeType, mBR);
                    return fileHeader;
                }
            }
        }
    }
}
