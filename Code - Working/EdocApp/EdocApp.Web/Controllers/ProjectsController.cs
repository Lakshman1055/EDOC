using System.Collections.Generic;
using System.Web.Http;
using EdocApp.BusinessLogic;
using EdocApp.DataModel;

namespace EdocApp.Web.Controllers
{
    public class ProjectsController : ApiController
    {
        DocumentBL docBL = new DocumentBL();

        // GET api/Projects/Get
        public IEnumerable<Project> Get()
        {
            List<Project> projectList = new List<Project>();
            projectList = docBL.getAllProjects();
            return projectList;
        }
    }
}