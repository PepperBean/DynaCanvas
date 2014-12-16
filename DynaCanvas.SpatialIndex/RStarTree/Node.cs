using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace DynaCanvas.SpatialIndex.RStarTree
{
    public class NonLeafNode : INode<INode>
    {
        public NonLeafNode(int id)
        {

            _ID = id;
            MBR = default(Envelope);
        }

        ConcurrentDictionary<int, INode> _entries
            = new ConcurrentDictionary<int, INode>();
        private int _ID;

        public bool IsLeaf
        {
            get
            {
                return false;
            }
        }

        public bool IsRoot
        {
            get { return Level == 1; }
        }

        public int EntryCount
        {
            get { return _entries.Values.Count; }
        }

        public int Level
        {
            get;
            set;
        }

        public INode GetEntry(int id)
        {
            return _entries[id];
        }

        public void AddEntry(INode entry)
        {
            _entries.AddOrUpdate(entry.ID, entry, (i, n) => entry);
            entry.ParentNode = this;
            if (this.MBR == null)
            {
                this.RefreshMBR();
            }
            this.MBR = this.MBR.ExpandedBy(entry.MBR);
        }
        public void AddEntries(IEnumerable<IEntry> entries)
        {
            foreach (var e in entries)
            {
                AddEntry(e as INode);
            }
        }
        public void RemoveEntry(int id)
        {
            INode n;
            _entries.TryRemove(id, out n);
            RefreshMBR();
        }

        public Envelope MBR
        {
            get;
            private set;
        }

        public int ID
        {
            get { return _ID; }
        }

        public void RefreshMBR()
        {
            foreach (var e in _entries.Values)
            {
                if (e is Leaf)
                {
                    var l = e as Leaf;
                    l.RefreshMBR();
                }

                if (e is NonLeafNode)
                {
                    var l = e as NonLeafNode;
                    l.RefreshMBR();
                }
            }
            MBR = this._entries.Values.ExtentAll();
        }


        public INode ParentNode
        {
            get;
            set;
        }

        public IEnumerable<IEntry> Entries
        {
            get { return _entries.Values; }
        }


        public void UpdateSubTreeLevel(int currentLevel)
        {
            Level = currentLevel;
            foreach (var e in Entries)
            {
                if (e is NonLeafNode)
                    (e as NonLeafNode).UpdateSubTreeLevel(currentLevel + 1);
                else
                    (e as Leaf).Level = currentLevel + 1;
            }

        }

    }

    public class Leaf : INode<DataEntry>
    {
        public Leaf(int id)
        {
            ID = id;
            MBR = default(Envelope);
        }
        ConcurrentDictionary<int, DataEntry>
            _entries = new ConcurrentDictionary<int, DataEntry>();
        public IEnumerable<IEntry> Entries
        {
            get { return _entries.Values; }
        }

        public DataEntry GetEntry(int entryId)
        {
            return _entries[entryId];
        }

        public void AddEntry(DataEntry entry)
        {
            _entries.AddOrUpdate(entry.ID, entry, (i, d) => { return entry; });
            if (this.MBR == null)
            {
                this.RefreshMBR();
            }
            this.MBR = this.MBR.ExpandedBy(entry.MBR);
        }
        public void AddEntries(IEnumerable<IEntry> entries)
        {
            foreach (var e in entries)
            {
                AddEntry(e as DataEntry);
            }
        }

        public void RemoveEntry(int id)
        {
            DataEntry de;
            _entries.TryRemove(id, out de);
            RefreshMBR();
        }

        public void RefreshMBR()
        {
            //foreach (var e in _entries.Values)
            //{
            //    if (e is Leaf)
            //    {
            //        var l = e as Leaf;
            //        l.RefreshMBR();
            //    }

            //    if (e is NonLeafNode)
            //    {
            //        var l = e as NonLeafNode;
            //        l.RefreshMBR();
            //    }
            //}
            MBR = this._entries.Values.ExtentAll();
        }

        public bool IsLeaf
        {
            get { return true; }
        }

        public bool IsRoot
        {
            get { return Level == 1; }
        }

        public int Level
        {
            get;
            set;
        }

        public INode ParentNode
        {
            get;
            set;
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
