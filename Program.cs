using System;
using System.IO;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace MemeTeamPro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Database databaseObject = new Database();

            Console.ReadKey();*/

            SQLiteConnection.CreateFile("MyDatabase.sqlite");
            Console.WriteLine("Created a database!");

            // Open a database connection
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            // Create a table
            string sql = "CREATE TABLE Highscores (name TEXT, score INTEGER)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            // Create a table for CRUD
            string sql1 = "CREATE TABLE User(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "FirstName TEXT NOT NULL, LastName TEXT NOT NULL)";
            SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
            command1.ExecuteNonQuery();


            // Insert dummy data into table scores
            sql = "INSERT INTO Highscores (name, score) VALUES ('Me', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "INSERT INTO Highscores (name, score) VALUES ('Myself', 6000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "INSERT INTO Highscores (name, score) VALUES ('And I', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();




            // Read the data from the database
            sql = "SELECT * FROM User ORDER BY Id DESC";
            command1 = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                Console.WriteLine("Id: " + reader1["Id"] + "\tFirstName: " + reader1["FirstName"] + "\tLastName: " + reader1["LastName"]);
            }

            // Read the data from the database
            sql = "SELECT * FROM Highscores ORDER BY score DESC";
            command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            }

            // Close the connection
            m_dbConnection.Close();

        }
    }
}
