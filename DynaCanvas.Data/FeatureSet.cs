using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DynaCanvas.Data
{
    public class FeatureSet:KeyedCollection<int,Feature>
    {
        protected override int GetKeyForItem(Feature item)
        {
            return item.FeatureID;
        }
    }
}
