using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;

 public class player : Connection
    {
        #region attributes

        private DateTime _dateOfBirth;
        private string _firstName, _lastName;
        private int _id, _heightInInches, _weightInPounds;
        private string _team;
        private byte[] _logo;
    

        #endregion

        #region properties

        /// <summary>
        /// person's years
        /// </summary>
        ///  public string Team
        public string Team
        {
            get { return _team; }
            set { _team = value; }
        }

        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - _dateOfBirth.Year;
                if (_dateOfBirth.Month > today.Month | (_dateOfBirth.Month == today.Month & _dateOfBirth.Day > today.Day))
                    age--;
                return age;
            }
        }

        /// <summary>
        /// person's first name
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        /// <summary>
        /// person's last name
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
       
        /// <summary>
        /// person's full name
        /// </summary>
        public string FullName
        {
            get { return _firstName + " " + _lastName; }
        }

        /// <summary>
        /// person's birth
        /// </summary>
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        /// <summary>
        /// person's weight in pounds
        /// </summary>
        public int WeightInPounds
        {
            get { return _weightInPounds; }
            set { _weightInPounds = value; }
        }

        /// <summary>
        /// person's height in inches
        /// </summary>
        public int HeightInInches
        {
            get { return _heightInInches; }
            set { _heightInInches = value; }
        }


        public string HeightInFeet
        {
            get
            {
                
                int feet, feetconver = 0;
                feet = (int)_heightInInches / 12;
                feetconver = (int)_heightInInches % 12;
                return feet + "'" + feetconver + "\"";

            }
        }

        public string HeightInMeters
        {
            get
            {
                double meters = 0;
                meters = _heightInInches / 39.370;
                return meters.ToString("n2");
            }
        }

        public double WeightInKilograms
        {
            get
            {
                double kilograms = 0;
                kilograms = _weightInPounds / 2.2046;
                return kilograms;
            }
        }


        /// <summary>
        /// person's id
        /// </summary>
        public int Id
        {
            get { return _id; }
        }

        public BitmapImage Logo
        {
            get { return ImageConverter.ByteToBitmapImage(_logo); }
        }
        public string LogoUri
        {
            set { _logo = ImageConverter.FileToByte(value); }
        }

        #endregion
      
        #region constructors

        public player()
        {
            _id = 0;
            _dateOfBirth = new DateTime();
            _firstName ="";
            _lastName = "";
            _heightInInches = 0;
            _weightInPounds = 0;
        }

        public player(int id)
        {
            //query
            string query = "select pla_first_name, pla_last_name, pla_date_of_birth, pla_height_inches, imagen, pla_weight_pounds from players where pla_id=@ID;";
            //sql command
            SqlCommand command = new SqlCommand(query);
            //parameters
            command.Parameters.Add(new SqlParameter("@ID", id));
            //execute command
            DataRow row = GetFirstRow(command);
            //chek if row has data
            if (row != null)
            {
                _id = id;
                _firstName = row["pla_first_name"].ToString();
                _lastName = row["pla_last_name"].ToString();
                _dateOfBirth = (DateTime)row["pla_date_of_birth"];
                _heightInInches = (int)row["pla_height_inches"];
                _logo = (byte[])row["imagen"];
                _weightInPounds = (int)row["pla_weight_pounds"];
                //_balance = (double)row["acc_balance"];
            }
            else
            {
                _id = 0;
                _firstName = "";
                _lastName = "";
                _dateOfBirth = new DateTime();
                _heightInInches = 0;
                _weightInPounds = 0;
            }
        }

        #endregion

        #region methods

        public bool Add()
        {
            string insert = "insert into players (pla_first_name, pla_last_name, pla_date_of_birth, pla_height_inches, pla_weight_pounds, imagen, pla_team) values (@pla_first_name,@pla_last_name,@pla_date_of_birth,@pla_height_inches,@pla_weight_pounds, @imagen,@pla_team)";
            //command

            SqlCommand command = new SqlCommand(insert);
            //parameters
            command.Parameters.Add(new SqlParameter("@pla_first_name", _firstName));
            command.Parameters.Add(new SqlParameter("@pla_last_name", _lastName));
            command.Parameters.Add(new SqlParameter("@pla_date_of_birth", _dateOfBirth));
            command.Parameters.Add(new SqlParameter("@pla_height_inches", _heightInInches));
            command.Parameters.Add(new SqlParameter("@pla_weight_pounds", _weightInPounds));
            command.Parameters.AddWithValue("@imagen", _logo);
            command.Parameters.Add(new SqlParameter("@pla_team", _team));
            //execute command
            return ExecuteNonQuery(command);
        
        }

        public bool Delete()
        {

            string delete = "delete from players where pla_id = @pla_id";
            //command
            SqlCommand command = new SqlCommand(delete);
            //parameters
            command.Parameters.Add(new SqlParameter("@pla_id",_id));
            //execute command
            return ExecuteNonQuery(command);

            
        
        }

        public bool Update()
        {
            string update = "update players set pla_first_name = @pla_first_name,pla_last_name=@pla_last_name,pla_date_of_birth=@pla_date_of_birth,pla_height_inches=@pla_height_inches,pla_weight_pounds=@pla_weight_pounds where pla_id=@pla_id";
            //command
            SqlCommand command = new SqlCommand(update);
            //parameters
            command.Parameters.Add(new SqlParameter("@pla_id",_id));
            command.Parameters.Add(new SqlParameter("@pla_first_name", _firstName));
            command.Parameters.Add(new SqlParameter("@pla_last_name", _lastName));
            command.Parameters.Add(new SqlParameter("@pla_date_of_birth", _dateOfBirth));
            command.Parameters.Add(new SqlParameter("@pla_height_inches", _heightInInches));
            command.Parameters.Add(new SqlParameter("@pla_weight_pounds", _weightInPounds));
            //execute command
            return ExecuteNonQuery(command);
        
        }


        #endregion
    }

