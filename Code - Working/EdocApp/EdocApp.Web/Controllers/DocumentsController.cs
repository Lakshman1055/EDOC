using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EdocApp.BusinessLogic;
using EdocApp.DataModel;

namespace EdocApp.Web.Controllers
{

    /// <summary>
    /// Controller class for Document model
    /// </summary>
    /// 
    public class DocumentsController : ApiController
    {

        DocumentBL docBL = new DocumentBL();


        // GET api/Documents/Get
        public IEnumerable<Document> Get()
        {
            List<Document> docList = docBL.getAllDocuments();
            return docList;
        }

        // GET api/Documents/GetDocumentsFromSearch/?searchQuery...
        public IEnumerable<Document> GetDocumentsFromSearch([FromUri] string[] searchQuery)
        {
            List<Document> docList = docBL.getDocumentsFromSearch(searchQuery);
            return docList;
        }
        [OverrideActionFilters]
        [HttpPost]
        // POST api/Documents/Post (INSERT)
        public HttpResponseMessage Post([FromBody] Document doc)
        {
            if (doc == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read the Document from body");
            }
            bool success = docBL.insertDocumentEntry(doc);
            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save Document model to database");
        }

        // PUT api/Documents/Put (UPDATE)
        public HttpResponseMessage Put([FromBody] Document doc)
        {
            
            if (doc == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read the Document from body");
            }
            bool success = docBL.updateDocumentEntry(doc);
            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save Document model to the database");
        }

        // DELETE api/Documents/Delete/id
        public HttpResponseMessage Delete(int id)
        {
            bool success = docBL.deleteDocumentEntry(id);
            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not delete Document model from the database");
        }

    }
}