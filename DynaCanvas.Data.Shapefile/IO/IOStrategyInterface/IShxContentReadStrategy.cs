using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile;
using System.Diagnostics.Contracts;
using System.Diagnostics;
namespace DynaCanvas.Data.Shapefile.IO
{
    [ContractClass(typeof(_IShxContentReadStrategy))]
    public interface IShxContentReadStrategy
    {
        ShxRecord[] ReadRecordes(int startIndex, int count);
        ShxRecord ReadRecorde(int index);

    }

    [ContractClassFor(typeof(IShxContentReadStrategy))]
    class _IShxContentReadStrategy : IShxContentReadStrategy
    {
        public ShxRecord[] ReadRecordes(int startIndex, int count)
        {
            Contract.Requires(startIndex >= 0);
            Contract.Requires(count > 0);
            Contract.Ensures(Contract.Result<ShxRecord[]>().Length == count);

            return default(ShxRecord[]);
        }

        public ShxRecord ReadRecorde(int index)
        {
            Contract.Requires(index >= 0);
            return default(ShxRecord);
        }
    }
}
