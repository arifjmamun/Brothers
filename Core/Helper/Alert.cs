using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class Alert
    {
        public static string SuccessClass = "alert alert-success alert-dismissable";
        public static string InfoClass = "alert alert-info alert-dismissable";
        public static string WarningClass = "alert alert-warning alert-dismissable";
        public static string ErrorClass = "alert alert-danger alert-dismissable";

        public static string SuccessText = "Success!";
        public static string InfoText = "Info!";
        public static string WarningText = "Warning!";
        public static string ErrorText = "Error!";

        public string CssClass { get; set; }
        public string Type { get; set; }
        public string Msg { get; set; }
        public bool Flag { get; set; }
    }
}
