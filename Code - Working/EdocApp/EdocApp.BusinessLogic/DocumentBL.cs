using System;
using System.Collections.Generic;
using System.Data;
using EdocApp.DataModel;

namespace EdocApp.BusinessLogic
{
    /// <summary>
    /// This class is responsible for transporting data to and from the Web API
    /// and the Data Access Object (DAO).
    /// </summary>
    public class DocumentBL : BusinessBase
    {

        /// <summary>
        /// Constructor for class representing document business logic.
        /// </summary>
        public DocumentBL() : base()
        {
        }

        /// <summary>
        /// Retrieves all documents in the DB via the data access object
        /// </summary>
        /// <returns>A list of all documents sorted by date</returns>
        public List<Document> getAllDocuments()
        {
            DataTable dataTable = new DataTable();
            dataTable = businessDao.getAllDocuments();
            List<Document> docList = new List<Document>();
            Document doc = new Document();
            foreach (DataRow row in dataTable.Rows)
            {
                doc = getDocumentFromDataRow(row);
                docList.Add(doc);
            }
            docList.Sort();
            return docList;
        }


        /// <summary>
        /// Gets the form inputs from the user (on the UI) and retrieves
        /// a list of documents matching the form criteria.
        /// </summary>
        /// <param name="searchQuery">String array representing the form inputs in certain order</param>
        /// <returns></returns>
        public List<Document> getDocumentsFromSearch(string[] searchQuery) 
        {
            DataTable dataTable = new DataTable();
            dataTable = businessDao.getDocumentsFromSearch(searchQuery);
            List<Document> docList = new List<Document>();
            foreach (DataRow row in dataTable.Rows)
            {
                Document doc = getDocumentFromDataRow(row);
                docList.Add(doc);
            }
            docList.Sort();
            return docList;
        }

        /// <summary>
        /// This method retrieves documents information from a single
        /// row of a DataTable.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>A Document instance containing information from DataRow</returns>
        public Document getDocumentFromDataRow(DataRow row)
        {
            Document doc = new Document();
            doc.setDocumentId(Convert.ToInt32(row["Document_ID"].ToString()));
            doc.setDocumentName(row["Document_NAME"].ToString());
            doc.setCompany(row["Company_NAME"] != DBNull.Value ? new Company(Convert.ToInt32(row["Company_ID"].ToString()), row["Company_NAME"].ToString()) : new Company());
            doc.setCategory(row["Category_NAME"] != DBNull.Value ? new Category(Convert.ToInt32(row["Category_ID"]), row["Category_NAME"].ToString(), Convert.ToInt32(row["Parent_ID"] as int?), Convert.ToBoolean(row["Has_Children_BIT"]))
                : new Category());
            doc.setEmployee(row["Employee_First_NAME"] != DBNull.Value ? new Employee(row["Employee_ID"].ToString(), row["Employee_First_NAME"].ToString(), row["Employee_Last_NAME"].ToString())
                : new Employee());
            doc.setProject(row["Project_NAME"] != DBNull.Value ? new Project(Convert.ToInt32(row["Project_ID"]), row["Project_NAME"].ToString())
                : new Project());
            doc.setTags(getDocumentTags(doc.getDocumentId()));

           doc.setEmployeeSsn(Convert.ToInt32(getUserDetails(doc.getEmployee().getEmployeeId())));
           // doc.setEmployeeSsn(Convert.ToInt32(getUserDetails("U00000001")));
           // getUserDetails(doc.getEmployee().getEmployeeId());
            doc.setDocumentDate(Convert.ToDateTime(row["Document_DATE"].ToString()));
            doc.setUploadedDate(Convert.ToDateTime(row["Uploaded_DATE"].ToString()));
            doc.setUpdatedBy(new Employee(row["Updated_By_ID"].ToString(), row["Updated_First_NAME"].ToString(), row["Updated_Last_NAME"].ToString()));
            return doc;
        }

        /// <summary>
        /// Returns document tags for the passed document ID as a comma-separated list of words
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public string getDocumentTags(int documentId)
        {
            return businessDao.getDocumentTags(documentId);
        }
        public int getUserDetails(string userId)
        {
            return businessDao.getUserDetails(userId);
        }
        /// <summary>
        /// Takes company records from DataTable result and places them in a Company object list.
        /// </summary>
        /// <returns></returns>
        public List<Company> getAllCompanies()
        {
            DataTable dataTable = new DataTable();
            dataTable = businessDao.getAllCompanies();
            List<Company> companyList = new List<Company>();
            foreach (DataRow row in dataTable.Rows)
            {
                Company company = new Company(Convert.ToInt32(row["Company_ID"]), row["Company_NAME"].ToString());
                companyList.Add(company);
            }
            return companyList;
        }

        /// <summary>
        /// Takes category records from DataTable result and places them in a Category object list.
        /// </summary>
        /// <returns></returns>
        public List<Category> getAllCategories()
        {
            DataTable dataTable = new DataTable();
            dataTable = businessDao.getAllCategories();
            List<Category> categoryList = new List<Category>();
            foreach (DataRow row in dataTable.Rows)
            {
                Category category = new Category(Convert.ToInt32(row["Category_ID"]), row["Category_NAME"].ToString(), Convert.ToInt32(row["Parent_ID"] as int?), Convert.ToBoolean(row["Has_Children_BIT"]));
                categoryList.Add(category);
            }
            return categoryList;
        }

        /// <summary>
        /// Takes subcategory records from DataTable result and places them in a Category object list.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Category> getSubcategories(int categoryId)
        {
            DataTable dataTable = new DataTable();
            dataTable = businessDao.getSubcategories(categoryId);
            List<Category> subcategoryList = new List<Category>();
            foreach (DataRow row in dataTable.Rows)
            {
                Category category = new Category(Convert.ToInt32(row["Category_ID"]), row["Category_NAME"].ToString(), Convert.ToInt32(row["Parent_ID"]), Convert.ToBoolean(row["Has_Children_BIT"]));
                subcategoryList.Add(category);
            }
            return subcategoryList;
        }

        /// <summary>
        /// Gets the category that is the parent of the subcategory passed in.
        /// </summary>
        /// <param name="subcategoryId"></param>
        /// <returns></returns>
        public Category getCategoryFromSubcategory(int subcategoryId)
        {
            DataTable dataTable = new DataTable();
            dataTable = businessDao.getCategoryFromSubcategory(subcategoryId);
            Category category = new Category(Convert.ToInt32(dataTable.Rows[0]["Category_ID"]), dataTable.Rows[0]["Category_NAME"].ToString());
            return category;
        }

        /// <summary>
        /// Takes project records from the DataTable result and places them in a Project object list.
        /// </summary>
        /// <returns></returns>
        public List<Project> getAllProjects()
        {
            DataTable dataTable = new DataTable();
            dataTable = businessDao.getAllProjects();
            List<Project> projectList = new List<Project>();
            foreach (DataRow row in dataTable.Rows)
            {
                Project project = new Project(Convert.ToInt32(row["Project_ID"]), row["Project_NAME"].ToString());   
                projectList.Add(project);
            }
            return projectList;
        }

        /// <summary>
        /// Retrieves a list of all employees with admin flag turned on.
        /// </summary>
        /// <returns>List<Employee> object containing infomation about admin users.</returns>
        public List<Employee> getAdminUsers()
        {
            

            DataTable dataTable = new DataTable();
            dataTable = businessDao.getAdminUsers();
            List<Employee> adminList = new List<Employee>();
            foreach (DataRow row in dataTable.Rows)
            {
                Employee employee = new Employee(Convert.ToString(row["User_ID"]), Convert.ToString(row["First_NAME"]), Convert.ToString(row["Last_NAME"]));
                adminList.Add(employee);
            }
            return adminList;
        }

        /// <summary>
        /// Validates the user's employee input by checking if an employee
        /// with the passed in first and last names exists in the database.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>A boolean integer (0 or 1) -- 1 indicates valid input, 0 indicates input that is invalid</returns>
        public int validateUser(string firstName, string lastName)
        {
            return businessDao.validateUser(firstName, lastName);
        }

        /// <summary>
        /// Calls DAO to send insert command to DB for passed document. 
        /// </summary>
        /// <param name="doc">Document object to be inserted into the DB</param>
        /// <returns></returns>
        public bool insertDocumentEntry(Document doc)
        {
            return businessDao.insertDocument(doc);
        }

        /// <summary>
        /// Calls DAO to send update command to DB for passed document.
        /// </summary>
        /// <param name="doc">Document object to be updated in the DB</param>
        /// <returns></returns>
        public bool updateDocumentEntry(Document doc)
        {
            return businessDao.updateDocument(doc);
        }

        /// <summary>
        /// Calls DAO to send delete command to DB for passed document.
        /// </summary>
        /// <param name="documentId">Document object to be deleted from the DB</param>
        /// <returns></returns>
        public bool deleteDocumentEntry(int documentId)
        {
            return businessDao.deleteDocument(documentId);
        }
    }
}