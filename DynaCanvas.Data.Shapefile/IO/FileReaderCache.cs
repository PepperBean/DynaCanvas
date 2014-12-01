using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data.Shapefile.IO
{
    internal static class FileReaderCache
    {
        static Dictionary<string, WeakReference<FileReaderItems>>
             _Cache = new Dictionary<string, WeakReference<FileReaderItems>>();
        public static FileReaderItems GetFileReadItems(string filePath)
        {
            Contract.Requires(File.Exists(filePath));

            FileReaderItems fi;
            if (_Cache.ContainsKey(filePath))
            {
                if (!_Cache[filePath].TryGetTarget(out fi))
                {
                    fi = new FileReaderItems(filePath);
                    _Cache[filePath] = new WeakReference<FileReaderItems>(fi);
                }
            }
            else
            {
                fi = new FileReaderItems(filePath);
                _Cache.Add(filePath, new WeakReference<FileReaderItems>(fi));
            }
            return fi;
        }
    }

    internal class FileReaderItems
    {
        public FileReaderItems(string filePath)
        {
            FilePath = filePath;
            FileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            BinaryReader = new BinaryReader(FileStream, System.Text.Encoding.Unicode);
            //BigEndianBinaryReader = new BinaryReader(FileStream, System.Text.Encoding.BigEndianUnicode);
        }
        public readonly string FilePath;
        public readonly FileStream FileStream;
        public readonly BinaryReader BinaryReader;
        //public readonly BinaryReader BigEndianBinaryReader;
    }
}
