using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile;

namespace DynaCanvas.Data.Shapefile.IO
{
    public interface IFileHeaderReadStrategy
    {
        FileHeader ReadHeader();
    }
}
