using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdocApp.DataAccess;

namespace EdocApp.BusinessLogic
{
    public class BusinessBase
    {
        protected EdocDAO businessDao;

        public BusinessBase()
        {
           businessDao = new EdocDAO();
        }
    }
}
