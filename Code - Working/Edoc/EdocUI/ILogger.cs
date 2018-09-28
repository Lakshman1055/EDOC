using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EdocUI
{
    public interface ILogger 
    {
        void Logger(string functionName, string error, string stacktrace);
    }
    public class MessageLogger : ILogger
    {

        public string edocobj;
        public void Logger(string functionName, string error, string stacktrace)
        {
            MessageBox.Show("Function Name:" + " " + functionName + "\n" + "Exception:" + " " + error + "\n" + "Stack Trace:" + " " + stacktrace);
            if (edocobj == "EdocDesktopApplication")
            EdocDesktopApplication.UpdateStatus(functionName, error,"Error");
            else
            {
                Login.UpdateStatus(functionName, error, "Error");
            }
            
        }

    }

}
