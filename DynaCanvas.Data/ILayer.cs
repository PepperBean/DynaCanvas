﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.DataStructures;
using GeoAPI.Geometries;

namespace DynaCanvas.Data
{
    public interface ILayer
    {
        // feature id start form 1
        string LayerName
        {
            get;
        }

        Envelope MBR
        {
            get;
        }



    }
}
