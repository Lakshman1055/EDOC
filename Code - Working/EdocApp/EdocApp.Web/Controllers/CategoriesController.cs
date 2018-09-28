using System.Collections.Generic;
using System.Web.Http;
using EdocApp.BusinessLogic;
using EdocApp.DataModel;

namespace EdocApp.Web.Controllers
{

    /// <summary>
    /// Controller class for Category model
    /// </summary>
    public class CategoriesController : ApiController
    {
        DocumentBL docBL = new DocumentBL();

        // GET api/Categories/Get
        public IEnumerable<Category> Get()
        {
            List<Category> categoryList = new List<Category>();
            categoryList = docBL.getAllCategories();
            return categoryList;
        }

        // GET api/Categories/GetSubcategories/id
        public IEnumerable<Category> GetSubcategories(int id)
        {
            List<Category> categoryList = new List<Category>();
            categoryList = docBL.getSubcategories(id);
            return categoryList;
        }

        // GET api/Categories/GetCategoryFromSubcategory/id
        public Category GetCategoryFromSubcategory(int id)
        {
            Category category = new Category();
            category = docBL.getCategoryFromSubcategory(id);
            return category;
        }
    }
}