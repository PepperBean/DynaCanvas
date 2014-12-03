using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DynaCanvas.Data.Shapefile.IO;
using DynaCanvas.Data.Shapefile.Shapes;
using GeoAPI.Geometries;

namespace DynaCanvas.Data.Shapefile
{
    public class Shapefile// : ILayer
    {
        string _FileName;
        string _ShpFilePath;
        string _ShxFilePath;
        string _DBFileFilePath;

        BoundingBox _MBR;
        ShapeType _ShapeType = ShapeType.NullShape;
        string _LayerName;
        IGeometryFactory _GeoFactory;

        ShpFile _ShpFile;
        ShxFile _ShxFile;
        DBFile _DBFile;


        public Shapefile(string fullName,IGeometryFactory geoFactory)
        {
            Contract.Requires(!string.IsNullOrEmpty(fullName));

            _GeoFactory = geoFactory;
            InitFilePath(fullName);
            InitShapefile();
        }

        private void InitShapefile()
        {
            _LayerName = Path.GetFileNameWithoutExtension(_FileName);
            LoadShxFile();
            _ShapeType = (ShapeType)_ShxFile.Header.ShapeType;
            _MBR = _ShxFile.Header.MBR;
            LoadShpFile();
        }

        private void InitFilePath(string fullName)
        {
            Contract.Requires(File.Exists(fullName));
            Contract.Ensures(File.Exists(_ShpFilePath));
            Contract.Ensures(File.Exists(_ShxFilePath));
            Contract.Ensures(File.Exists(_DBFileFilePath));

            _FileName = Path.GetFileNameWithoutExtension(fullName);
            _ShpFilePath = Path.ChangeExtension(fullName, Util.SHP_SUFFIX);
            _ShxFilePath = Path.ChangeExtension(fullName, Util.SHX_SUFFIX);
            _DBFileFilePath = Path.ChangeExtension(fullName, Util.DBF_SUFFIX);
        }

        private void LoadShxFile()
        {
            // temp
            IShxContentReadStrategy contentReader = new BinaryShxContentReader(_ShxFilePath);
            IFileHeaderReadStrategy headerReader = new BinaryFileHeaderReader(_ShxFilePath);
            _ShxFile = new ShxFile(headerReader, contentReader);
        }

        private void LoadShpFile()
        {
            Contract.Requires(_ShapeType != ShapeType.NullShape);
            Contract.Requires(_ShpFile != null);

            switch (_ShapeType)
            {
                case ShapeType.Point:
                    {
                        BinaryPointReader contentReader = new BinaryPointReader(_ShpFilePath);
                        IFileHeaderReadStrategy headerReader = new BinaryFileHeaderReader(_ShpFilePath);
                        _ShpFile = new ShpFile<Point>(headerReader, contentReader);
                    }
                    break;
                case ShapeType.Polyline:
                    {
                        BinaryPolylineReader contentReader = new BinaryPolylineReader(_ShpFilePath);
                        IFileHeaderReadStrategy headerReader = new BinaryFileHeaderReader(_ShpFilePath);
                        _ShpFile = new ShpFile<Polyline>(headerReader, contentReader);
                    }
                    break;
                case ShapeType.Polygon:
                    {
                        BinaryPolygonReader contentReader = new BinaryPolygonReader(_ShpFilePath);
                        IFileHeaderReadStrategy headerReader = new BinaryFileHeaderReader(_ShpFilePath);
                        _ShpFile = new ShpFile<Polygon>(headerReader, contentReader);
                    }
                    break;
            }
        }

        //public Feature GetFeature(int fid)  // feature id start form 1
        //{
           
    //        var rPos=_ShxFile.Records[fid - 1].Offset;
    //        BinaryReader br;
    //        MemoryStream ms=new MemoryStream(
    //        switch (_ShapeType)

    //{
    //          //  case ShapeType.Point:

    ////	default:
    //}
            
        //}



        public string LayerName
        {
            get { return _LayerName; }
        }

        //public GeoAPI.Geometries.IEnvelope Extent
        //{
        //    get { throw new NotImplementedException(); }
        //}

    }
}
