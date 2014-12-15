using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace DynaCanvas.SpatialIndex.RStarTree
{
    public interface IEntry
    {
        int ID
        {
            get;
        }
        Envelope MBR
        {
            get;
        }
    }

    public class DataEntry : IEntry
    {
        public DataEntry(int id, Envelope mbr)
        {
            ID = id;
            MBR = mbr;
        }

        public int ID
        {
            get;
            private set;
        }

        public Envelope MBR
        {
            get;
            private set;
        }
    }
}
