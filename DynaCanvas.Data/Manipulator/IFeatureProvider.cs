using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Geometries;
using GeoAPI.Geometries;

namespace DynaCanvas.Data.Manipulator
{
    public interface IFeatureProvider : IDisposable
    {
        Collection<Feature> GetFeaturesBySpatialRelation(IEnvelope range
            , SRPredicate relation);
        Collection<Feature> GetFeatureaBySpatialRelation(IGeometry range
            , SRPredicate relation);
        //Collection<uint> GetFeatureIDsBySpatialRelation(IEnvelope range
        //    , SpatialRelationPredicate relation);
        //Collection<uint> GetFeatureIDsBySpatialRelation(IGeometry range
        //    , SpatialRelationPredicate relation);
        uint GetFeatureCount();

        IEnvelope GetExtents();

        
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
