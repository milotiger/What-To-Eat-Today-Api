using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;


namespace PullFromFoody
{
    class Writer
    {
        public static void DatatoJson(Object Items, String ExportPath)
        {
            string Json = JsonConvert.SerializeObject(Items, Formatting.Indented);
            System.IO.File.WriteAllText(ExportPath, Json);
        }
    }
}
