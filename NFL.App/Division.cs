using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public class Division : Connection
{
    #region attributes

    private string _id, _name;
    private List<Team> _teams = new List<Team>();

    #endregion

    #region proporties

    /// <summary>
    /// Division Id
    /// </summary>
    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }
    /// <summary>
    /// Divison Name
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    /// <summary>
    /// Teams that belong to the division
    /// </summary>
    public List<Team> Teams
    {
        get { return _teams; }
        set { _teams = value; }
    }
    #endregion

    #region constructors

    /// <summary>
    /// Creates empty division
    /// </summary>
    public Division()
    {
        _id = "";
        _name = "";
    }
    /// <summary>
    /// Creates division with data
    /// </summary>
    /// <param name="id"></param>
    public Division(string id)
    {
        string query = @"select div_name, team_id from divisions left join teams on div_id = team_id_division where div_id = @IDDIVISION;";
        SqlCommand command = new SqlCommand(query);
        command.Parameters.Add(new SqlParameter("@IDDIVISION", id));
        DataTable table = GetTable(command);
        if (table.Rows.Count > 0)
        {
            DataRow rowDivision = table.Rows[0];
            _id = id;
            _name = rowDivision["div_name"].ToString();
            foreach (DataRow rowTeam in table.Rows)
            {
                _teams.Add(new Team(rowTeam["team_id"].ToString()));
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
    /// Represents the object as string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return _name;
    }

    #endregion
}
