using Microsoft.Data.SqlClient;
using Nitrilon.Entities;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
namespace Nitrilon.DataAccess
{
    public class Repository
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = NitrilonDB;Integrated Security = True; Connect Timeout = 30; Encrypt=True;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";
        public List<Event> GetAllEvent()
        {
            List<Event> events = new List<Event>();
            string sql = "SELECT * FROM Events";
            // Do that db stuff

            //1: make a sql connection object
            SqlConnection connection = new SqlConnection(connectionString);

            //2: make a sqlcommand object

            SqlCommand command = new SqlCommand(sql, connection);

            //3: open the connection

            connection.Open();

            //4: Execute Query
            SqlDataReader reader = command.ExecuteReader();
            //5: Read the results
            while (reader.Read())
            {
                Event e = new Event();
                e.Id = reader.GetInt32(0);
                e.Date = reader.GetDateTime(1);
                e.Name = reader.GetString(2);
                e.Attendees = reader.GetInt32(3);
                e.Description = reader.GetString(4);
                events.Add(e);



            }
            connection.Close();


            return events;

        }

        public Event GetEvent(int id)
        {
            Event e = new Event();
            string sql = $"SELECT * FROM Events WHERE EventId = {id}";
            // Do that db stuff

            //1: make a sql connection object
            SqlConnection connection = new SqlConnection(connectionString);

            //2: make a sqlcommand object

            SqlCommand command = new SqlCommand(sql, connection);

            //3: open the connection

            connection.Open();

            //4: Execute Query
            SqlDataReader reader = command.ExecuteReader();
            //5: Read the results
            while (reader.Read())
            {
                e.Id = reader.GetInt32(0);
                e.Date = reader.GetDateTime(1);
                e.Name = reader.GetString(2);
                e.Attendees = reader.GetInt32(3);
                e.Description = reader.GetString(4);
            }
            connection.Close();
            return e;
        }



        public int Save(Event newEvent)
        {
                int newid = 1;
            string sql = $"INSERT INTO Events (Date, Name, Attendees, Description) VALUES ('{newEvent.Date.ToString("yyyy-MM-dd")}', '{newEvent.Name}',{newEvent.Attendees},'{newEvent.Description}'); SELECT SCOPE_IDENTITY();";
            // Do that db stuff

            //1: make a sql connection object
            SqlConnection connection = new SqlConnection(connectionString);

            //2: make a sqlcommand object

            SqlCommand command = new SqlCommand(sql, connection);

            // todo: try catch block
            //3: open the connection

            connection.Open();
            //todo: figure out how to get the id of the new record
            //4 : execute the insert command
            //command.ExecuteNonQuery();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                newid = (int)sqlDataReader.GetDecimal(0);
            }

            //5. close the connection when it is not needed anymore

            connection.Close();



            return newid;


        }
        public int Delete(int id)
        {
            string sql = $"DELETE FROM Events WHERE EventId = {id}";
            int rowsAffected = 0;
            // Do that db stuff
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Add parameter to the command
                    command.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Handle the exception here
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
                return rowsAffected;
            }

        }
        public int Update(Event updatedEvent)
        {
            string sql = $"UPDATE Events SET Date = '{updatedEvent.Date.ToString("yyyy-MM-dd")}', Name = '{updatedEvent.Name}', Attendees = {updatedEvent.Attendees}, Description = '{updatedEvent.Description}' WHERE EventId = {updatedEvent.Id}";
            int rowsAffected = 0;
            // Do that db stuff
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Add parameter to the command
                    command.Parameters.AddWithValue("@Id", updatedEvent.Id);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Handle the exception here
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
                return rowsAffected;
            }
        }


    }
}


