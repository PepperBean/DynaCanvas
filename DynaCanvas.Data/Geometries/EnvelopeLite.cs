using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data.Geometries
{
    public struct EnvelopeLite:GeoAPI.Geometries.IEnvelope
    {
        public double Area
        {
            get { throw new NotImplementedException(); }
        }

        public GeoAPI.Geometries.ICoordinate Centre
        {
            get { throw new NotImplementedException(); }
        }

        public bool Contains(GeoAPI.Geometries.IEnvelope other)
        {
            throw new NotImplementedException();
        }

        public bool Contains(GeoAPI.Geometries.ICoordinate p)
        {
            throw new NotImplementedException();
        }

        public bool Contains(double x, double y)
        {
            throw new NotImplementedException();
        }

        public bool Covers(GeoAPI.Geometries.IEnvelope other)
        {
            throw new NotImplementedException();
        }

        public bool Covers(GeoAPI.Geometries.ICoordinate p)
        {
            throw new NotImplementedException();
        }

        public bool Covers(double x, double y)
        {
            throw new NotImplementedException();
        }

        public double Distance(GeoAPI.Geometries.IEnvelope env)
        {
            throw new NotImplementedException();
        }

        public void ExpandBy(double deltaX, double deltaY)
        {
            throw new NotImplementedException();
        }

        public void ExpandBy(double distance)
        {
            throw new NotImplementedException();
        }

        public void ExpandToInclude(GeoAPI.Geometries.IEnvelope other)
        {
            throw new NotImplementedException();
        }

        public void ExpandToInclude(double x, double y)
        {
            throw new NotImplementedException();
        }

        public void ExpandToInclude(GeoAPI.Geometries.ICoordinate p)
        {
            throw new NotImplementedException();
        }

        public double Height
        {
            get { throw new NotImplementedException(); }
        }

        public void Init(double x1, double x2, double y1, double y2)
        {
            throw new NotImplementedException();
        }

        public void Init(GeoAPI.Geometries.ICoordinate p1, GeoAPI.Geometries.ICoordinate p2)
        {
            throw new NotImplementedException();
        }

        public void Init(GeoAPI.Geometries.IEnvelope env)
        {
            throw new NotImplementedException();
        }

        public void Init(GeoAPI.Geometries.ICoordinate p)
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public GeoAPI.Geometries.IEnvelope Intersection(GeoAPI.Geometries.IEnvelope env)
        {
            throw new NotImplementedException();
        }

        public bool Intersects(GeoAPI.Geometries.IEnvelope other)
        {
            throw new NotImplementedException();
        }

        public bool Intersects(double x, double y)
        {
            throw new NotImplementedException();
        }

        public bool Intersects(GeoAPI.Geometries.ICoordinate p)
        {
            throw new NotImplementedException();
        }

        public bool IsNull
        {
            get { throw new NotImplementedException(); }
        }

        public double MaxX
        {
            get { throw new NotImplementedException(); }
        }

        public double MaxY
        {
            get { throw new NotImplementedException(); }
        }

        public double MinX
        {
            get { throw new NotImplementedException(); }
        }

        public double MinY
        {
            get { throw new NotImplementedException(); }
        }

        public bool Overlaps(double x, double y)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(GeoAPI.Geometries.ICoordinate p)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(GeoAPI.Geometries.IEnvelope other)
        {
            throw new NotImplementedException();
        }

        public void SetCentre(GeoAPI.Geometries.ICoordinate centre, double width, double height)
        {
            throw new NotImplementedException();
        }

        public void SetCentre(GeoAPI.Geometries.IPoint centre)
        {
            throw new NotImplementedException();
        }

        public void SetCentre(GeoAPI.Geometries.ICoordinate centre)
        {
            throw new NotImplementedException();
        }

        public void SetCentre(GeoAPI.Geometries.IPoint centre, double width, double height)
        {
            throw new NotImplementedException();
        }

        public void SetCentre(double width, double height)
        {
            throw new NotImplementedException();
        }

        public void SetToNull()
        {
            throw new NotImplementedException();
        }

        public void Translate(double transX, double transY)
        {
            throw new NotImplementedException();
        }

        public GeoAPI.Geometries.IEnvelope Union(GeoAPI.Geometries.IEnvelope box)
        {
            throw new NotImplementedException();
        }

        public GeoAPI.Geometries.IEnvelope Union(GeoAPI.Geometries.ICoordinate coord)
        {
            throw new NotImplementedException();
        }

        public GeoAPI.Geometries.IEnvelope Union(GeoAPI.Geometries.IPoint point)
        {
            throw new NotImplementedException();
        }

        public double Width
        {
            get { throw new NotImplementedException(); }
        }

        public void Zoom(double perCent)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(GeoAPI.Geometries.IEnvelope other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(GeoAPI.Geometries.IEnvelope other)
        {
            throw new NotImplementedException();
        }
    }
}
