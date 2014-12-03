using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data.Shapefile.IO
{

    public interface IShpContentReadStrategy
    {
     //   byte[] ReaderShape(int recPos);
    }
    public interface IShpContentReadStrategy<out T> : IShpContentReadStrategy
    {
        T ReadShape(int recPos);
    }

   
}
