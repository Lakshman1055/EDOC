using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EdocApp.BusinessLogic;
using EdocApp.DataModel;

namespace EdocApp.Web.Controllers
{
    public class EmployeesController : ApiController
    {
        DocumentBL docBL = new DocumentBL();

        // GET api/Employees/GetAdmin
        public IEnumerable<Employee> GetAdmin()
        {
            List<Employee> adminList = docBL.getAdminUsers();
            return adminList;
        }

        // GET api/Employees/GetValidateUser
        public int GetValidateUser(string firstName, string lastName)
        {
            return docBL.validateUser(firstName, lastName);
        }
    }
}