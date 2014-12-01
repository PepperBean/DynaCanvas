using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data
{
    public class DataSet : KeyedCollection<string, ILayer>
    {
        protected override string GetKeyForItem(ILayer item)
        {
            return item.LayerName;
        }
    }
}
