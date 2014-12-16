using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Geometries;
using DynaCanvas.Data.Manipulator;
using GeoAPI.Geometries;

namespace DynaCanvas.Data
{
   public interface IVectorLayer:ILayer
    {
       IVectorFeature GetFeatureByID(int FeatureID);
       IEnumerable<IVectorFeature> GetFeatureByRange(Envelope env);
       IEnumerable<IVectorFeature> GetFeatureByRange(IGeometry geo, SRPredicate relation);

    }
}
