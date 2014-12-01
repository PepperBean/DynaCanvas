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
            Contract.Requires(File.Exists(filePath));
            _FilePath = filePath;
        }

        public ShxRecord[] ReadRecordes(int startIndex, int count)
        {

            var brShapeIndex = FileReaderCache.GetFileReadItems(_FilePath).BinaryReader;
            brShapeIndex.BaseStream.Position = 0;
            brShapeIndex.BaseStream.Seek(100 + startIndex * 8, 0);  //skip the header
            var records = new ShxRecord[count];

            for (int i = 0; i < count; i++)
            {
                int offset =2* Util.SwapByteOrder(brShapeIndex.ReadInt32()); // word to byte
                int cLength =2* Util.SwapByteOrder(brShapeIndex.ReadInt32());

                records[i] = new ShxRecord(offset, cLength);
            }
            return records;
        }

        public ShxRecord ReadRecorde(int index)
        {
            // temp
            return ReadRecordes(index, 1)[0];
        }
    }
}
