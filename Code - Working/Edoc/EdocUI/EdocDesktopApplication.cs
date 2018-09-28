using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using AxAcroPDFLib;
using EdocApp.DataModel;
using EdocUI.Properties;
using System.Configuration;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace EdocUI
{
   
    public partial class EdocDesktopApplication : Form
    {
        private List<Document> documentList; // List of document results
        private Document currentDocument; // Currently selected document
        public Employee currentEmployee { get; set; }  // Current admin employee logged in
        private DataGridView unclassifiedDGV;
        private DataGridView classifiedDGV;
        private int mode; // 0 for unclassified mode, 1 for classified mode
        private int docIndex; // Keeps track of the previously selected document
    
     
        private DataGridView dgvC, dgvUC;
        private MessageLogger logger;
        private TextAndImageColumn lastcolumn1, lastcolumn2, lastcolumn3;
        private Category categoryname;
        private string employeeid;
      
        public static ToolStripStatusLabel StatusControl;
     
        public EdocDesktopApplication(Employee employee)
        {
         
         
            InitializeComponent();

           // this.Font = new System.Drawing.Font()

      
            
            logger = new MessageLogger();
            logger.edocobj = "EdocDesktopApplication";
            StatusControl = new ToolStripStatusLabel();
            StatusControl = statusbar_msg;
            currentEmployee = employee;
            this.lblusername.Text += currentEmployee.getEmployeeFirstName() + " " + currentEmployee.getEmployeeLastName();
            employeeFirstNameModifyInput.Text = currentEmployee.getEmployeeFirstName();
            employeeLastNameModifyInput.Text = currentEmployee.getEmployeeLastName();
            employeeid=currentEmployee.getEmployeeId();
            getAllCompanies(companySelect);
            getAllCategories(categorySelect);
            getAllProjects(projectSelect);

            documentList = new List<Document>();
            currentDocument = new Document();
            startDateInput.Text =Convert.ToString(DateTime.Now.Date.AddDays(-30));
            endDateInput.Text =Convert.ToString(DateTime.Now);
            employeeFirstNameModifyInput.Text = currentEmployee.getEmployeeFirstName();
            employeeLastNameModifyInput.Text = currentEmployee.getEmployeeLastName();
            modifyUnclassifiedTabClicked();
          
           
        }

        private void TabLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int indexValue = (sender as TabControl).SelectedIndex;
                TabPage item = (sender as TabControl).SelectedTab;
                if (indexValue == 0)
                {
                    modifyUnclassifiedTabClicked();
                }
                else
                {
                    modifyclassifiedTabClicked();
                    
                  
                }
            }
            catch (Exception ex)
            {
                logger.Logger("TabLayout_SelectedIndexChanged", ex.Message, ex.StackTrace);
                return;
            }

        }


        #region Control event handlers


        /// <summary>
        /// Displays all the unclassified documents in the table when 
        /// the "Modify Unclassified" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void modifyUnclassifiedTabClicked()
        {
            try
            {
                refresh();
                mode = 0;
                string[] fileList = Directory.GetFiles(Constants.DEFAULT_PATH, "*.pdf");
                documentList = new List<Document>();
                foreach (string s in fileList)
                {
                    Document doc = new Document(Path.GetFileNameWithoutExtension(s));
                    FileInfo fi = new FileInfo(s);
                    doc.setUploadedDate(fi.CreationTime);
                    documentList.Add(doc);
                }
                displayUnclassifiedTable();
               
            }
            catch (Exception ex)
            {
                logger.Logger("modifyUnclassifiedTabClicked", ex.Message, ex.StackTrace);
            }
        }
        private void modifyclassifiedTabClicked()
        {
            try
            {
                 refresh();
                mode = 1;

                string[] fileList = Directory.GetFiles(Constants.CLASSIFIED_PATH, "*.pdf");
                documentList = new List<Document>();
                foreach (string s in fileList)
                {
                    Document doc = new Document(Path.GetFileNameWithoutExtension(s));
                    FileInfo fi = new FileInfo(s);
                    doc.setUploadedDate(fi.CreationTime);
                    documentList.Add(doc);
                }
                displayClassifiedTable();


                //searchButton.PerformClick();

            }
            catch (Exception ex)
            {
                logger.Logger("modifyclassifiedTabClicked", ex.Message, ex.StackTrace);
                return;
            }
        }

        /// <summary>
        /// Displays all documents in the table that match the search parameters
        /// entered into the search box form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                refresh();
                mode = 1;
                string[] searchParams = new string[10];

                // Adds the search parameters to a list, making them null if no value is entered.
                searchParams[0] = documentNameInput.Text;

                /* 
                 * For selection of company, category (or subcategory, if selected) and project, 
                 * sets to the ID of the object if selected. Else, sets to "0".
                 */
                searchParams[1] = (Convert.ToString(companySelect.Text) != Constants.FILTER_DEFAULT_TEXT)
                    ? Convert.ToString(((Company)companySelect.SelectedItem).getCompanyId()) : "0";

                #region Set category search parameter
                if (Convert.ToString(categorySelect.Text) == Constants.FILTER_DEFAULT_TEXT)
                {
                    searchParams[2] = "0";
                }
                else if (Convert.ToString(subcategorySelect.Text) != Constants.FILTER_DEFAULT_TEXT && Convert.ToString(subcategorySelect.Text) != "")
                {
                    searchParams[2] = Convert.ToString(((Category)subcategorySelect.SelectedItem).getCategoryId());
                }
                else
                {
                    searchParams[2] = Convert.ToString(((Category)categorySelect.SelectedItem).getCategoryId());
                }
                #endregion

              

                searchParams[3] = (Convert.ToString(projectSelect.Text) != Constants.FILTER_DEFAULT_TEXT)
                    ? Convert.ToString(((Project)projectSelect.SelectedItem).getProjectId()) : "0";

                searchParams[4] = !string.IsNullOrWhiteSpace(employeeLastNameInput.Text)
                   ? employeeLastNameInput.Text : "";
                searchParams[5] = !string.IsNullOrWhiteSpace(employeeFirstNameInput.Text)
                    ? employeeFirstNameInput.Text : "";
                searchParams[6] = !string.IsNullOrWhiteSpace(tagsInput.Text)
                    ? tagsInput.Text : "";
                searchParams[7] = (startDateInput.Value == DateTime.Now && endDateInput.Value == DateTime.Now)
                    ? "" : startDateInput.Value.ToShortDateString();
                //searchParams[8] = (startDateInput.Value == DateTime.Now && endDateInput.Value == DateTime.Now)
                //    ? "" : endDateInput.Value.AddDays(1).ToShortDateString();
                searchParams[8] = (startDateInput.Value == DateTime.Now && endDateInput.Value == DateTime.Now)
                    ? "" : endDateInput.Value.ToShortDateString();
                searchParams[9] = !string.IsNullOrWhiteSpace(employeeSsnInput.Text)
                    ? employeeSsnInput.Text : "0";

                //Passes list of search parameters to HTTPGET request, puts the results in a Document list and arranges the list in a DataGridView.
                getDocumentsFromSearch(searchParams);
                displayClassifiedTable();
            }
            catch (Exception ex)
            {
                logger.Logger("searchButton_Click", ex.Message, ex.StackTrace);
                return;
            }
        }
       
      
        /// <summary>
        /// Goes back from the modify panel and displays the most recent table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (mode == 0)
                {
                   
                    modifyUnclassifiedTabClicked();

                }
                else
                {
                   

                    modifyclassifiedTabClicked();


                }
            }
            catch (Exception ex)
            {
                logger.Logger("backButton_Click", ex.Message, ex.StackTrace);
                return;
            }
        }


        /// <summary>
        /// Archives the document's most recent information and removes it from the main documents table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            deleteButton_Click();
        }

        /// <summary>
        /// Determines which subcategories (if any) to display when a category is selected from the search panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void categorySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                subcategorySelect.Items.Clear();
                if (Convert.ToString(((ComboBox)sender).Text) != Constants.FILTER_DEFAULT_TEXT && ((Category)((ComboBox)sender).SelectedItem).getHasChildren()) // IMPORTANT: Same idea for implementing modifyPanel pre-filled fields!
                {
                    int temp = ((Category)((ComboBox)sender).SelectedItem).getCategoryId();
                    getSubcategories(subcategorySelect, temp);
                    subcategorySelect.Enabled = true;
                }
                else
                {
                    subcategorySelect.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                logger.Logger("categorySelect_SelectedIndexChanged", ex.Message, ex.StackTrace);
                return;
            }
        }


        #endregion

        #region Table display handlers and miscellaneous methods

        /// <summary>
        /// Hides both the modify panel and the embedded PDF view
        /// Clears the document list and the table panel.
        /// </summary>
        private void refresh()
        {

            try
            {
           
                documentPanel.Visible = false;
                documentDisplay.Visible = false;
                documentList.Clear();
                modifyPanel.Visible = false;
                modifyunclassifytable.Visible = false;
                classifiedTab.Controls.Clear();
                unclassifiedTab.Controls.Clear();
              
                tagsModifyInput.Clear();
                employeemodifyssn.Clear();
                modifyContainerPanel.Visible = false;
                panelopendelete.Visible = false;
                tableLayoutDocument.Visible = false;
                getAllCompanies(companyModifyInput);
                getAllCategories(categoryModifyInput);
                getAllProjects(projectModifyInput);
                expandButton.Visible = false;
                tablesearch.Visible = true;
                GridLayoutTable.Visible = false;
                UpdateStatus("", "Done","Success");
            

            }
            catch (Exception ex)
            {
                logger.Logger("refresh", ex.Message, ex.StackTrace);
                return;
            }


        }

        /* UNUSED METHOD -- MAY BE NEEDED LATER
         * 
        /// <summary>
        /// Kills any open instances of Adobe Reader without killing the embedded PDF reader process
        /// (Checks for Adobe Reader vs embedded reader through memory)
        /// </summary>
        private void killProcess()
        {
            Process[] processList = Process.GetProcessesByName("AcroRd32");
            foreach (Process p in processList)
            {
                if (p.PrivateMemorySize64 > Constants.ADOBE_READER_MIN_MEMORY_SIZE)
                {
                    p.Kill();
                }
            }
        }
         * 
         */

        /// <summary>
        /// Hides the currently displayed document grid and displays the modify panel and the embedded
        /// PDF viewer for the selected document.
        /// </summary>
        private void showModifyPanelAndDocumentDisplay()
        {
            try
            {
               
                modifyunclassifytable.Visible = true;

                if (unclassifiedDGV != null) unclassifiedDGV.Visible = false;
                modifyPanel.BringToFront();


                modifyPanel.Visible = true;


                modifyContainerPanel.Visible = true;
                modifyunclassifytable.BringToFront();
                modifyContainerPanel.BringToFront();
                documentPanel.Visible = true;
                documentPanel.BringToFront();
                unclassifiedTab.Controls.Clear();
                unclassifiedTab.Controls.Add(modifyunclassifytable);
                unclassifiedTab.BringToFront();
                getAllCompanies(companyModifyInput);
                getAllCategories(categoryModifyInput);
                getAllProjects(projectModifyInput);
                employeeFirstNameModifyInput.Text = currentEmployee.getEmployeeFirstName();
                employeeLastNameModifyInput.Text = currentEmployee.getEmployeeLastName();
                documentDisplay.BringToFront();
                documentDisplay.Visible = true;
                panelopendelete.Visible = true;
                tableLayoutDocument.Visible = true;
                expandButton.Visible = true;
            }
            catch (Exception ex)
            {
                logger.Logger("showModifyPanelAndDocumentDisplay", ex.Message, ex.StackTrace);
                return;
            }

        }
        private void ShowModifyClassifiedDocumentDisplay()
        {
            try
            {
                modifyunclassifytable.Visible = true;

                if (classifiedDGV != null) classifiedDGV.Visible = false;
                modifyPanel.BringToFront();

                modifyPanel.Visible = true;

                documentPanel.Visible = true;
                documentPanel.BringToFront();
                modifyContainerPanel.Visible = true;
                modifyunclassifytable.BringToFront();
                modifyContainerPanel.BringToFront();

                classifiedTab.Controls.Clear();
                classifiedTab.Controls.Add(modifyunclassifytable);
                classifiedTab.BringToFront();

                documentPanel.Visible = true;
                documentPanel.BringToFront();

           
                documentDisplay.BringToFront();
                documentDisplay.Visible = true;
                panelopendelete.Visible = true;
                tableLayoutDocument.Visible = true;
                expandButton.Visible = true;
            }
            catch (Exception ex)
            {
                logger.Logger("ShowModifyClassifiedDocumentDisplay", ex.Message, ex.StackTrace);
                return;
            }
        }

        /// <summary>
        /// Hides the modify panel and embedded PDF viewer and displays the last document grid.
        /// </summary>
        private void showTablePanel()
        {
            modifyPanel.Visible = false;
            documentDisplay.Visible = false;
            classifiedTab.BringToFront();
            unclassifiedTab.BringToFront();
          
            modifyDocumentButton.Enabled = true;
            tablesearch.Visible = true;
            GridLayoutTable.Visible = true;
            GridLayoutTable.Controls.Clear();
         
        }

        /// <summary>
        /// Places the DataGridView for classified documents in the table panel and docks it.
        /// </summary>
        private void displayClassifiedTable()
        {
            try
            {

                using (classifiedDGV = new DataGridView())
                {
                    classifiedDGV = createClassifiedTable();
                    classifiedDGV.AllowUserToAddRows = false;
                    classifiedDGV.MultiSelect = false;
                    classifiedDGV.AllowUserToOrderColumns = false;
                    classifiedDGV.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    classifiedDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    classifiedDGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(154, 197, 213);
                    classifiedDGV.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    classifiedDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    classifiedDGV.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 228, 198);
                    classifiedDGV.DefaultCellStyle.SelectionForeColor = Color.Black;
                    classifiedDGV.Dock = DockStyle.Fill;
                    classifiedDGV.Cursor = System.Windows.Forms.Cursors.Arrow;
                  
                    TabLayout.SelectedIndex = 1;
                
                    classifiedDGV.ColumnHeadersVisible = true;
                    showTablePanel();
                    GridLayoutTable.Controls.Add(classifiedDGV, 0, 0);

                    classifiedTab.Controls.Add(GridLayoutTable);
                    if (documentList.Count > 0)
                    {
                        currentDocument = documentList[0];
                        classifiedDGV.Rows[0].Selected = true;
                    }
                    documentDisplay.LoadFile(Constants.CLASSIFIED_PATH + currentDocument.getDocumentName() + ".pdf");
                    classifiedDGV.SelectionChanged += new EventHandler(dgvC_SelectionChanged);

                }
            }
            catch (Exception ex)
            {
                logger.Logger("displayClassifiedTable", ex.Message, ex.StackTrace);
                return;
            }
        }

        /// <summary>
        /// Places text and controls and sets the style of DataGridView for classified documents (size of columns, etc.)
        /// </summary>
        /// <returns></returns>
        /// 
        private DataGridView createClassifiedTable()
        {
            dgvC = new DataGridView();
            dgvC.AutoSize = false;
            dgvC.ForeColor = Color.Black;
            dgvC.Cursor=System.Windows.Forms.Cursors.Arrow;
            dgvC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvC.RowHeadersVisible = false;
            dgvC.AllowUserToResizeRows = false;
            dgvC.AllowUserToResizeColumns = false;
            dgvC.AllowUserToDeleteRows = false;
            dgvC.AllowUserToResizeRows = false;
            dgvC.AllowUserToOrderColumns = false;
            dgvC.AllowUserToAddRows = false;
            
            dgvC.ReadOnly = true;
            dgvC.BackgroundColor = Color.White;
            dgvC.EnableHeadersVisualStyles = false;
            dgvC.BorderStyle = BorderStyle.None;
            dgvC.Dock = DockStyle.Fill;

            dgvC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvC.ColumnHeadersHeight = 40;
            dgvC.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 228, 198);
            dgvC.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvC.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvC.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvC.Columns.Add("col1", "Document Id");
            dgvC.Columns.Add("col2", "Document Name");
            dgvC.Columns.Add("col3", "Date");
            dgvC.Columns.Add("col4", "Company");
            dgvC.Columns.Add("col5", "Category");
            dgvC.Columns.Add("col6", "SubCategory");
            dgvC.Columns.Add("col7", "Project");
            dgvC.Columns.Add("col8", "Employee");
       

                lastcolumn1 = new TextAndImageColumn();
            lastcolumn1.SortMode = DataGridViewColumnSortMode.NotSortable;

           
            lastcolumn1.ReadOnly = true;
         
            dgvC.Columns.Add(lastcolumn1);

            lastcolumn2 = new TextAndImageColumn();
            lastcolumn2.HeaderText = "Action";
           
            lastcolumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            
            dgvC.Columns.Add(lastcolumn2);

            lastcolumn3 = new TextAndImageColumn();
            lastcolumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvC.Columns.Add(lastcolumn3);


            dgvC.Columns[8].Width = 10;
            dgvC.Columns[9].Width = 10;
            dgvC.Columns[10].Width = 15;
            dgvC.Columns[0].Visible = false;
           
         

            dgvC.CellClick += new DataGridViewCellEventHandler(dgvC_CellClick);
            dgvC.KeyDown += new KeyEventHandler(dgvC_KeyDown);
            dgvC.CellMouseMove +=new DataGridViewCellMouseEventHandler(dgvC_CellMouseMove);
            dgvC.CellMouseLeave+=new DataGridViewCellEventHandler(dgvC_CellMouseLeave);
            

            for (int indx = 0; indx < documentList.Count; indx++)
            {
                Category subcategoryname=new Category();
                // string employeename =(documentList[indx].getEmployee().ToString() == ", ") ? "" : documentList[indx].getEmployee().ToString();

                //if (documentList[indx].getCategory().getParentId() != 0)
                //{
                //    getCategoryFromSubcategory(categoryModifyInput, documentList[indx].getCategory().getCategoryId());
                //   string[] temp= {indx.ToString(),documentList[indx].getDocumentName(), documentList[indx].getDocumentDate().Value.Date.ToString("MMMM dd, yyyy"),documentList[indx].getCompany().ToString(),
                //                  categoryname.ToString(),documentList[indx].getCategory().ToString(),documentList[indx].getProject().ToString(),documentList[indx].getEmployee().ToString()};
                //   dgvC.Rows.Add(temp);
                //}
                //else
                //{
                //    string[] temp = {indx.ToString(),documentList[indx].getDocumentName(), documentList[indx].getDocumentDate().Value.Date.ToString("MMMM dd, yyyy"),documentList[indx].getCompany().ToString(),
                //                 documentList[indx].getCategory().ToString(),null,documentList[indx].getProject().ToString(),documentList[indx].getEmployee().ToString()};
                //    dgvC.Rows.Add(temp);
                //}                
                    string[] temp = { indx.ToString(), documentList[indx].getDocumentName(), documentList[indx].getUploadedDate().Value.Date.ToString("MMMM dd, yyyy") };
                    dgvC.Rows.Add(temp);
                    dgvC.Rows[indx].Height = 30;
                    lastcolumn1.Image = global::EdocUI.Properties.Resources.icon_open;
                    lastcolumn1.Button = "OPEN";
                    lastcolumn2.Image = global::EdocUI.Properties.Resources.icon_edit;
                    lastcolumn2.Button = "EDIT";
                    lastcolumn3.Image = global::EdocUI.Properties.Resources.icon_delete;
                    lastcolumn3.Button = "DELETE";                
                
                dgvC.Rows[indx].Height = 30;
                
                lastcolumn1.Image = global::EdocUI.Properties.Resources.icon_open;
                lastcolumn1.Button = "OPEN";
                lastcolumn2.Image = global::EdocUI.Properties.Resources.icon_edit;
                lastcolumn2.Button = "EDIT";
                lastcolumn3.Image = global::EdocUI.Properties.Resources.icon_delete;
                lastcolumn3.Button = "DELETE";

              
            }
            if (documentList.Count == 0)
            {
                UpdateStatus("", "No Data Found", "Error");
            }

            return dgvC;

        }

     

       private void dgvC_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex>-1 && e.ColumnIndex >= 8)
            {
                classifiedDGV.Cursor = System.Windows.Forms.Cursors.Hand;
            }
            else
            {
                classifiedDGV.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }

    
        /// <summary>
        /// Places the DataGridView for unclassified documents in the table panel and docks it.
        /// </summary>
        private void displayUnclassifiedTable()
        {
            try
            {
                using (unclassifiedDGV = new DataGridView())
                {
                    unclassifiedDGV = createUnclassifiedTable();
                               
                    TabLayout.SelectedIndex = 0;
                    unclassifiedDGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(154, 197, 213);
                    unclassifiedDGV.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    unclassifiedDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    unclassifiedDGV.AllowUserToAddRows = false;
                    unclassifiedDGV.MultiSelect = false;
                    unclassifiedDGV.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 228, 198);
                    unclassifiedDGV.DefaultCellStyle.SelectionForeColor = Color.Black;
                    unclassifiedDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    unclassifiedDGV.Dock = DockStyle.Fill;
                    unclassifiedDGV.AllowUserToOrderColumns = false;
                 
                    unclassifiedDGV.Cursor = System.Windows.Forms.Cursors.Arrow;
                    unclassifiedDGV.ColumnHeadersVisible = true;
                    showTablePanel();
                    GridLayoutTable.Controls.Add(unclassifiedDGV, 0, 0);
                    unclassifiedTab.Controls.Add(GridLayoutTable);
                    if (documentList.Count > 0)
                    {
                        //currentDocument = (currentDocument.getDocumentName() == null) ? documentList[0] : currentDocument;                        
                        currentDocument = documentList[0];
                        unclassifiedDGV.Rows[0].Selected = true;

                    }
                    documentDisplay.LoadFile(Constants.DEFAULT_PATH + currentDocument.getDocumentName() + ".pdf");
                    unclassifiedDGV.SelectionChanged += new EventHandler(dgvUC_SelectionChanged);
                }
            }
            catch (Exception ex)
            {
                logger.Logger("displayUnclassifiedTable", ex.Message, ex.StackTrace);
                return;
            }
        }

        /// <summary>
        /// Places text and controls and sets the style of DataGridView for unclassified documents (size of columns, etc.)
        /// </summary>
        /// <returns></returns>
        private DataGridView createUnclassifiedTable()
        {

            dgvUC = new DataGridView();
            dgvUC.AutoSize = false;
            dgvUC.ForeColor = Color.Black;
            dgvUC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUC.RowHeadersVisible = false;
            dgvUC.AllowUserToResizeRows = false;
            dgvUC.AllowUserToResizeColumns = false;
            dgvUC.AllowUserToDeleteRows = false;

            dgvUC.AllowUserToAddRows = false;
            dgvUC.ReadOnly = true;
            dgvUC.BackgroundColor = Color.White;
            dgvUC.EnableHeadersVisualStyles = false;
            dgvUC.BorderStyle = BorderStyle.None;
            dgvUC.Dock = DockStyle.Fill;
            dgvUC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvUC.ColumnHeadersHeight = 40;
            dgvUC.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 228, 198);
            dgvUC.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUC.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvUC.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUC.Columns.Add("col1", "Document Id");
            dgvUC.Columns.Add("col2", "Document Name");
            dgvUC.Columns.Add("col3", "Date Uploaded");
        
            dgvUC.TabStop = false;
            lastcolumn1 = new TextAndImageColumn();
            lastcolumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvUC.Columns.Add(lastcolumn1);
           // dgvUC.ColumnHeadersVisible = false;

            lastcolumn2 = new TextAndImageColumn();
            lastcolumn2.HeaderText = "Action";
            lastcolumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvUC.Columns.Add(lastcolumn2);

            lastcolumn3 = new TextAndImageColumn();
            lastcolumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvUC.Columns.Add(lastcolumn3);
            dgvUC.Columns[0].Width = 0;
            dgvUC.Columns[1].Width = 100;
            dgvUC.Columns[2].Width = 100;
            dgvUC.Columns[3].Width = 15;
            dgvUC.Columns[4].Width = 15;
            dgvUC.Columns[5].Width = 15;

            dgvUC.Columns[0].Visible = false;

            dgvUC.CellClick += new DataGridViewCellEventHandler(dgvUC_CellClick);
            dgvUC.CellMouseMove +=new DataGridViewCellMouseEventHandler(dgvUC_CellMouseMove);
            dgvUC.CellMouseLeave +=new DataGridViewCellEventHandler(dgvUC_CellMouseLeave);
            dgvUC.KeyDown +=new KeyEventHandler(dgvUC_KeyDown);
         

            for (int indx = 0; indx < documentList.Count; indx++)
            {
                string[] temp = {indx.ToString(), documentList[indx].getDocumentName(), documentList[indx].getUploadedDate().Value.Date.ToString("MMMM dd, yyyy") };
                dgvUC.Rows.Add(temp);
                dgvUC.Rows[indx].Height = 30;
                lastcolumn1.Image = global::EdocUI.Properties.Resources.icon_open;
                lastcolumn1.Button = "OPEN";
                lastcolumn2.Image = global::EdocUI.Properties.Resources.icon_edit;
                lastcolumn2.Button = "EDIT";
                lastcolumn3.Image = global::EdocUI.Properties.Resources.icon_delete;
                lastcolumn3.Button = "DELETE";


            }
            
            if (documentList.Count == 0)
            {
                UpdateStatus("", "No Data Found", "Error");
            }

            return dgvUC;
        }

       

       private void dgvUC_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            unclassifiedDGV.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

       private void dgvC_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            classifiedDGV.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

       private void dgvUC_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex>-1 && e.ColumnIndex >= 3)
            {
                unclassifiedDGV.Cursor = System.Windows.Forms.Cursors.Hand;
            }
            else
            {
                unclassifiedDGV.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }

       private void dgvUC_KeyDown(object sender, KeyEventArgs e)
        {
           try{
           int keycode=Convert.ToInt32(e.KeyCode);
           
            if(keycode==13)
            {
                e.Handled = true;
                object rowdata = unclassifiedDGV.Rows[unclassifiedDGV.CurrentCell.RowIndex].Cells[0].Value;
                int colindex = unclassifiedDGV.CurrentCell.ColumnIndex;
              
                dgvUCSelectionIndexChanged(rowdata);
              
                switch (colindex)
                {
                    case 3:
                        Process.Start(Constants.DEFAULT_PATH + currentDocument.getDocumentName() + ".pdf");
                        break;
                    case 4:
                        ShowModifyUnclassifiedPanel();
                        break;
                    case 5:
                        deleteButton_Click();
                        break;
                    default:
                        break;
                }
            
            }
       }
              
                 catch (Win32Exception ex)
            {
                DialogResult d = MessageBox.Show("The requested document could not be opened.", "Error Opening Document",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                UpdateStatus("dgvUC_KeyDown", "The requested document is not present in the repository", "Warning");
            }
           catch (Exception ex)
           {
               logger.Logger("dgvUC_KeyDown", ex.Message, ex.StackTrace);
           }
        }

       private void dgvC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int keycode = Convert.ToInt32(e.KeyCode);

               
                if (keycode == 13)
                {
                    e.Handled = true;
                    object rowdata = classifiedDGV.Rows[classifiedDGV.CurrentCell.RowIndex].Cells[0].Value;
                    int colindex = classifiedDGV.CurrentCell.ColumnIndex;
                    dgvCSelectionIndexChanged(rowdata);
                    switch (colindex)
                    {
                        case 8:

                            Process.Start(Constants.CLASSIFIED_PATH + currentDocument.getDocumentName() + ".pdf");
                            break;
                        case 9:
                            ShowModifyUnclassifiedPanel();
                            break;
                        case 10:
                            deleteButton_Click();
                            break;
                        default:
                            break;
                    }

                  
                }
              
            }
           
                catch (Win32Exception ex)
            {
                DialogResult d = MessageBox.Show("The requested document could not be opened.", "Error Opening Document",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                UpdateStatus("dgvC_CellClick","The requested document is not present in the repository","Warning");
            }
          
            
        }

      


        /// <summary>
        /// Opens the selected classified document in Adobe Reader when "Open" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                UpdateStatus("","Done","Success");
           
                if (e.RowIndex > -1) {
                    switch (e.ColumnIndex)
                    {
                        case 8:
                            Process.Start(Constants.CLASSIFIED_PATH + currentDocument.getDocumentName() + ".pdf");
                            break;
                        case 9:
                            ShowModifyUnclassifiedPanel();
                            break;
                        case 10:
                            deleteButton_Click();
                            break;
                        default:
                            break;
                    }
                }


            }
            catch (Win32Exception ex)
            {
                DialogResult d = MessageBox.Show("The requested document could not be opened.", "Error Opening Document",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                UpdateStatus("dgvC_CellClick","The requested document is not present in the repository","Warning");
            }
        }

        /// <summary>
        /// Sets the current classified document to the one represented by the selected row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvC_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                object rowdata = classifiedDGV.Rows[classifiedDGV.CurrentCell.RowIndex].Cells[0].Value;
                dgvCSelectionIndexChanged(rowdata);
              
            }
            catch (Exception ex)
            {
                logger.Logger("dgvC_SelectionChanged", ex.Message, ex.StackTrace);
                return;
            }
        }
        private void dgvCSelectionIndexChanged(object rowdata)
        {
            docIndex = Convert.ToInt32(rowdata);
            currentDocument = documentList[Convert.ToInt32(rowdata)];
            companyModifyInput.SelectedItem = null;
            categoryModifyInput.SelectedItem = null;
            projectModifyInput.SelectedItem = null;
            if (documentDisplay.Visible == false)
            {
                documentDisplay.LoadFile(Constants.CLASSIFIED_PATH + currentDocument.getDocumentName() + ".pdf");
            }
            UpdateStatus("", "Done", "Success");
        }
        /// <summary>
        /// Opens the selected document in Adobe Reader when "Open" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
          
                if (e.RowIndex > -1)
                {
                    switch (e.ColumnIndex)
                    {
                        case 3:
                            Process.Start(Constants.DEFAULT_PATH + currentDocument.getDocumentName() + ".pdf");
                            break;
                        case 4:
                            ShowModifyUnclassifiedPanel();
                            break;
                        case 5:
                            deleteButton_Click();
                            break;
                        default:
                            break;
                    }
                    UpdateStatus("", "Done", "Success");
                }
            }

            catch (Exception ex)
            {
                logger.Logger("dgvUC_CellClick", ex.Message, ex.StackTrace);
            }
        }
        private void ShowModifyUnclassifiedPanel()
        {

            try
            {
                if (documentList.Count > 0)
                {
                    companyModifyInput.SelectedItem = null;
                    categoryModifyInput.SelectedItem = null;
                    projectModifyInput.SelectedItem = null;
                   

                    documentNameModifyInput.Text = currentDocument.getDocumentName();
                    if (mode == 1)
                    {
                        companyModifyInput.SelectedItem = currentDocument.getCompany();
                        if (currentDocument.getCategory().getParentId() != 0)
                        {
                            getCategoryFromSubcategory(categoryModifyInput, currentDocument.getCategory().getCategoryId());
                            getSubcategories(subcategoryModifyInput, ((Category)categoryModifyInput.SelectedItem).getCategoryId());
                            subcategoryModifyInput.SelectedItem = currentDocument.getCategory();
                        }
                        else
                        {
                            categoryModifyInput.SelectedItem = currentDocument.getCategory();
                        }
                        if (currentDocument.getProject().getProjectName() != null)
                        {
                            projectModifyInput.SelectedItem = currentDocument.getProject();
                        }
                        else
                        {
                            projectModifyInput.SelectedIndex = 0;
                        }
                        employeeLastNameModifyInput.Text = currentDocument.getEmployee().getEmployeeLastName();
                        employeeFirstNameModifyInput.Text = currentDocument.getEmployee().getEmployeeFirstName();
                       // employeessnmodifyinput.Text = currentDocument.getEmployee().getEmployeeId();
                        tagsModifyInput.Text = currentDocument.getTags();
                        employeemodifyssn.Text = currentDocument.getEmployeeSsn().ToString();
                        documentDateModifyInput.Text = currentDocument.getDocumentDate().ToString();
                        ShowModifyClassifiedDocumentDisplay();
                    }
                    else
                    {
                        documentDateModifyInput.Text = currentDocument.getUploadedDate().ToString();
                        showModifyPanelAndDocumentDisplay();
                    }
                    subcategoryModifyInput.SelectedItem = currentDocument.getCategory();
                    UpdateStatus("", "Done", "Success");
                }
            }
            catch (Exception ex)
            {
                logger.Logger("ShowModifyUnclassifiedPanel", ex.Message, ex.StackTrace);
                return;
            }
        }

        /// <summary>
        /// Sets the current document to the one represented by the selected row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUC_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                object rowdata = unclassifiedDGV.Rows[unclassifiedDGV.CurrentCell.RowIndex].Cells[0].Value;
                dgvUCSelectionIndexChanged(rowdata);

            }
            catch (Exception ex)
            {
                logger.Logger("dgvUC_SelectionChanged", ex.Message, ex.StackTrace);
                return;
            }
        }
        private void dgvUCSelectionIndexChanged(object rowdata)
        {
            docIndex = (unclassifiedDGV.CurrentCell == null) ? 0 : Convert.ToInt32(rowdata);
            currentDocument = (unclassifiedDGV.CurrentCell == null) ? documentList[0] : documentList[Convert.ToInt32(rowdata)];



            companyModifyInput.SelectedItem = null;
            categoryModifyInput.SelectedItem = null;
            projectModifyInput.SelectedItem = null;

            if (documentDisplay.Visible == false)
            {
                documentDisplay.LoadFile(Constants.DEFAULT_PATH + currentDocument.getDocumentName() + ".pdf");
            }
            UpdateStatus("", "Done", "Success");
        }
        #endregion

        public static void UpdateStatus(string functionane, string error, string status)
        {
            StatusControl.Text = error;
            if (status == "Error")
                StatusControl.Image = global::EdocUI.Properties.Resources.DeleteOrgn_Icon;
            else if (status == "Warning")
            {
                StatusControl.Image = global::EdocUI.Properties.Resources.form_attn;
            }
            else
                StatusControl.Image = global::EdocUI.Properties.Resources.icon_done;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (documentNameModifyInput.TextLength > 50)
                {
                    MessageBox.Show("Document Name length must be less than 50");
                    return;
                }
                #region Check for company, category, and employee input
                if ((string.IsNullOrWhiteSpace(employeeLastNameModifyInput.Text) &&
                       !string.IsNullOrWhiteSpace(employeeFirstNameModifyInput.Text))
                      ||
                      (string.IsNullOrWhiteSpace(employeeFirstNameModifyInput.Text) &&
                       !string.IsNullOrWhiteSpace(employeeLastNameModifyInput.Text)))
                {
                    MessageBox.Show("You must enter both an employee first name and last name.");
                    return;
                }

                else if (Convert.ToString(companyModifyInput.Text) == Constants.DEFAULT_COMBO_TEXT ||
                       Convert.ToString(categoryModifyInput.Text) == Constants.DEFAULT_COMBO_TEXT)
                {
                    MessageBox.Show("You must enter at least a company and a category.");
                    return;
                }
                #endregion

                #region Set new document values

                 if (!string.IsNullOrWhiteSpace(employeeFirstNameModifyInput.Text) &&
                    !string.IsNullOrWhiteSpace(employeeLastNameModifyInput.Text))
                
                {
                    if (
                        !(Convert.ToBoolean(validateUser(employeeFirstNameModifyInput.Text, employeeLastNameModifyInput.Text))))
                    {
                        MessageBox.Show("Not a valid employee.");
                        return;
                    }
                }

                string temp = currentDocument.getDocumentName();
                currentDocument.setDocumentName(documentNameModifyInput.Text);
                
                currentDocument.setCompany((Company)companyModifyInput.SelectedItem);
                currentDocument.setProject((Convert.ToString(projectModifyInput.Text) == Constants.DEFAULT_COMBO_TEXT ? new Project(0, Constants.DEFAULT_COMBO_TEXT) : (Project)projectModifyInput.SelectedItem));
                if (mode == 0)
                {
                    currentDocument.setEmployee((string.IsNullOrWhiteSpace(employeeFirstNameModifyInput.Text) || string.IsNullOrWhiteSpace(employeeLastNameModifyInput.Text)) ?
                      new Employee() : new Employee(currentEmployee.getEmployeeId(), employeeFirstNameModifyInput.Text.Trim(), employeeLastNameModifyInput.Text.Trim()));
                }
                else
                {
                    currentDocument.setEmployee((string.IsNullOrWhiteSpace(employeeFirstNameModifyInput.Text) || string.IsNullOrWhiteSpace(employeeLastNameModifyInput.Text)) ?
                      new Employee() : new Employee(currentDocument.getEmployee().getEmployeeId(), employeeFirstNameModifyInput.Text.Trim(), employeeLastNameModifyInput.Text.Trim()));
                }
                if (employeemodifyssn.Text != "")
                {
                    if (Convert.ToInt32(employeemodifyssn.Text) >= 100000000 && Convert.ToInt32(employeemodifyssn.Text) <= 999999999)
                    {

                        currentDocument.setEmployeeSsn(Convert.ToInt32(employeemodifyssn.Text));

                    }
                    else
                    {
                        MessageBox.Show("Employee SSN value must be from 100000000 to 999999999");
                        return;
                    }
                }
                else
                {
                    currentDocument.setEmployeeSsn(0);
                }
                currentDocument.setDocumentDate((DateTime)documentDateModifyInput.Value);
                currentDocument.setTags(tagsModifyInput.Text);
                currentDocument.setUpdatedDate(DateTime.Now);
                currentDocument.setUpdatedBy(currentEmployee);
                if (subcategoryModifyInput.Enabled == false || Convert.ToString(subcategoryModifyInput.Text) == Constants.DEFAULT_COMBO_TEXT)
                {
                    currentDocument.setCategory((Category)categoryModifyInput.SelectedItem);
                }
                else
                {
                    currentDocument.setCategory((Category)subcategoryModifyInput.SelectedItem);
                }

                bool tryAgain = true;

                #region Insert Document (Mode 0)
                if (mode == 0)
                {
                    currentDocument.setDocumentId(0);
                    while (tryAgain)
                    {
                        try
                        {
                            File.Move(Constants.DEFAULT_PATH + temp + ".pdf",
                                Constants.CLASSIFIED_PATH + currentDocument.getDocumentName() + ".pdf");
                            insertDocument(currentDocument, temp);
                          
                            modifyUnclassifiedTabClicked();
                            return;
                        }
                        catch (HttpRequestException ex)
                        {

                            logger.Logger("applyButton_Click/Insert Document", ex.Message, ex.StackTrace);
                            return;
                        }
                        catch (IOException ex)
                        {
                           
                            DialogResult d =
                             
                                MessageBox.Show(
                                   ex.Message, "Warning", MessageBoxButtons.RetryCancel,
                                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.DefaultDesktopOnly);

                            if (d == DialogResult.Retry)
                            {
                                tryAgain = true;
                            }
                            else
                            {
                                modifyUnclassifiedTabClicked();
                               
                                return;
                            }
                            UpdateStatus("applyButton_Click", "The requested document is not present in the repository","Error");
                        }
                    }
                }
                #endregion

                #region Update Document (Mode 1)
                else
                {
                    while (tryAgain)
                    {
                        try
                        {
                            File.Move(Constants.CLASSIFIED_PATH + temp + ".pdf",
                                Constants.CLASSIFIED_PATH + currentDocument.getDocumentName() + ".pdf");
                            updateDocument(currentDocument, temp);
                          //  searchButton.PerformClick();
                            return;
                        }
                        catch (HttpRequestException ex)
                        {
                            logger.Logger("applyButton_Click/Update Document", ex.Message, ex.StackTrace);
                            return;
                        }
                        catch (IOException ex)
                        {
                           
                            DialogResult d =                                
                            MessageBox.Show(
                                  ex.Message, "Warning", MessageBoxButtons.RetryCancel,
                                   MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                                   MessageBoxOptions.DefaultDesktopOnly);

                            if (d == DialogResult.Retry)
                            {
                                tryAgain = true;
                            }
                            else
                            {
                                searchButton.PerformClick();
                                return;
                            }
                            UpdateStatus("applyButton_Click", "The requested document is not present in the repository","Error");
                        }
                    }
                }
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                logger.Logger("applyButton_Click", ex.Message, ex.StackTrace);
                return;
            }
        }

        private void openDocumentButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (mode == 0)
                {
                    Process.Start(Constants.DEFAULT_PATH + currentDocument.getDocumentName() + ".pdf");
                }
                else
                {
                    Process.Start(Constants.CLASSIFIED_PATH + currentDocument.getDocumentName() + ".pdf");
                }
            }
            catch (Win32Exception ex)
            {
                DialogResult d = MessageBox.Show("The requested document could not be opened.", "Error Opening Document",
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (d == DialogResult.Retry)
                {
                    openDocumentButton.PerformClick();
                }
                UpdateStatus("openDocumentButton_Click", "The requested document is not present in the repository", ex.StackTrace);
            }
        }
       
        private void deleteButton_Click()
        {
            try
            {
                if (documentList.Count > 0)
                {
                    if (mode == 0)
                   {

                       DialogResult d1 = MessageBox.Show("You cannot delete an unclassified document.", "Delete Document", MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                       //File.Delete(Constants.DEFAULT_PATH + currentDocument.getDocumentName() + ".pdf");
                       //modifyUnclassifiedTabClicked();
                        return;
                    }
                    DialogResult d2 = MessageBox.Show("Are you sure?", "Delete Document", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    if (d2 == DialogResult.Yes)
                    {
                        try
                        {
                            currentDocument.setUpdatedDate(DateTime.Now);
                            //int count = 1;
                            //string filename = Path.GetFileNameWithoutExtension(Constants.ARCHIVE_PATH + currentDocument.getDocumentName() + ".pdf");
                            //string extension = Path.GetExtension(Constants.ARCHIVE_PATH + currentDocument.getDocumentName() + ".pdf");
                            //string path = Path.GetDirectoryName(Constants.ARCHIVE_PATH + currentDocument.getDocumentName() + ".pdf");
                            //string newfullpath = Constants.ARCHIVE_PATH + currentDocument.getDocumentName() + ".pdf";
                            //while (File.Exists(newfullpath))
                            //{
                            //    string tempfilename = string.Format("{0}({1})", filename, count++);
                            //    newfullpath = Path.Combine(path, tempfilename + extension);
                            //}
                            //File.Move(Constants.CLASSIFIED_PATH + currentDocument.getDocumentName() + ".pdf",
                            //   newfullpath);


                            if (File.Exists(Constants.ARCHIVE_PATH + currentDocument.getDocumentName() + ".pdf"))
                            {

                                DialogResult d3 = MessageBox.Show("File already Exists" + "\n" + "Do you want to overwrite it?", "Delete Document", MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning);
                                if (d3 == DialogResult.Yes)
                                {
                                    File.Delete(Constants.ARCHIVE_PATH + currentDocument.getDocumentName() + ".pdf");
                                    File.Move(Constants.CLASSIFIED_PATH + currentDocument.getDocumentName() + ".pdf",
                              Constants.ARCHIVE_PATH + currentDocument.getDocumentName() + ".pdf");
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                File.Move(Constants.CLASSIFIED_PATH + currentDocument.getDocumentName() + ".pdf",
                                Constants.ARCHIVE_PATH + currentDocument.getDocumentName() + ".pdf");
                            }
                            deleteDocument(currentDocument.getDocumentId());
                            refresh();
                            searchButton.PerformClick();
                        }
                        catch (HttpRequestException ex)
                        {

                            logger.Logger("deleteButton_Click", ex.Message, ex.StackTrace);
                        }
                        catch (IOException ex)
                        {

                            MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            UpdateStatus("", ex.Message, "Warning");
                                return;
                            }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Logger("deleteButton_Click", ex.Message, ex.StackTrace);
                return;
            }

        }

        private void categoryModifyInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                subcategoryModifyInput.Items.Clear();
                if (Convert.ToString(((ComboBox)sender).Text) != "" && Convert.ToString(((ComboBox)sender).Text) != Constants.DEFAULT_COMBO_TEXT && ((Category)((ComboBox)sender).SelectedItem).getHasChildren()) // IMPORTANT: Same idea for implementing modifyPanel pre-filled fields!
                {
                    int temp = ((Category)((ComboBox)sender).SelectedItem).getCategoryId();
                    getSubcategories(subcategoryModifyInput, temp);
                    subcategoryModifyInput.Enabled = true;
                }
                else
                {
                    subcategoryModifyInput.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                logger.Logger("categoryModifyInput_SelectedIndexChanged", ex.Message, ex.StackTrace);
                return;
            }
        }
        private bool IsExpand = false;
        private void expandButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsExpand)
                {
                    tablesearch.Visible = false;
                
                    IsExpand = true;
                }
                else
                {
                    tablesearch.Visible = true;
                    IsExpand = false;
                }
            }
            catch (Exception ex)
            {

                logger.Logger("expandButton_Click", ex.Message, ex.StackTrace);
            }

        }

        private void PerformSearch(KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            if (key == '\r')
            {
                searchButton.PerformClick();
            }
        }
       private void documentNameInput_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            PerformSearch(e);
        }
       private void employeeFirstNameInput_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            PerformSearch(e);
        }
       private void employeeSsnInput_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            PerformSearch(e);

        }
       private void employeeLastNameInput_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            PerformSearch(e);
        }

       private void tagsInput_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            PerformSearch(e);
        }
       private void AllowOnlyNumeric(KeyEventArgs e)
       {
           int keycode = Convert.ToInt32(e.KeyCode);
           if ((keycode >= 48 && keycode <= 57) || keycode == 8)
           {
               e.Handled = true;
           }
           else if ((keycode >= 96 && keycode <= 105) || keycode == 13)
           {
               e.Handled = true;
           }
           else
           {
               e.SuppressKeyPress = true;
           }
       }
       private void employeemodifySsn_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            AllowOnlyNumeric(e);
        }
       void employeeSsnInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
       {
           AllowOnlyNumeric(e);
       }

    }
   


    public class TextAndImageColumn : DataGridViewTextBoxColumn
    {
        private Image imageValue;
        private Size imageSize;
        private string ButtonValue;

        public TextAndImageColumn()
        {
            this.CellTemplate = new TextAndImageCell();
        }

        public override object Clone()
        {
            TextAndImageColumn c = base.Clone() as TextAndImageColumn;
            c.imageValue = this.imageValue;
            c.imageSize = this.imageSize;
            c.ButtonValue = this.ButtonValue;
            return c;
        }

        public Image Image
        {
            get { return this.imageValue; }
            set
            {
                if (this.Image != value)
                {
                    this.imageValue = value;
                    this.imageSize = value.Size;

                    if (this.InheritedStyle != null)
                    {
                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        /*this.DefaultCellStyle.Padding = new Padding(imageSize.Width,
                                              inheritedPadding.Top, inheritedPadding.Right,
                                              inheritedPadding.Bottom);*/
                        this.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                       
                    }
                }
            }
        }
        public string Button
        {
            get { return this.ButtonValue; }
            set { this.ButtonValue = value; }

        }

        private TextAndImageCell TextAndImageCellTemplate
        {
            get { return this.CellTemplate as TextAndImageCell;
            
            }
           
        }


        internal Size ImageSize
        {
            get { return imageSize; }
        }
    }

    public class TextAndImageCell : DataGridViewTextBoxCell
    {
        private Image imageValue;
        private Size imageSize;
        private string ButtonValue;

        public override object Clone()
        {
            TextAndImageCell c = base.Clone() as TextAndImageCell;
            c.imageValue = this.imageValue;
            c.imageSize = this.imageSize;
            c.ButtonValue = this.ButtonValue;
            return c;
        }

        public Image Image
        {
            get
            {
                
                if (this.OwningColumn == null ||
                         this.OwningTextAndImageColumn == null)
                {
                    return imageValue;
                }
                else if (this.imageValue != null)
                {
                    return this.imageValue;
                }
                else
                {
                    return this.OwningTextAndImageColumn.Image;
                   
                }
            }
            set
            {
                if (this.imageValue != value)
                {
                    this.imageValue = value;
                    this.imageSize = value.Size;

                    Padding inheritedPadding = this.InheritedStyle.Padding;
                    //this.Style.Padding = new Padding(imageSize.Width,
                    //inheritedPadding.Top, inheritedPadding.Right,
                    //inheritedPadding.Bottom);
                    inheritedPadding.Top = 2;
                    
                  
                }
            }
        }
        public string Button
        {


            get
            {
                if (this.OwningColumn == null ||
                       this.OwningTextAndImageColumn == null)
                {
                    return ButtonValue;
                }
                else if (this.ButtonValue != null)
                {
                    return this.ButtonValue;
                }
                else
                {
                    return this.OwningTextAndImageColumn.Button;
                }
            }
            set
            {
                if (this.ButtonValue != value)
                {
                    this.ButtonValue = value;
                }
            }
        }


        protected override void Paint(Graphics graphics, Rectangle clipBounds,
                               Rectangle cellBounds, int rowIndex
                               , DataGridViewElementStates cellState,
                               object value, object formattedValue, string errorText,
                               DataGridViewCellStyle cellStyle,
                               DataGridViewAdvancedBorderStyle advancedBorderStyle,
                               DataGridViewPaintParts paintParts)
        {
            // Paint the base content
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
               value, formattedValue, errorText, cellStyle,
               advancedBorderStyle, paintParts);

            if (this.Image != null)
            {
                // Draw the image clipped to the cell.
                System.Drawing.Drawing2D.GraphicsContainer container =
                graphics.BeginContainer();
                graphics.SetClip(cellBounds);
                graphics.DrawImageUnscaled(this.Image, cellBounds.Location);
                graphics.EndContainer(container);

            }
            if (this.Button != null)
            {
                this.Value = this.Button;
            }
        }

        private TextAndImageColumn OwningTextAndImageColumn
        {
            get { return this.OwningColumn as TextAndImageColumn; }
        }


        public DataGridViewCellEventArgs e { get; set; }
    }

}


