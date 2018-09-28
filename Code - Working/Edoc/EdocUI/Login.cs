using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EdocApp.DataModel;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using System.Configuration;
namespace EdocUI
{
    public partial class Login : Form
    {

        private List<Employee> adminList;
        public bool isAdmin;
        public Employee currentEmployee;
        private MessageLogger logger;
        public static ToolStripStatusLabel StatusControl;
        public Login()
        {
            logger = new MessageLogger();
            logger.edocobj = "Login";
            isAdmin = false;
            adminList = new List<Employee>();
            fillAdminList();            
            InitializeComponent();
            this.AcceptButton = applyButton;
            StatusControl = LoginStatusLabel;      
        }

    /*    private void loginpanel_Paint(object sender, PaintEventArgs e)
        {
            using(LinearGradientBrush brush=new LinearGradientBrush(this.ClientRectangle,Color.FromArgb(70,153,183),Color.FromArgb(52,119,148),90F))
            {           
                
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
           
        }
        private void applyButton_Paint(object sender, PaintEventArgs e)
        {
            using(LinearGradientBrush brush=new LinearGradientBrush(this.ClientRectangle,Color.FromArgb(197,174,36),Color.FromArgb(201,188,86),90F))
            {           
                
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
           
        }*/
     
        
      
        /// <summary>
        /// Web API call (GET) to retrieve all admin users in the database and fill
        /// the List of Employee objects with them.
        /// </summary>
        private async void fillAdminList()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = client.GetAsync(Constants.API_ROOT_URI + "Employees/GetAdmin").Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var adminJsonString = response.Content.ReadAsStringAsync().Result;
                            adminList.AddRange(JsonConvert.DeserializeObject<List<Employee>>(adminJsonString));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Logger("fillAdminList", ex.Message, ex.StackTrace);                      
                return;
            }
        }

        /// <summary>
        /// Determines what occurs when the user presses the apply button or the "Enter" key
        /// Checks user input and, if it isn't blank, validates the user input by checking
        /// against the List Employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userFirstNameInput.Text) ||
                        string.IsNullOrWhiteSpace(userLastNameInput.Text))
                {
                    MessageBox.Show("You must enter a both a first and last name.");
                }
                else
                {
                    Employee temp = new Employee(userFirstNameInput.Text.Trim(), userLastNameInput.Text.Trim());
                  //  adminList = null;
                    foreach (Employee employee in adminList)
                    {
                        
                        if (employee.Equals(temp))
                        {
                            isAdmin = true;
                            currentEmployee = employee;
                            this.Close();
                            return;
                        }
                    }
                    MessageBox.Show("Not a valid admin user.");
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Logger("applyButton_Click", ex.Message, ex.StackTrace);    
                return;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        public static void UpdateStatus(string functionane, string error, string status)
        {
            StatusControl.Text = error;
            if (status == "Success")
                StatusControl.Image = global::EdocUI.Properties.Resources.icon_done;
            
            else
                StatusControl.Image = global::EdocUI.Properties.Resources.DeleteOrgn_Icon;
          

        }
    }
}
