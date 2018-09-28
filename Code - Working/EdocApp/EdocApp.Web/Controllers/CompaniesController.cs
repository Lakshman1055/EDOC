using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using EdocApp.BusinessLogic;
using EdocApp.DataModel;

namespace EdocApp.Web.Controllers
{
    /// <summary>
    /// Controller class for Company model
    /// </summary>
    public class CompaniesController : ApiController
    {
        DocumentBL docBL = new DocumentBL();

        // GET api/Companies/Get
        public IEnumerable<Company> Get()
        {
            List<Company> companyList = new List<Company>();
            companyList = docBL.getAllCompanies();
            return companyList;
        }
    }
}