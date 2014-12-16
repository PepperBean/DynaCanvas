using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile.IO;
using DynaCanvas.Data.Shapefile.Shapes;
using GeoAPI.Geometries;

namespace DynaCanvas.Data.Shapefile
{
    public class ShpFile
    {
        protected FileHeader _FileHeader;
      //  protected string _FilePath;
        private IFileHeaderReadStrategy _HeaderReader;
        private IShpContentReadStrategy _ContentReader;
        private IGeometryFactory _GeoFactory;
        private Envelope _MBR;
        private IShapefileIOFactory _IOFactory;

        public ShpFile(IShapefileIOFactory ioFactory)
        {
           // _FilePath = filePath;
            _IOFactory = ioFactory;
            _ContentReader = _IOFactory.GetShpContentReader();
            InitHeader();
        }

      
        private void InitHeader()
        {
            _HeaderReader = _IOFactory.GetShpFileHeaderReader();
            _FileHeader = _HeaderReader.ReadHeader();
            this._MBR = _FileHeader.MBR;
        }
        public Envelope MBR
        {
            get
            {
                return _MBR;
            }
        }

        public FeatureLite ReaderShape(int recHeaderPos, int recCLength)
        {

            return _ContentReader.ReaderShape(recHeaderPos, recCLength);
        }
        
    }

}
