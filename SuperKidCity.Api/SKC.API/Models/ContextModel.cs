using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SKC.Lib.Service;
using SKC.Lib.Service.Repos;
using System.Configuration;
using SKC.Lib.Service.Managers;
namespace SKC.API.Models
{
    public class ContextModel<T> where T : class
    {
        private ManagerBase<T> _manager;
        private string ConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public ContextModel()
        {
            if (_manager == null)
            {
                _manager = new ManagerBase<T>(ConnString);
            }
        }

        public ManagerBase<T> Manager
        {
            get
            {
                return _manager;
            }
        }
    }
}