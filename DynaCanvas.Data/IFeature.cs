using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace DynaCanvas.Data
{
    public interface IFeature
    {
        int FeatureID
        {
            get;
        }
        ILayer Layer
        {
            get;
        }

    }
}
