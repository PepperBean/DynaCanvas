using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile.IO;

namespace DynaCanvas.Data.Shapefile
{
    public class Shapefile
    {
        string _FileName;
        string _MainFilePath;
        string _IndexFilePath;
        string _DBFileFilePath;

        ShpFile _MainFile;
        ShxFile _IndexFile;
        DBFile _DBFile;

        public Shapefile(string fullName)
        {
            Contract.Requires(!string.IsNullOrEmpty(fullName));
            InitFilePath(fullName);
            LoadIndexFile();

        }
        private void InitFilePath(string fullName)
        {
            Contract.Requires(File.Exists(fullName));
            Contract.Ensures(File.Exists(_MainFilePath));
            Contract.Ensures(File.Exists(_IndexFilePath));
            Contract.Ensures(File.Exists(_DBFileFilePath));

            _FileName = Path.GetFileNameWithoutExtension(fullName);
            _MainFilePath = Path.ChangeExtension(fullName, Util.SHP_SUFFIX);
            _IndexFilePath = Path.ChangeExtension(fullName, Util.SHX_SUFFIX);
            _DBFileFilePath = Path.ChangeExtension(fullName, Util.DBF_SUFFIX);
        }

        private void LoadIndexFile()
        {
            // temp
            IShxContentReadStrategy contentReader = new BinaryShxContentReader(_IndexFilePath);
            IFileHeaderReadStrategy headerReader = new BinaryFileHeaderReader(_IndexFilePath);
            _IndexFile = new ShxFile(headerReader, contentReader);
        }


    }
}
