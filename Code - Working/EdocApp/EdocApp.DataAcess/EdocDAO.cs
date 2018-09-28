using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EdocApp.DataModel;

namespace EdocApp.DataAccess
{
    /// <summary>
    /// Builds parameters and stored proc names to pass to 
    /// EdocDBConnection
    /// </summary>
    public class EdocDAO
    {
        private EdocDBConnection conn;

        public EdocDAO()
        {
            conn = new EdocDBConnection();
        }

        /// <summary>
        /// Calls stored procedure to get information about all documents in database
        /// in the form of a DataTable.
        /// </summary>
        /// <returns>DataTable object containing all document records</returns>
        public DataTable getAllDocuments()
        {
            string query = "EDOC_RETRIEVE_S1";
            DataTable dataTable = new DataTable();
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }

        /// <summary>
        /// Calls the stored procedure to get records of documents matching the search parameters.
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns>DataTable object containing all document records that match the search query.</returns>
        public DataTable getDocumentsFromSearch(string[] searchQuery)
        {
            
            string query = "EDOC_RETRIEVE_S2";
            SqlParameter[] sqlParameters = new SqlParameter[10];
            sqlParameters[0] = new SqlParameter("@As_Document_NAME", Convert.ToString(searchQuery[0]));
            sqlParameters[1] = new SqlParameter("@Ai_Company_ID", Convert.ToInt32(searchQuery[1]));
            sqlParameters[2] = new SqlParameter("@Ai_Category_ID", Convert.ToInt32(searchQuery[2]));
            sqlParameters[3] = new SqlParameter("@Ai_Project_ID", Convert.ToInt32(searchQuery[3]));
            sqlParameters[4] = new SqlParameter("@As_Employee_Last_NAME", Convert.ToString(searchQuery[4]));
            sqlParameters[5] = new SqlParameter("@As_Employee_First_NAME", Convert.ToString(searchQuery[5]));
            sqlParameters[6] = new SqlParameter("@As_Tag_TEXT", Convert.ToString(searchQuery[6]));

            // Declares the parameters for start and end date as type DateTime2 (different from C# DateTime)
            sqlParameters[7] = new SqlParameter("@Ad_Start_DATE", SqlDbType.DateTime);
            sqlParameters[7].Value =  searchQuery[7];
            sqlParameters[8] = new SqlParameter("@Ad_End_DATE", SqlDbType.DateTime);
            sqlParameters[8].Value = searchQuery[8];

            sqlParameters[9] = new SqlParameter("@Ai_Employee_Ssn_NUMB", Convert.ToInt32(searchQuery[9]));

            return conn.executeSelectQuery(query, sqlParameters);
        }

        /// <summary>
        /// Calls the stored procedure to get the list of tags associated with the document as a comma-separated string.
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns>String with comma delimited tags pertaining to the document ID.</returns>
        public string getDocumentTags(int documentId)
        {
            string query = "EDOC_RETRIEVE_S3";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Ai_Document_ID", Convert.ToInt32(documentId));
            sqlParameters[1] = new SqlParameter("@As_Tag_TEXT", SqlDbType.VarChar, 2000);
            sqlParameters[1].Direction = ParameterDirection.Output;
            using (SqlConnection tempConn =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["EdocDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, tempConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlParameters);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                string result = (string)sqlParameters[1].Value;
                cmd.Connection.Close();
                return result;
            }
        }
        public int getUserDetails(string userId)
        {
            string query = "EDOC_RETRIEVE_S10";
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Ac_User_ID",userId);

            sqlParameters[1] = new SqlParameter("@As_First_NAME", SqlDbType.VarChar,30);
            sqlParameters[1].Direction = ParameterDirection.Output;
            sqlParameters[2] = new SqlParameter("@As_Last_NAME", SqlDbType.VarChar,30);
            sqlParameters[2].Direction = ParameterDirection.Output;
            sqlParameters[3] = new SqlParameter("@Ai_Ssn_NUMB", SqlDbType.Int, 9);
            sqlParameters[3].Direction = ParameterDirection.Output;
            using (SqlConnection tempConn =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["EdocDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, tempConn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlParameters);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                 int result;
                
              
                 result=(int)sqlParameters[3].Value;
                
             
                cmd.Connection.Close();
                return result;
            }
          //  return conn.executeSelectQuery(query, sqlParameters);
        }


        /// <summary>
        /// Calls the stored procedure to get records of all companies in the database.
        /// </summary>
        /// <returns>DataTable object containing all company records.</returns>
        public DataTable getAllCompanies()
        {
            string query = "EDOC_RETRIEVE_S4";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }

        /// <summary>
        /// Calls the stored procedure to get records of all parent categories in the database.
        /// </summary>
        /// <returns>DataTable object containing all category records.</returns>
        public DataTable getAllCategories()
        {
            string query = "EDOC_RETRIEVE_S5";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }

        /// <summary>
        /// Calls the stored procedure to get records of all subcategories with the specified parent category.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>DataTable object containing all subcategory records whose parent ID is the category ID.</returns>
        public DataTable getSubcategories(int categoryId)
        {
            string query = "EDOC_RETRIEVE_S6";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Ai_Category_ID", Convert.ToInt32(categoryId));
            return conn.executeSelectQuery(query, sqlParameters);
        }

        /// <summary>
        /// Calls the stored procedure to get records of all projects in the database.
        /// </summary>
        /// <returns>DataTable object containing all project records.</returns>
        public DataTable getAllProjects()
        {
            string query = "EDOC_RETRIEVE_S7";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }

        /// <summary>
        /// Calls the stored procedure to get the record of the parent category of the specified subcategory.
        /// </summary>
        /// <param name="subcategoryId"></param>
        /// <returns>DataTable object containing category record that is the parent of the subcategory.</returns>
        public DataTable getCategoryFromSubcategory(int subcategoryId)
        {
            string query = "EDOC_RETRIEVE_S8";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Ai_Category_ID", Convert.ToInt32(subcategoryId));
            return conn.executeSelectQuery(query, sqlParameters);
        }

        /// <summary>
        /// Calls the stored procedure to get the records of all users with admin flag turned on.
        /// </summary>
        /// <returns>DataTable object containing all admin user records.</returns>
        public DataTable getAdminUsers()
        {
            string query = "EDOC_RETRIEVE_S9";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
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
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings
                ["EdocDBConnectionString"].ConnectionString))
            {
                int result;
                SqlDataAdapter adapter = new SqlDataAdapter();
                if (conn.State == ConnectionState.Closed
                    || conn.State == ConnectionState.Broken)
                {
                    conn.Open();
                }
                SqlCommand command = new SqlCommand("SELECT dbo.ValidateUser(@As_Employee_First_NAME, @As_Employee_Last_NAME)", conn);
                command.Parameters.AddWithValue("@As_Employee_First_NAME", firstName);
                command.Parameters.AddWithValue("@As_Employee_Last_NAME", lastName);
                result = Convert.ToInt32(command.ExecuteScalar());
                return result;
            }
        }

        /// <summary>
        /// Calls the stored procedure to insert the specified document's record into the database.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>Boolean value indicating if the document insertion was successful or not.</returns>
        public bool insertDocument(Document doc)
        {
            string query = "EDOC_INSERT_S2";
            SqlParameter[] sqlParameters = new SqlParameter[14];
            sqlParameters[0] = new SqlParameter("@As_Document_NAME", doc.getDocumentName());
            sqlParameters[1] = new SqlParameter("@Ai_Company_ID", doc.getCompany().getCompanyId());
            sqlParameters[2] = new SqlParameter("@Ai_Category_ID", doc.getCategory().getCategoryId());
            sqlParameters[3] = new SqlParameter("@Ai_Project_ID", doc.getProject().getProjectId());
            sqlParameters[4] = new SqlParameter("@As_Employee_Last_NAME", doc.getEmployee().getEmployeeLastName());
            sqlParameters[5] = new SqlParameter("@As_Employee_First_NAME", doc.getEmployee().getEmployeeFirstName());
            sqlParameters[6] = new SqlParameter("@As_Tag_TEXT", doc.getTags());

            // Declares the parameters for document, uploaded and uploaded date as type DateTime2 (different from C# DateTime)
            sqlParameters[7] = new SqlParameter("@Ad_Document_DATE", SqlDbType.DateTime2);
            sqlParameters[7].Value = doc.getDocumentDate();
            sqlParameters[8] = new SqlParameter("@Ad_Uploaded_DATE", SqlDbType.DateTime2);
            sqlParameters[8].Value = doc.getUploadedDate();
            sqlParameters[9] = new SqlParameter("@Ad_Updated_DATE", SqlDbType.DateTime2);
            sqlParameters[9].Value = doc.getUpdatedDate();

            sqlParameters[10] = new SqlParameter("@As_Updated_Last_NAME", doc.getUpdatedBy().getEmployeeLastName());
            sqlParameters[11] = new SqlParameter("@As_Updated_First_NAME", doc.getUpdatedBy().getEmployeeFirstName());
            sqlParameters[12] = new SqlParameter("@Ac_User_ID", doc.getEmployee().getEmployeeId());
           
            sqlParameters[13] = new SqlParameter("@Ai_Employee_Ssn_NUMB", Convert.ToInt32(doc.getEmployeeSsn()));
            return conn.executeInsertQuery(query, sqlParameters);
        }

        /// <summary>
        /// Calls the stored procedure to update the specified document's record in the database.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>Boolean value indicating if the document updating was successful or not.</returns>
        public bool updateDocument(Document doc)
        {
            string query = "EDOC_UPDATE_S1";
            SqlParameter[] sqlParameters = new SqlParameter[15];
            sqlParameters[0] = new SqlParameter("@Ai_Document_ID", doc.getDocumentId());
            sqlParameters[1] = new SqlParameter("@As_Document_NAME", doc.getDocumentName());
            sqlParameters[2] = new SqlParameter("@Ai_Company_ID", doc.getCompany().getCompanyId());
            sqlParameters[3] = new SqlParameter("@Ai_Category_ID", doc.getCategory().getCategoryId());
            sqlParameters[4] = new SqlParameter("@Ai_Project_ID", doc.getProject().getProjectId());
            sqlParameters[5] = new SqlParameter("@As_Employee_Last_NAME", doc.getEmployee().getEmployeeLastName());
            sqlParameters[6] = new SqlParameter("@As_Employee_First_NAME", doc.getEmployee().getEmployeeFirstName());
            sqlParameters[7] = new SqlParameter("@As_Tag_TEXT", doc.getTags());

            // Declares the parameters for document, uploaded and uploaded date as type DateTime2 (different from C# DateTime)
            sqlParameters[8] = new SqlParameter("@Ad_Document_DATE", SqlDbType.DateTime2);
            sqlParameters[8].Value = doc.getDocumentDate();
            sqlParameters[9] = new SqlParameter("@Ad_Uploaded_DATE", SqlDbType.DateTime2);
            sqlParameters[9].Value = doc.getUploadedDate();
            sqlParameters[10] = new SqlParameter("@Ad_Updated_DATE", SqlDbType.DateTime2);
            sqlParameters[10].Value = doc.getUpdatedDate();

            sqlParameters[11] = new SqlParameter("@As_Updated_First_NAME", doc.getUpdatedBy().getEmployeeFirstName());
            sqlParameters[12] = new SqlParameter("@As_Updated_Last_NAME", doc.getUpdatedBy().getEmployeeLastName());
            sqlParameters[13] = new SqlParameter("@Ac_User_ID", doc.getEmployee().getEmployeeId());

            sqlParameters[14] = new SqlParameter("@Ai_Employee_Ssn_NUMB", Convert.ToInt32(doc.getEmployeeSsn()));
            return conn.executeUpdateQuery(query, sqlParameters);
        }

        /// <summary>
        /// Calls the stored procedure to delete the specified document's record from the database.
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns>Boolean value indicating if the document deletion was successful or not.</returns>
        public bool deleteDocument(int documentId)
        {
            string query = "EDOC_DELETE_S1";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Ai_Document_ID", documentId);
            return conn.executeDeleteQuery(query, sqlParameters);
        }

    }
}
