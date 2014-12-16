using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace DynaCanvas.Data.Shapefile.Shapes
{
    public enum ShapeType : uint
    {
        NullShape = 0,

        Point = 1,
        Polyline = 3,
        Polygon = 5,
        MultiPoint = 8,

        PointZ = 11,
        PolylineZ = 13,
        PolygonZ = 15,
        MultiPointZ = 18,

        PointM = 21,
        PolylineM = 23,
        PolygonM = 25,
        MultiPointM = 28,

        MultiPatch = 31
    }
    public static class ShapeTypeExt
    {
        public static OgcGeometryType ToOgcGeometrType(this ShapeType shapeType)
        {
            switch (shapeType)
            {
                case ShapeType.PointM:
                case ShapeType.PointZ:
                case ShapeType.Point:
                    return OgcGeometryType.Point;
                case ShapeType.MultiPointM:
                case ShapeType.MultiPointZ:
                case ShapeType.MultiPoint:
                    return OgcGeometryType.MultiPoint;
                case ShapeType.PolylineM:
                case ShapeType.PolylineZ:
                case ShapeType.Polyline:
                    return OgcGeometryType.LineString;
                case ShapeType.PolygonM:
                case ShapeType.PolygonZ:
                case ShapeType.Polygon:
                    return OgcGeometryType.Polygon;
                default:
                    return default(OgcGeometryType);
            }
        }

        public static ShapeType ToShapeType(this OgcGeometryType ogcGeoType)
        {
            switch (ogcGeoType)
            {
                case OgcGeometryType.Point:
                    return ShapeType.Point;
                case OgcGeometryType.Polygon:
                    return ShapeType.Polygon;
                case OgcGeometryType.LineString:
                    return ShapeType.Polyline;
                default:
                    return ShapeType.NullShape;
            }
        }

    }
}
