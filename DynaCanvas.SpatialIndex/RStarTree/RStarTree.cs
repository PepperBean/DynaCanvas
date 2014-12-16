using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace DynaCanvas.SpatialIndex.RStarTree
{
    public class RStarTree : ISpatialIndex
    {

        private int _maxEntryCount;
        private int _minEntryCount;
        private ConcurrentDictionary<int, INode> _nodesMap
            = new ConcurrentDictionary<int, INode>();
        public INode RootNode
        {
            get
            {
                return _root;
            }
        }
        // 2 <= m <= M/2
        private const int DEFAULT_MAX_NODE_ENTRIES = 12;
        private const int DEFAULT_MIN_NODE_ENTRIES = 6;
        // private const int CHOOSE_LEAF_P = 32;

        INode _root;
        public RStarTree(int maxE, int minE)
        {
            _maxEntryCount = maxE;
            _minEntryCount = minE;
            init();
        }
        public RStarTree()
        {
            _maxEntryCount = DEFAULT_MAX_NODE_ENTRIES;
            _minEntryCount = DEFAULT_MIN_NODE_ENTRIES;
            init();
        }

        int _NonLeafNodeID = 0;
        private int NonLeafNodeIDGenerater()
        {
            return --_NonLeafNodeID;
        }
        private void init()
        {
            if (_maxEntryCount < 2)
            {
                _maxEntryCount = DEFAULT_MAX_NODE_ENTRIES;
            }

            if (_minEntryCount < 1 || _minEntryCount > _maxEntryCount / 2)
            {
                _minEntryCount = _maxEntryCount / 2;
            }

        }
        public void Insert(int id, Envelope env)
        {
            DataEntry de = new DataEntry(id, env);
            InsertNode(de);
        }

        //public void Insert(ICollection<Tuple<int, Envelope>> dataList)
        //{
        //    Stopwatch s1 = new Stopwatch();
        //    s1.Start();
        //    dataList.AsParallel().ToList().ForEach(e =>
        //    {
        //        Insert(e.Item1, e.Item2);
        //    });

        //    s1.Stop();
        //    Debug.Assert(false, "初始化耗时：" + s1.ElapsedMilliseconds);
        //}

        public void Remove(int id)
        {
           // todo...
            throw new NotImplementedException();
        }

        public IEnumerable<int> Search(Envelope extent)
        {

            List<int> result = new List<int>();
            if (_root == null)
            {
                return result;
            }

            if (!_root.MBR.Intersects(extent))
            {
                return result;
            }

            InnerSearch(extent, _root, ref   result);

            return result;
        }
        public void InnerSearch(Envelope extent, IEntry entry, ref List<int> result)
        {
            if (!extent.Intersects(entry.MBR))
            {
                return;
            }
            if (entry is NonLeafNode)
            {
                var node = entry as NonLeafNode;
                foreach (var n in node.Entries)
                {
                    InnerSearch(extent, n, ref result);
                }
            }
            else if (entry is Leaf)
            {
                var leaf = entry as Leaf;
                foreach (var e in leaf.Entries)
                {
                    if (extent.Intersects(e.MBR))
                    {
                        result.Add(e.ID);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryToAdd"></param>
        /// <param name="nodeAddTo"></param>
        /// <returns></returns>
        private INode ChooseSubTree(IEntry entryToAdd, INode nodeAddTo, int entryLevel = -1)
        {

            if (entryToAdd is NonLeafNode
                && nodeAddTo is NonLeafNode
                && (nodeAddTo as NonLeafNode).Entries.First() is Leaf)
            {
                return nodeAddTo;
            }

            if (entryToAdd is DataEntry
                && nodeAddTo is Leaf)
            {
                return nodeAddTo;
            }
            if (entryLevel == nodeAddTo.Level + 1)
            {
                if (entryToAdd is Leaf
                    && nodeAddTo is NonLeafNode
                    && !((nodeAddTo as NonLeafNode).Entries.First() is Leaf))
                {
                    return ChooseSubTree(entryToAdd, nodeAddTo, ++entryLevel);

                }
                else
                {
                    return nodeAddTo;
                }
            }
            NonLeafNode nlf = nodeAddTo as NonLeafNode;
            if (nlf.Entries.First() is Leaf)
            {
                var chosenEntryID = nlf.Entries//.AsParallel()
                                    .OrderBy(e => e.MBR.Area)
                                    .OrderBy(e => e.MBR.EnlargementSize(entryToAdd.MBR))
                                    .OrderBy(e =>
                                    {
                                        return ((e as Leaf).Entries
                                            .Sum(e2 => e2.MBR.OverlapSize(entryToAdd.MBR)));
                                    })
                                    .Take(2)
                                    .First().ID;
                return ChooseSubTree(entryToAdd, _nodesMap[chosenEntryID], entryLevel);
            }
            else
            {
                var chosenEntryID = nlf.Entries//.AsParallel()
                                    .OrderBy(e => e.MBR.Area)
                                    .OrderBy(e => e.MBR.EnlargementSize(entryToAdd.MBR))
                                    .First().ID;
                return ChooseSubTree(entryToAdd, _nodesMap[chosenEntryID], entryLevel);
            }
        }
        private void Split(INode nodeAfterEntryAdded)
        {

            INode newN1, newN2;
            if (nodeAfterEntryAdded is NonLeafNode)
            {
                var result = SplitEntries(nodeAfterEntryAdded);
                NonLeafNode n1 = new NonLeafNode(NonLeafNodeIDGenerater());
                n1.Level = nodeAfterEntryAdded.Level;
                n1.AddEntries(result.Item1);
                NonLeafNode n2 = new NonLeafNode(NonLeafNodeIDGenerater());
                n2.AddEntries(result.Item2);
                n2.Level = nodeAfterEntryAdded.Level;
                newN1 = n1;
                newN2 = n2;
            }
            else
            {
                var result = SplitEntries(nodeAfterEntryAdded);
                Leaf n1 = new Leaf(NonLeafNodeIDGenerater());
                n1.Level = nodeAfterEntryAdded.Level;
                n1.AddEntries(result.Item1);
                Leaf n2 = new Leaf(NonLeafNodeIDGenerater());
                n2.Level = nodeAfterEntryAdded.Level;
                n2.AddEntries(result.Item2);
                newN1 = n1;
                newN2 = n2;
            }

            _nodesMap.AddOrUpdate(newN1.ID, newN1, (i, n) => { return newN1; });
            _nodesMap.AddOrUpdate(newN2.ID, newN2, (i, n) => { return newN2; });


            NonLeafNode parentNode;
            if (!nodeAfterEntryAdded.IsRoot)
            {
                parentNode = nodeAfterEntryAdded.ParentNode as NonLeafNode;
                parentNode.RemoveEntry(nodeAfterEntryAdded.ID);
                _nodesMap.TryRemove(nodeAfterEntryAdded.ID, out nodeAfterEntryAdded);
            }
            else
            {
                parentNode = new NonLeafNode(NonLeafNodeIDGenerater());
                parentNode.Level = 1;
                _root = parentNode;
                _nodesMap.AddOrUpdate(_root.ID, _root, (i, n) => { return _root; });
                _nodesMap.TryRemove(nodeAfterEntryAdded.ID, out nodeAfterEntryAdded);

            }
            parentNode.AddEntry(newN1);
            parentNode.AddEntry(newN2);

            parentNode.UpdateSubTreeLevel(parentNode.Level);

            if (parentNode.Entries.Count() > DEFAULT_MAX_NODE_ENTRIES)
            {
                OverflowTreatment(parentNode);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeAfterEntryAdded"></param>
        private Tuple<IEnumerable<IEntry>, IEnumerable<IEntry>>
            SplitEntries(INode nodeAfterEntryAdded)
        {
            var entryListAfterAddedOrderX
                = nodeAfterEntryAdded.Entries
                .OrderBy(e => e.MBR.MinX)
                .OrderBy(e => e.MBR.MaxX)
                .ToList();
            var entryListAfterAddedOrderY
                = nodeAfterEntryAdded.Entries
                .OrderBy(e => e.MBR.MinY)
                .OrderBy(e => e.MBR.MaxY)
                .ToList();

            double totalMarginX = 0, totalMarginY = 0;
            for (int k = 1; k <= _maxEntryCount - 2 * _minEntryCount + 2; k++)
            {
                int i = _minEntryCount - 1 + k;
                //X-----------------------------------------------------------------------------------------------------
                var xFrontMbr = entryListAfterAddedOrderX.Take(i).ExtentAll();
                var xBackMbr = entryListAfterAddedOrderX.Skip(i).ExtentAll();
                totalMarginX += (xFrontMbr.Margin() + xBackMbr.Margin());

                //Y------------------------------------------------------------------------------------------------------
                var yFrontMbr = entryListAfterAddedOrderY.Take(i).ExtentAll();
                var yBackMbr = entryListAfterAddedOrderY.Skip(i).ExtentAll();
                totalMarginY += (yFrontMbr.Margin() + yBackMbr.Margin());
            }

            List<IEntry> chosenEntryList;
            if (totalMarginX < totalMarginY)
            {
                chosenEntryList = entryListAfterAddedOrderX;
            }
            else
            {
                chosenEntryList = entryListAfterAddedOrderY;
            }
            double overLapValue = double.MaxValue;
            double areaValue = double.MaxValue;
            int splitIndex = -1;
            for (int k = 1; k <= _maxEntryCount - 2 * _minEntryCount + 2; k++)
            {
                int i = _minEntryCount - 1 + k;
                var xFrontMbr = chosenEntryList.Take(i).ExtentAll();
                var xBackMbr = chosenEntryList.Skip(i).ExtentAll();

                var olv = xFrontMbr.OverlapSize(xBackMbr);
                if (olv < overLapValue)
                {
                    splitIndex = i;
                    areaValue = xFrontMbr.Area + xBackMbr.Area;
                    overLapValue = olv;
                }
                else if (Math.Abs(olv - overLapValue) < double.Epsilon)
                {
                    var av = xFrontMbr.Area + xBackMbr.Area;
                    if (av < areaValue)
                    {
                        splitIndex = i;
                        areaValue = av;
                    }
                }
            }

            var e1 = chosenEntryList.Take(splitIndex);
            var e2 = chosenEntryList.Skip(splitIndex);
            return new Tuple<IEnumerable<IEntry>, IEnumerable<IEntry>>(e1, e2);
        }

        HashSet<int> _OverflowTreatmentToken = new HashSet<int>();
        private void OverflowTreatment(INode nodeNeedTreat)
        {
            if (nodeNeedTreat.Level != _root.Level
                && !_OverflowTreatmentToken.Contains(nodeNeedTreat.Level))
            {
                _OverflowTreatmentToken.Add(nodeNeedTreat.Level);
                if (nodeNeedTreat is Leaf)
                {
                    ReInsert<DataEntry>(nodeNeedTreat as Leaf);
                }
                else
                {
                    ReInsert<INode>(nodeNeedTreat as NonLeafNode);
                }

            }
            else
            {
                Split(nodeNeedTreat);

            }
        }

        readonly int REINSERT_P = (int)(DEFAULT_MAX_NODE_ENTRIES * 0.5);

        private void ReInsert<ET>(INode<ET> nodeAfterEntryAdded)
            where ET : IEntry
        {

            var nodeCenter = nodeAfterEntryAdded.MBR.Centre;
            var orderedEntries = nodeAfterEntryAdded.Entries
                                .OrderByDescending(e => e.MBR.Centre.Distance(nodeCenter));
            var reinsertEntries = orderedEntries.Take(REINSERT_P).ToList();
            reinsertEntries.ForEach(e =>
            {
                INode o;
                nodeAfterEntryAdded.RemoveEntry(e.ID);
                _nodesMap.TryRemove(e.ID, out o);
            });
            nodeAfterEntryAdded.RefreshMBR();

            reinsertEntries.Reverse();
            foreach (var e in reinsertEntries)
            {
                if (e is INode)
                {
                    (e as INode).Level = (e as INode).ParentNode.Level + 1;
                }
                InsertNode(e, true);
            }

        }
        private void InsertNode(IEntry entry, bool reInsert = false)
        {

            if (!reInsert)
                _OverflowTreatmentToken.Clear();

            if (_root == null)
            {
                if (entry is DataEntry)
                {
                    var r = new Leaf(NonLeafNodeIDGenerater());
                    r.Level = 1;
                    r.AddEntry(entry as DataEntry);
                    _root = r;
                    _nodesMap.AddOrUpdate(_root.ID, _root, (i, n) => { return _root; });
                }
                else
                {
                    var r = entry as INode;
                    r.Level = 1;
                    //   r.AddEntry(entry as DataEntry);
                    _root = r;
                    _nodesMap.AddOrUpdate(_root.ID, _root, (i, n) => { return _root; });
                }
                return;
            }

            //_nodesMap.AddOrUpdate(entry.ID, entry, (i, n) => { return entry; });
            INode nodeAddTo;

            if (entry is INode)
            {
                if (_root is NonLeafNode)
                    (_root as NonLeafNode).UpdateSubTreeLevel(1);
                nodeAddTo = (entry as INode).Level == 1 ? _root : ChooseSubTree(entry, _root, (entry as INode).Level);

                (nodeAddTo as NonLeafNode).AddEntry(entry as INode);
                _nodesMap.AddOrUpdate(entry.ID, entry as INode, (i, n) => entry as INode);

                if ((nodeAddTo as NonLeafNode).Entries.Count() > _maxEntryCount)
                {
                    OverflowTreatment(nodeAddTo);
                }
            }
            else
            {
                nodeAddTo = ChooseSubTree(entry, _root);
                (nodeAddTo as Leaf).AddEntry(entry as DataEntry);
                //      _nodesMap.AddOrUpdate(entry.ID, entry as INode, (i, n) => entry as INode);
                if ((nodeAddTo as Leaf).Entries.Count() > _maxEntryCount)
                {
                    OverflowTreatment(nodeAddTo);
                }
            }

        }
    }
}
