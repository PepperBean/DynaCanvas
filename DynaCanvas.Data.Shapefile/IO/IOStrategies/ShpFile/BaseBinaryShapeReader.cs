using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data.Shapefile.IO
{
    public abstract class BaseBinaryShapeReader<T> : IShpContentReadStrategy<T>
    {
        protected string _FilePath;

        public BaseBinaryShapeReader(string filePath)
        {
            Contract.Requires(!string.IsNullOrEmpty(filePath));
            Contract.Requires(File.Exists(filePath));
            _FilePath = filePath;

        }
        protected ShpRecordHeader ReadRecordHeader(int recPos)
        {
            var biReader = FileReaderCache.GetFileReadItems(_FilePath).BinaryReader;
            biReader.BaseStream.Seek(recPos, 0);
            var rn = Util.SwapByteOrder(biReader.ReadInt32());
            var cl = Util.SwapByteOrder(biReader.ReadInt32());
            ShpRecordHeader rh = new ShpRecordHeader(rn, cl);

            return rh;
        }

        public abstract T ReadShape(int recPos);

    }
}
