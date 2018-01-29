using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace My_Shop_MVC.App_Code
{
    public class JesusChrist
    {
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }
    }
}