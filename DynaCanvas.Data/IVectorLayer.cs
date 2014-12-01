using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Manipulator;

namespace DynaCanvas.Data
{
   public interface IVectorLayer:ILayer
    {
       FeatureSet FeatureSet
       {
           get;
       }
       bool ReadOnly
       {
           get;
       }
       IFeatureProvider Provider
       {
           get;
       }

    }
}
