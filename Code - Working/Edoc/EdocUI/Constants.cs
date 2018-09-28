using EdocApp.DataModel;
using System.Configuration;

namespace EdocUI
{
    public class Constants
    {
      /*  
        public const string DEFAULT_PATH = @"D:\Temp\EDOC\DefaultRepo\";

        // Defines default path for classified documents.
        public const string CLASSIFIED_PATH = @"D:\Temp\EDOC\ClassifiedRepo\";

        // Defines default path for all archived ("deleted") documents.
        public const string ARCHIVE_PATH = @"D:\Temp\EDOC\PRO-FP-02\Users\ScanFolder\Archive\";

        // Defines the root URI for the Web API that is used to get and set application data.
        public const string API_ROOT_URI = @"http://192.168.0.27/EdocApp.Web/api/";

        // Defines the maximum number of document entries there can be without a scrollable panel.
       

        /*
         * Defines the minimum memory threshold held by Adobe Reader as a process, allowing
         * the program to kill any instances of Adobe Reader without killing the embedded reader.
         */
        public  int ADOBE_READER_MIN_MEMORY_SIZE = 20480000;

        // Defines the root URI for the Web API that is used to get and set application data.

        public static string API_ROOT_URI = ConfigurationSettings.AppSettings["API_ROOT_URI"];

        // Defines default path for all newly scanned documents (unclassified).

        public static string DEFAULT_PATH = ConfigurationSettings.AppSettings["DEFAULT_PATH"];

        // Defines default path for classified documents.

        public static string CLASSIFIED_PATH = ConfigurationSettings.AppSettings["CLASSIFIED_PATH"];

        // Defines default path for all archived ("deleted") documents.

        public static string ARCHIVE_PATH = ConfigurationSettings.AppSettings["ARCHIVE_PATH"];

        // Defines the maximum number of document entries there can be without a scrollable panel.

        public static int MAX_GRID_SIZE = 13;
        public static string FILTER_DEFAULT_TEXT = "All";
        public static string DEFAULT_COMBO_TEXT = "Select";

    }
}
