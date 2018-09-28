using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using EdocApp.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading;

namespace EdocUI
{
    /// <summary>
    /// This partition of EdocDesktopApplication handles all Web API calls made by the application.
    /// </summary>
    public partial class EdocDesktopApplication
    {
       
        /// <summary>
        /// Web API call (GET) to get all companies in the database and fill the passed ComboBox with them.
        /// </summary>
        /// <param name="cb"></param>
        private async void getAllCompanies(ComboBox cb)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(Constants.API_ROOT_URI + "Companies/Get"))
                    {
                        List<Company> companyList = new List<Company>();
                        if (response.IsSuccessStatusCode)
                        {
                           
                            var companyJsonString = await response.Content.ReadAsStringAsync();
                            cb.Items.Clear();
                            if (cb.Name == "companySelect")
                            {
                                cb.Items.Add(Constants.FILTER_DEFAULT_TEXT);
                            }
                            else
                            {
                                cb.Items.Add(Constants.DEFAULT_COMBO_TEXT);
                            }
                            companyList = JsonConvert.DeserializeObject<List<Company>>(companyJsonString).ToList();
                            cb.Items.AddRange(companyList.ToArray());
                            cb.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {

                logger.Logger("getAllCompanies", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Web API call (GET) to get all categories in the database and fill the passed ComboBox with them.
        /// </summary>
        /// <param name="cb"></param>
        private async void getAllCategories(ComboBox cb)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(Constants.API_ROOT_URI + "Categories/Get"))
                    {
                        List<Category> categoryList = new List<Category>();
                        if (response.IsSuccessStatusCode)
                        {
                            var categoryJsonString = response.Content.ReadAsStringAsync().Result;
                            cb.Items.Clear();
                            if (cb.Name == "categorySelect")
                            {
                                cb.Items.Add(Constants.FILTER_DEFAULT_TEXT);
                            }
                            else
                            {
                                cb.Items.Add(Constants.DEFAULT_COMBO_TEXT);
                            }
                          
                            categoryList = JsonConvert.DeserializeObject<List<Category>>(categoryJsonString).ToList();
                            cb.Items.AddRange(categoryList.ToArray());
                            cb.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {

                logger.Logger("getAllCategories", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Web API call (GET) to get subcategories of the selected category.
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="categoryName"></param>
        private async void getSubcategories(ComboBox cb, int categoryId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (
                        var response =
                            client.GetAsync(Constants.API_ROOT_URI + "Categories/GetSubcategories/" + categoryId).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var subcategoryJsonString = response.Content.ReadAsStringAsync().Result;
                            cb.Items.Clear();
                            if (cb.Name == "subcategorySelect")
                            {
                                cb.Items.Add(Constants.FILTER_DEFAULT_TEXT);
                            }
                            else
                            {
                                cb.Items.Add(Constants.DEFAULT_COMBO_TEXT);
                            }
                           
                            cb.Items.AddRange(JsonConvert.DeserializeObject<List<Category>>(subcategoryJsonString).ToArray());
                            cb.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {

                logger.Logger("getSubcategories", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Web API call (GET) to get parent category of the current document's subcategory.
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="subcategoryName"></param>
        private async void getCategoryFromSubcategory(ComboBox cb, int subcategoryId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (
                        var response =
                                client.GetAsync(Constants.API_ROOT_URI + "Categories/GetCategoryFromSubcategory/" + subcategoryId).Result
                        )
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var categoryJsonString = await response.Content.ReadAsStringAsync();
                            categoryname = new Category();
                          
                            categoryname = JsonConvert.DeserializeObject<Category>(categoryJsonString);
                            cb.SelectedItem = categoryname;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {

                logger.Logger("getCategoryFromSubcategory", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Web API call (GET) to get all projects in the database and fill the passed ComboBox with them.
        /// </summary>
        /// <param name="cb"></param>
        private async void getAllProjects(ComboBox cb)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(Constants.API_ROOT_URI + "Projects/Get"))
                    {
                        List<Project> projectList = new List<Project>();
                        if (response.IsSuccessStatusCode)
                        {
                            var projectJsonString = await response.Content.ReadAsStringAsync();
                            cb.Items.Clear();
                            if (cb.Name == "projectSelect")
                            {
                                cb.Items.Add(Constants.FILTER_DEFAULT_TEXT);
                            }
                            else
                            {
                                cb.Items.Add(Constants.DEFAULT_COMBO_TEXT);
                            }
                            
                            projectList = JsonConvert.DeserializeObject<List<Project>>(projectJsonString).ToList();
                            cb.Items.AddRange(projectList.ToArray());
                            cb.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {

                logger.Logger("getAllProjects", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Web API call (GET) to get all documents pertaining to user search and fill the document list with them.
        /// Passes the search parameters in the API link as a query string.
        /// </summary>
        /// <param name="searchParams"></param>
        private void getDocumentsFromSearch(string[] searchParams)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string uriQuery = "?";
                    for (int i = 0; i < searchParams.Length - 1; i++)
                    {
                        uriQuery += "searchQuery=" + searchParams[i] + "&";
                    }
                    uriQuery += "searchQuery=" + searchParams[9];
                    using (var response =
                        client.GetAsync(Constants.API_ROOT_URI + "Documents/GetDocumentsFromSearch/" + uriQuery).Result)
                    {
                        var documentJsonString = response.Content.ReadAsStringAsync().Result;
                        documentList = new List<Document>();
                        documentList = JsonConvert.DeserializeObject<List<Document>>(documentJsonString).ToList();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                logger.Logger("getDocumentsFromSearch", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Web API call (GET) that validates the employee passed in as one present in the database.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        private int validateUser(string firstName, string lastName)
        {
            using (var client = new HttpClient())
            {
                string uriQuery = "?";
                uriQuery += "firstName=" + firstName + "&";
                uriQuery += "lastName=" + lastName;
                using (var response =
                        client.GetAsync(Constants.API_ROOT_URI + "Employees/GetValidateUser/" + uriQuery).Result)
                {
                    var validateResult = response.Content.ReadAsStringAsync().Result;
                    int result = JsonConvert.DeserializeObject<int>(validateResult);
                    return result;
                }
            }
        }

        /// <summary>
        /// Web API call (POST) to insert a new document record into the database.
        /// </summary>
        /// <param name="doc"></param>
        private async void insertDocument(Document doc, string temp)
        {
           
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response =
                            await client.PostAsJsonAsync<Document>(Constants.API_ROOT_URI + "Documents/Post", doc);
                        response.EnsureSuccessStatusCode();
                    }
                    catch (HttpRequestException ex)
                    {
                        logger.Logger("insertDocument", "The document data was not saved successfully.", ex.StackTrace);

                    }
                    catch (System.Exception ex)
                    {
                        logger.Logger("insertDocument",ex.Message, ex.StackTrace);

                    }
                  
                  
                }
          
        }

        /// <summary>
        /// Web API call (PUT) to copy a documents record into the archive table and update its current record
        /// in the main documents table.
        /// </summary>
        /// <param name="doc"></param>
        private async void updateDocument(Document doc, string temp)
        {
          
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = await client.PutAsJsonAsync(Constants.API_ROOT_URI + "Documents/Put", doc);
                        response.EnsureSuccessStatusCode();
                        searchButton.PerformClick();
                    }
                    catch (HttpRequestException ex)
                    {
                        logger.Logger("updateDocument", "The document data was not saved successfully.", ex.StackTrace);
                       
                    }
                }
           
        }

        /// <summary>
        /// Web API call (DELETE) to copy a documents record into the archive table and delete the
        /// entry from the main documents table.
        /// </summary>
        /// <param name="documentId"></param>
        private async void deleteDocument(int documentId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.DeleteAsync(Constants.API_ROOT_URI + "Documents/Delete/" + documentId);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException ex)
                {
                    logger.Logger("deleteDocument", "The document data was not deleted successfully.", ex.StackTrace);
                   
                }
            }
        }
      
    }
}
