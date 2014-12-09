using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile;

namespace DynaCanvas.Data.Shapefile.IO
{
    public class BinaryShxContentReader : IShxContentReadStrategy
    {
        private string _FilePath;
        const int _ShxRecordLength = 8;
        public BinaryShxContentReader(string filePath)
        {
            Contract.Requires(!string.IsNullOrEmpty(filePath));
            Contract.Requires(File.Exists(filePath));
            _FilePath = filePath;
        }

        public ShxRecord[] ReadRecordes(int startRecIndex, int count)
        {
            using (var stream = MMFCache.GetFileReadItems(_FilePath)
                .MMF.CreateViewStream(100 + startRecIndex * 8, count * 16))
            {
                using (var brShapeIndex = new BinaryReader(stream))
                {
                    var records = new ShxRecord[count];

                    for (int i = 0; i < count; i++)
                    {
                        int offset = 2 * Util.SwapByteOrder(brShapeIndex.ReadInt32()); // word to byte
                        int cLength = 2 * Util.SwapByteOrder(brShapeIndex.ReadInt32());

                        records[i] = new ShxRecord(offset, cLength);
                    }
                    return records;
                }
            }
        }

        public ShxRecord ReadRecorde(int recIndex)
        {
            // temp
            return ReadRecordes(recIndex, 1)[0];
        }
    }
}
