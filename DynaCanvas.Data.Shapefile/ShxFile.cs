using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DynaCanvas.Data.Shapefile.Shapes;
using System.Runtime.InteropServices;
using DynaCanvas.Data.Shapefile.IO;
using System.Diagnostics.Contracts;

namespace DynaCanvas.Data.Shapefile
{
    public class ShxFile
    {
        FileHeader _FileHeader;
        //string _FilePath;
        private int _RecordCount;
        private ShxRecord[] _Records;
        private IFileHeaderReadStrategy _HeaderReader;
        private IShxContentReadStrategy _ContexReader;
        // ShapeType _ShapeType = ShapeType.NullShape;
        // int _RecordCount=0;
        // BoundingBox _MBR;

        public ShxFile(
            //string filePath,
            IFileHeaderReadStrategy headerReader
            , IShxContentReadStrategy contexReader
            )
        {
            //  Contract.Requires(File.Exists(filePath));
            // Contract.Requires(Path.GetExtension(filePath).ToLower() == Util.SHX_SUFFIX.ToLower());
            Contract.Requires(headerReader != null);
            Contract.Requires(contexReader != null);

            _HeaderReader = headerReader;
            _ContexReader = contexReader;
            //  _FilePath = filePath;
            InitIndexFile();
        }
        private void InitIndexFile()
        {
            _FileHeader = _HeaderReader.ReadHeader();
            _RecordCount = (2 * _FileHeader.FileLength - 100) / 8;
            _Records = _ContexReader.ReadRecordes(0, _RecordCount);
        }

        private void LoadHeader(BinaryReader brShapeIndex)
        {
            brShapeIndex.BaseStream.Seek(0, 0);
            //Check file header
            if (brShapeIndex.ReadInt32() != 170328064) //File Code is actually 9994, but in Little Endian Byte Order this is '170328064'
                throw (new ApplicationException("Invalid Shapefile Index (.shx)"));

            brShapeIndex.BaseStream.Seek(24, 0); //seek to File Length
            int IndexFileSize = Util.SwapByteOrder(brShapeIndex.ReadInt32()); //Read filelength as big-endian. The length is based on 16bit words
            _RecordCount = (2 * IndexFileSize - 100) / 8; //Calculate FeatureCount. Each feature takes up 8 bytes. The header is 100 bytes

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
            _FileHeader = new FileHeader(IndexFileSize, version, shapeType, mBR);


        }

        private void LoadRecords(BinaryReader brShapeIndex)
        {
            brShapeIndex.BaseStream.Seek(100, 0);  //skip the header
            _Records = new ShxRecord[_RecordCount];

            for (int i = 0; i < _RecordCount; i++)
            {
                int offset = Util.SwapByteOrder(brShapeIndex.ReadInt32());
                int cLength = Util.SwapByteOrder(brShapeIndex.ReadInt32());
                //int offset = brShapeIndex.ReadInt32();
                //int cLength =brShapeIndex.ReadInt32();
                // shapemap method:
                //int offset = 2 * Util.SwapByteOrder(brShapeIndex.ReadInt32()); //Read shape data position // ibuffer);
                //brShapeIndex.BaseStream.Seek(brShapeIndex.BaseStream.Position + 4, 0); //Skip content length
                //int cLength = 0;
                _Records[i] = new ShxRecord(offset, cLength);
            }


            //byte[] buff = brShapeIndex.ReadBytes(RecordSize.Size*_RecordCount);
            //GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);

            //fixed (Record* r = &_Records[0])
            //{
            //    *r = (Record*)abuff;
            //       //handle.AddrOfPinnedObject().;
            //}

            //_Records = (Record[])Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(Record[]));
            //handle.Free();

            //for (int x = 0; x < _RecordCount; ++x)
            //{
            //    _Records[x].Offset = Util.SwapByteOrder(_Records[x].Offset);
            //    _Records[x].ContentLength = Util.SwapByteOrder(_Records[x].ContentLength);
            //}
        }


        public FileHeader Header
        {
            get
            {
                return _FileHeader;
            }
        }



        //class RecordSize
        //{
        //     static int _size;

        //    static RecordSize()
        //    {
        //        _size = Marshal.SizeOf(typeof(Record));
        //    }

        //    public static int Size
        //    {
        //        get
        //        {
        //            return _size;
        //        }
        //    }
        //}
    }
    public struct ShxRecord
    {
        public ShxRecord(int offset, int cLength)
        {
            Offset = offset;
            ContentLength = cLength;
        }
        public int Offset;
        public int ContentLength;
    }
}
