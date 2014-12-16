using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile.IO;
using DynaCanvas.Data.Shapefile.Shapes;
using DynaCanvas.SpatialIndex;
using DynaCanvas.SpatialIndex.RStarTree;
using GeoAPI.Geometries;

namespace DynaCanvas.Data.Shapefile
{
    public class Shapefile : IVectorLayer
    {
        string _FileName;
        string _ShpFilePath;
        string _ShxFilePath;
        string _DBFileFilePath;

        Envelope _MBR;
        ShapeType _ShapeType = ShapeType.NullShape;
        string _LayerName;
        //  IGeometryFactory _GeoFactory;
        IShapefileIOFactory _IOFactory;
        ISpatialIndex _SpatialIndex;
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
            InitSpatialIndex();
        }

        private void InitSpatialIndex()
        {
            _SpatialIndex = new RStarTree();
            BuildTreeInMemo();
        }

        private void BuildTreeInMemo()
        {
            foreach (var recHeader in this._ShxFile.Records)
            {
                FeatureLite rec = _ShpFile.ReaderShape(recHeader.Offset, recHeader.ContentLength);
                _SpatialIndex.Insert(rec.FeatureID, rec.MBR);
            }
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
            Contract.Ensures(_ShxFile != null);
            _ShxFile = new ShxFile(_IOFactory);
        }

        private void InitShpFile()
        {
            Contract.Ensures(_ShpFile != null);
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

        public IVectorFeature GetFeatureByID(int FeatureID)
        {
            // feature begins from 1
            Contract.Requires(FeatureID > 0);
            var idxRec = _ShxFile.Records[FeatureID];
            return this._ShpFile.ReaderShape(idxRec.Offset, idxRec.ContentLength);
        }

        public IEnumerable<IVectorFeature> GetFeatureByRange(Envelope env)
        {
            var featureIds = _SpatialIndex.Search(env);
            foreach (var fid in featureIds)
            {
                yield return GetFeatureByID(fid);
            }
        }

        public IEnumerable<IVectorFeature> GetFeatureByRange(IGeometry geo, Geometries.SRPredicate relation)
        {
            // todo...
            throw new NotImplementedException();
        }


        public Envelope MBR
        {
            get { return _MBR; }
        }
    }
}
