using AutoMapper;
using SKC.Lib.Service.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Service.Managers
{
    public sealed class ManagerBase<T> :IDisposable where T : class
    {
        private RepositoryContext<T> _context;
        // private MapperConfiguration _mapperConfiguration;
        public ManagerBase(string connStr)
        {
            if (_context == null)
            {
                _context = new RepositoryContext<T>(connStr);
            }
        }

        public RepositoryContext<T> Context
        {
            get
            {
                return _context;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context = null;
            }
        }
    }
}
