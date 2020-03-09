using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.Handlers
{
    public static class DataMode
    {
        public static string GetPath(string input)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[input] ?? "Error";
            }
            catch (ConfigurationErrorsException)
            {
                return "Error";
            }
        }
    }
}
