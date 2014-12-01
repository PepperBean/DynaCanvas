using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data.Geometries
{
    /// <summary>
    /// Spatial operations
    /// </summary>
    public enum SpatialRelationPredicate
    {
        Contains,
        ContainsProperly,
        Covers,
        CoveredBy,
        Crosses,
        Disjoint,
        Intersects,
        Overlaps,
        Touches,
        Within,
    }
}
