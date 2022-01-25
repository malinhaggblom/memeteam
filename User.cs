using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace MemeTeamPro
{
    internal class User
    {
        public SQLiteConnection m_dbConnection;

        private IEnumerable<KeyValuePair<string, object>> args;
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public object Id { get; private set; }
        public User(string FirstName, string LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        //query execution; inserting, updating or removing from database
        //first method
        private int ExecuteWrite(string query, Dictionary<string, object> args)
        {
            int numberOfRowsAffected;

            //setup the connection to the database
            using (var m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                m_dbConnection.Open();
               

                //open a new command
                using (var cmd = new SQLiteCommand(query, m_dbConnection))
                {
                    //set the arguments given in the query
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }

                    //execute the query and get the number of row affected
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }

                return numberOfRowsAffected;
            }
        }

        //query execution; reading from database
        //second method
        private DataTable Execute(string query)
        {
            if (string.IsNullOrEmpty(query.Trim()))
                return null;

            using (var m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                // Open a database connection
                m_dbConnection.Open();
                using (var cmd = new SQLiteCommand(query, m_dbConnection))
                {
                    foreach (KeyValuePair<string, object> entry in args)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }

                    var da = new SQLiteDataAdapter(cmd);

                    var dt = new DataTable();
                    da.Fill(dt);

                    da.Dispose();
                    return dt;
                }
            }
        }

        //CRUD methods
        //adding user
        private int AddUser(User user)
        {
            const string query = "INSERT INTO User(FirstName, LastName) VALUES(@firstName, @lastName)";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@firstName", user.FirstName},
                {"@lastName", user.LastName}
            };

            return ExecuteWrite(query, args);
        }

        //CRUD method; editing user
        private int EditUser(User user)
        {
            const string query = "UPDATE User SET FirstName = @firstName, LastName = @lastName WHERE Id = @id";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@id", user.Id},
                {"@firstName", user.FirstName},
                {"@lastName", user.LastName}
            };

                return ExecuteWrite(query, args);
        }

        //CRUD Method; deleting user
        private int DeleteUser(User user)
        {
            const string query = "Delete from User WHERE Id = @id";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@id", user.Id}
            };

            return ExecuteWrite(query, args);
        }

        /*//CRUD Method; adding user by id
        private User GetUserById(int id)
        {
            var query = "SELECT * FROM User WHERE Id = @id";

            var args = new Dictionary<string, object>
            {
                {"@id", id}
            };

               DataTable dt = ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            var user = new User
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                FirstName = Convert.ToString(dt.Rows[0]["FirstName"]),
                LastName = Convert.ToString(dt.Rows[0]["LastName"])
            };

            return user;
        }//*/


    }
}
