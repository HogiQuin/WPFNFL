using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public class Catalogs : Connection
{
    public static List<Conference> GetConferences()
    {
        List<Conference> list = new List<Conference>();
        string query = "select conf_id from conferences";
        SqlCommand command = new SqlCommand(query);
        DataTable table = GetTable(command);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new Conference(row["conf_id"].ToString()));
        }
        return list;
    }
}
