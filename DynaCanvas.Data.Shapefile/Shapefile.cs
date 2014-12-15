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
      //  IGeometryFactory _GeoFactory;
        IShapefileIOFactory _IOFactory;
        ShpFile _ShpFile;
        ShxFile _ShxFile;
        DBFile _DBFile;


        public Shapefile(string fullName)//, IGeometryFactory geoFactory)
        {
            Contract.Requires(!string.IsNullOrEmpty(fullName));

           // _GeoFactory = geoFactory;
            InitFilePath(fullName);
            _IOFactory = new DefaultShapefileIOFactory(_ShpFilePath, _ShxFilePath, _DBFileFilePath);
            InitShapefile();
        }

        private void InitShapefile()
        {
            _LayerName = Path.GetFileNameWithoutExtension(_FileName);
            InitShxFile();
            _ShapeType = (ShapeType)_ShxFile.ShapeType;
            _MBR = _ShxFile.MBR;
            InitShpFile();
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

        private void InitShxFile()
        {
            Contract.Requires(_ShxFile != null);
            _ShxFile = new ShxFile(_IOFactory);
        }

        private void InitShpFile()
        {
            Contract.Requires(_ShpFile != null);
            _ShpFile = new ShpFile(_IOFactory);
        }

        private void InitDBFile()
        {
            // TODO...
        }

        




        public string LayerName
        {
            get { return _LayerName; }
        }


    }
}
