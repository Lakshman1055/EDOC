using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EdocUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             Login loginForm;
            using (loginForm = new Login())
            {
                Application.Run(loginForm);
                if (loginForm.isAdmin == true)
                {
                        Application.Run(new EdocDesktopApplication(loginForm.currentEmployee));
                }
                
            }
        }
    }
}
