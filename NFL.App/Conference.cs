using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;

public class Conference : Connection
{
    #region attributes

    private string _id, _name;
    private List<Division> _divisions = new List<Division>();
    private byte[] _logo;

    #endregion

    #region properties

    /// <summary>
    /// Conference Id
    /// </summary>
    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }
    /// <summary>
    /// Conference Name
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    /// <summary>
    /// Divisions that belong to the conference
    /// </summary>
    public List<Division> Divisions
    {
        get { return _divisions; }
        set { _divisions = value; }
    }
    /// <summary>
    /// Gets the logo in BitmapImage format
    /// </summary>
    public BitmapImage Logo
    {
        get { return ImageConverter.ByteToBitmapImage(_logo); }
    }
    /// <summary>
    /// Sets the logo from a image file's URI
    /// </summary>
    public string LogoUri
    {
        set { _logo = ImageConverter.FileToByte(value); }
    }

    #endregion

    #region constructors

    /// <summary>
    /// Creates an empty conference
    /// </summary>
    public Conference()
    {
        _id = "";
        _name = "";
        _divisions = new List<Division>();
    }
    /// <summary>
    /// Creates a conference with data
    /// </summary>
    /// <param name="id">Conference Id</param>
    public Conference(string id)
    {
        string query = @"select conf_name, conf_logo, div_id from conferences left join divisions on conf_id = div_id_conference where conf_id = @IDCONFERENCE;";
        SqlCommand command = new SqlCommand(query);
        command.Parameters.Add(new SqlParameter("@IDCONFERENCE", id));
        DataTable table = GetTable(command);
        if (table.Rows.Count > 0)
        {
            DataRow rowConference = table.Rows[0];
            _id = id;
            _name = rowConference["conf_name"].ToString();
            _logo = (byte[])rowConference["conf_logo"];
            foreach (DataRow rowDivision in table.Rows)
            {
                _divisions.Add(new Division(rowDivision["div_id"].ToString()));
            }
        }
        else
        {
            throw new RecordNotFoundException();
        }
    }

    #endregion

    #region methods

    /// <summary>
    /// Adds a new Conference
    /// </summary>
    /// <returns></returns>
    public bool Add()
    {
        string instruction = "insert into conferences (conf_id, conf_name, conf_logo) values (@ID, @NAME, @LOGO)";
        SqlCommand command = new SqlCommand(instruction);
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@NAME", _name);
        command.Parameters.AddWithValue("@LOGO", _logo);
        return ExecuteNonQuery(command);
    }

    /// <summary>
    /// Represents the object as a string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return _name;
    }
    #endregion
}
