using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace DynaCanvas.Data.Manipulator
{
    interface IFeatureModifier : IDisposable
    {
        Feature AddFeature(IGeometry newGeo);
        Feature ChangeGeometry(uint fid, IGeometry newGeo);
        bool DeleteFeature(uint fid);

        string ConnectionInfo
        {
            get;
        }
        /// <summary>
        /// The spatial reference ID (CRS)
        /// </summary>
        int SRID { get; set; }

        IDataSource DataSource
        {
            get;
        }
    }
}
