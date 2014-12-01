using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace DynaCanvas.Data
{
    public class Feature
    {
        public Feature()
        {

        }
        public IGeometry Geometry
        {
            get;
            set;
        }
        public int FeatureID
        {
            get;
            private set;
        }
        public string LayerName
        {
            get;
            private set;
        }

        public string GeometryType
        {
            get
            {
                return Geometry==null?string.Empty:Geometry.GeometryType;
            }
        }

    }
}
