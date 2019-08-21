using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace RockPaperScissor.BL
{
    public class Funciones
    {
        
        public static DataTable DataSet_To_DataTable(DataSet data)
        {
           return data.Tables[0];
        }

        public static string DataTableToJSON(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

    }
}

