using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class Connection
{
    #region atributtes
    //connection string
    private static string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString; 

    //connection
    private static SqlConnection connection = new SqlConnection();
    #endregion

    #region methods
    /// <summary>
    /// Opens a connection to SQL Server
    /// </summary>
    /// <returns></returns>
    private static bool OpenConnection()
    {
        bool opened = false;//connection not openend
        connection.ConnectionString = connectionString;//asign connection string       
        try
        {
            connection.Open();//open connection
            opened = true;//connection opened
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex);
        }
        return opened; //return result
    }

    /// <summary>
    /// Executes query and return result table
    /// </summary>
    /// <param name="command">SQL Command</param>
    /// <returns></returns>
    protected static DataTable GetTable(SqlCommand command)
    {
        DataTable table = new DataTable(); //data table
        //open connection
        if (OpenConnection())
        {
            command.Connection = connection; //asign connection 
            //adapter
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                //execute query and fill table
                adapter.Fill(table);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            //close connection
            connection.Close();
        }
        return table;//return result
    }

    /// <summary>
    /// Gets the first row of a table
    /// </summary>
    /// <param name="command">SQL Command</param>
    /// <returns></returns>
    protected static DataRow GetFirstRow(SqlCommand command)
    {
        DataRow row = null; //empty row
        //execute query
        DataTable table = GetTable(command);
        //check if table has rows
        if (table.Rows.Count > 0)
        {
            row = table.Rows[0];//get first row
        }
        return row;//return result
    }

    /// <summary>
    /// Executes a non query command
    /// </summary>
    /// <param name="command">SQL command</param>
    /// <returns></returns>
    protected static bool ExecuteNonQuery(SqlCommand command)
    {
        bool executed = false;
        if (OpenConnection())
        {
            command.Connection = connection; //assign connection to command
            try
            {
                //execute command
                command.ExecuteNonQuery();
                //command excuted
                executed = true;
            }
            catch (SqlException ex)
            {
            }
            connection.Close();
        }
        return executed;//return result
    }

    /// <summary>
    /// Executes a stored procedures
    /// </summary>
    /// <param name="command">SQL command</param>
    /// <returns></returns>
    protected static bool ExcecuteProcedure(SqlCommand command)
    {
        //command is a stored procedure
        command.CommandType = CommandType.StoredProcedure;
        //execute procedure
        return ExecuteNonQuery(command);
    }

    #endregion

}
