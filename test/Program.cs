﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCanvas.Data.Shapefile;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\workspace\Data\china-latest\roads.shp";
            Shapefile shpFile = new Shapefile(path);
            var f = shpFile.GetFeatureByID(101);
           
             Console.WriteLine("done!"+f.MBR.Area);
            Console.ReadLine();
        }
    }
}
