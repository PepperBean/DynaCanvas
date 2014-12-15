using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data.Shapefile.IO
{
    public class DefaultShapefileIOFactory : IShapefileIOFactory
    {
        private string _ShpFilePath;
        private string _ShxFilePath;
        private string _DBFilePath;
        public DefaultShapefileIOFactory(string shpFilePath, string shxFilePath, string dbFilePath)
        {
            _ShpFilePath = shpFilePath;
            _ShxFilePath = shxFilePath;
            _DBFilePath = dbFilePath;
        }
        public IFileHeaderReadStrategy GetShpFileHeaderReader()
        {
            return new FileHeaderReader(_ShpFilePath);
        }
        public IFileHeaderReadStrategy GetShxFileHeaderReader()
        {
            return new FileHeaderReader(_ShxFilePath);
        }

        public IShpContentReadStrategy GetShpContentReader()
        {
            return new ShpContentReader(_ShpFilePath);
        }

        public IShxContentReadStrategy GetShxContentReader()
        {
            return new ShxContentReader(_ShxFilePath);
        }
    }
}
