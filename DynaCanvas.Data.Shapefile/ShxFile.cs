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
using GeoAPI.Geometries;

namespace DynaCanvas.Data.Shapefile
{
    public class ShxFile
    {
        FileHeader _FileHeader;
        //string _FilePath;
        private int _RecordCount;
        private ShxRecord[] _Records;
        private IFileHeaderReadStrategy _HeaderReader;
        private IShxContentReadStrategy _ContentxReader;
        private IShapefileIOFactory _IOFactory;


        public ShxFile(IShapefileIOFactory ioFactory)
        {
            _IOFactory = ioFactory;
          
            InitIndexFile();
        }
        private void InitIndexFile()
        {
            _FileHeader = _IOFactory.GetShxFileHeaderReader().ReadHeader();
            _ContentxReader = _IOFactory.GetShxContentReader();
            _RecordCount = (2 * _FileHeader.FileLength - 100) / 8;
            _Records = _ContentxReader.ReadRecordes(0, _RecordCount);
        }


        public Envelope MBR
        {
            get
            {
                return this._FileHeader.MBR;
            }
        }
        public ShapeType ShapeType
        {
            get
            {
                return (ShapeType)this._FileHeader.ShapeType;
            }
        }
        public ShxRecord[] Records
        {
            get
            {
                return _Records;
            }
        }

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
