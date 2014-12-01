using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Contracts;

namespace DynaCanvas.Data.Shapefile
{
    public class Util
    {
        public const string SHP_SUFFIX = "shp";
        public const string SHX_SUFFIX = "shx";
        public const string DBF_SUFFIX = "dbf";
        public static string _ErrorMessage;
        public static bool FileSetValidate(
            string filePath)
        {
            Contract.Requires(!string.IsNullOrEmpty(filePath));

            bool result = true;
            _ErrorMessage = string.Empty;
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            result &= File.Exists(fileNameWithoutExtension + "." + SHP_SUFFIX);
            if (!result) _ErrorMessage = "can't found the ." + SHP_SUFFIX + " file" + Environment.NewLine;

            result &= File.Exists(fileNameWithoutExtension + "." + SHX_SUFFIX);
            if (!result) _ErrorMessage += "can't found the ." + SHX_SUFFIX + " file" + Environment.NewLine;
            
            result &= File.Exists(fileNameWithoutExtension + "." + DBF_SUFFIX);
            if (!result) _ErrorMessage += "can't found the ." + DBF_SUFFIX + " file" + Environment.NewLine;
            
            return result;
        }

        ///<summary>
        ///Swaps the byte order of an int32
        ///</summary>
        /// <param name="i">Integer to swap</param>
        /// <returns>Byte Order swapped int32</returns>
        public static int SwapByteOrder(int i)
        {
            byte[] buffer = BitConverter.GetBytes(i);
            Array.Reverse(buffer, 0, buffer.Length);
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}
