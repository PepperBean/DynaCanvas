using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace DynaCanvas.Data
{
    public interface IVectorFeature:IFeature
    {
        Envelope MBR
        {
            get;
        }

        OgcGeometryType GeometryType
        {
            get;
        }

        IGeometry GetGeometry();
        void SetGeometry();
    }
}
