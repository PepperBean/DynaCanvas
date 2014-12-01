using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaCanvas.Data.Shapefile
{
    public class DataSource : IDataSource
    {
        private readonly string _FilePath;
        public DataSource(string filePath)
        {
            Contract.Requires(!string.IsNullOrEmpty(filePath));

            _FilePath = filePath;
            this.Open();
        }

        public void Open()
        {
           


        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public string ConnectionInfo
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsOpen
        {
            get { throw new NotImplementedException(); }
        }
        bool _IsDisposed = false;
        public bool IsDisposed
        {
            get { return _IsDisposed; }
            private set { _IsDisposed = value; }
        }

        public DataSourceSharedMode SheardMode
        {
            get { return DataSourceSharedMode.StandAlong; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
