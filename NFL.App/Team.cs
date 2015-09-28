using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;

public class Team : Connection
{
    #region attributes

    private string _id, _city, _nickname;
    private byte[] _logo;

    #endregion

    #region properties

    /// <summary>
    /// Team Id
    /// </summary>
    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }
    /// <summary>
    /// Team City
    /// </summary>
    public string City
    {
        get { return _city; }
        set { _city = value; }
    }
    /// <summary>
    /// Team nickname
    /// </summary>
    public string Nickname
    {
        get { return _nickname; }
        set { _nickname = value; }
    }
    /// <summary>
    /// Gets the full team name
    /// </summary>
    public string FullName
    {
        get { return _city + " " + _nickname; }
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
    /// Creates an empty team
    /// </summary>
    public Team()
    {
        _id = "";
        _city = "";
        _nickname = "";
    }
    /// <summary>
    /// Creates a team with data
    /// </summary>
    /// <param name="id">Team Id</param>
    public Team(string id)
    {
        string query = @"select team_city, team_nickname, team_logo from teams where team_id = @ID;";
        SqlCommand command = new SqlCommand(query);
        command.Parameters.Add(new SqlParameter("@ID", id));
        DataRow row = GetFirstRow(command);
        if (row != null)
        {
            _id = id;
            _city = row["team_city"].ToString();
            _nickname = row["team_nickname"].ToString();
            _logo = (byte[])row["team_logo"];
        }
        else
        {
            throw new RecordNotFoundException();
        }
    }

    #endregion

    #region methods

    /// <summary>
    /// Adds a new Team
    /// </summary>
    /// <returns></returns>
    public bool Add(string idDivision)
    {
        string instruction = "insert into teams (team_id, team_city, team_nickname, team_logo, team_id_division) values (@ID, @CITY, @NICKNAME, @LOGO, @IDDIVISION)";
        SqlCommand command = new SqlCommand(instruction);
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@CITY", _city);
        command.Parameters.AddWithValue("@NICKNAME", _nickname);
        command.Parameters.AddWithValue("@LOGO", _logo);
        command.Parameters.AddWithValue("@IDDIVISION", idDivision);
        return ExecuteNonQuery(command);
    }
    public static List<player> GetPlayers(Team t)
    {
        List<player> list = new List<player>();
        string query = "select pla_id from players where pla_team ='" + t.Id + "'";
        SqlCommand command = new SqlCommand(query);
        DataTable table = GetTable(command);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new player((int)row["pla_id"]));
        }
        return list;
    }

    /// <summary>
    /// Represents the object as a string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return _city + " " +_nickname;
    }
    #endregion
}
