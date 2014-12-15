using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace DynaCanvas.SpatialIndex.RStarTree
{
    public interface INode : IEntry
    {
        bool IsLeaf
        {
            get;
        }
        bool IsRoot
        {
            get;
        }
        int Level
        {
            get;
            set;
        }
        INode ParentNode
        {
            get;
            set;
        }
        IEnumerable<IEntry> Entries
        {
            get;
        }
        void RefreshMBR();
    }
    public interface INode<ET> : INode
        where ET : IEntry
    {
        ET GetEntry(int entryId);
        void AddEntry(ET entry);
        void RemoveEntry(int id);
    }


    public static class INodeExt
    {
        public static Envelope ExtentAll<T>(this IEnumerable<T> entries)
            where T : IEntry
        {
            var etor = entries.GetEnumerator();
            Envelope env;
            if (etor.MoveNext())
            {
                env = etor.Current.MBR;
            }
            else
            {
                return default(Envelope);
            }
            while (etor.MoveNext())
            {
                env = env.ExpandedBy(etor.Current.MBR);
            }
            return env;
        }

    }
}
