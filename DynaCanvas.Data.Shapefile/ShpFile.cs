using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile.IO;
using GeoAPI.Geometries;

namespace DynaCanvas.Data.Shapefile
{
    public abstract class ShpFile
    {
        protected FileHeader _FileHeader;
        protected string _FilePath;
        private IFileHeaderReadStrategy _HeaderReader;
        private IGeometryFactory _GeoFactory;

        protected ShpFile(IFileHeaderReadStrategy headerReader)
           // ,IGeometryFactory geoFactory)
        {
            _HeaderReader = headerReader;
           // _GeoFactory = geoFactory;
        }

        private void InitHeader()
        {
            _FileHeader = _HeaderReader.ReadHeader();
        }
        public FileHeader Header
        {
            get
            {
                return _FileHeader;
            }
        }


    }
    public class ShpFile<ShapeT> : ShpFile
    {
        IShpContentReadStrategy<ShapeT> _ContentReader;
        public ShpFile(
           IFileHeaderReadStrategy headerReader
           , IShpContentReadStrategy<ShapeT> contentReader
           )
            : base(headerReader)
        {
            Contract.Requires(headerReader != null);

            _ContentReader = contentReader;
        }
        public ShapeT ReadShape(int recPos)
        {
            return _ContentReader.ReadShape(recPos);
        }
        // writer todo...

    }


    public struct ShpRecordHeader
    {
        public ShpRecordHeader(int id, int cLength)
        {
            ID = id;
            ContentLength = cLength;
        }
        public int ID;
        public int ContentLength;
    }
}
