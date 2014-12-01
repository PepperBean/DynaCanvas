using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.DataStructures;

namespace DynaCanvas.Data
{
    public interface ILayer
    {
        string LayerName
        {
            get;
        }

        GeoAPI.Geometries.IEnvelope Extent
        {
            get;
        }
        
        
    }
}
