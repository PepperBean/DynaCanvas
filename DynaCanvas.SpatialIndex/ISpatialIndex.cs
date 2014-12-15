using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace DynaCanvas.SpatialIndex
{
    public interface ISpatialIndex
    {
        void Insert(int id, Envelope mbr);
        IEnumerable<int> Search(Envelope mbr);
        void Remove(int id);
    }
}
