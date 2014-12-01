using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data
{
    public interface IDataSource:IDisposable
    {
        void Open();
        void Close();
        void Flush();
        string ConnectionInfo
        {
            get;
        }
      
        bool IsOpen
        {
            get;
        }
        bool IsDisposed
        {
            get;
        }
      
        DataSourceSharedMode SheardMode
        {
            get;
        }
    }
}
