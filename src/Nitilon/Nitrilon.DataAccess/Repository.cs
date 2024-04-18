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
                int idFromDb = reader.GetInt32(0);
                DateTime dateFromDb = reader.GetDateTime(1);
                string nameFromDb = reader.GetString(2);
                int attendeesFromDb = reader.GetInt32(3);
                string descriptionFromDb = reader.GetString(4);
                List<Rating> ratings = new List<Rating>();
                Event e = new Event(idFromDb, dateFromDb, nameFromDb, attendeesFromDb, descriptionFromDb, ratings );
                 
                events.Add(e);
            };
                

            
                



            
            connection.Close();


            return events;

        }

        public List<Event> GetEvent(int id)
        {
            List<Event> events = new List<Event>();
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

                int idFromDb = reader.GetInt32(0);
                DateTime dateFromDb = reader.GetDateTime(1);
                string nameFromDb = reader.GetString(2);
                int attendeesFromDb = reader.GetInt32(3);
                string descriptionFromDb = reader.GetString(4);
                List<Rating> ratings = new List<Rating>();
                Event e = new Event(idFromDb, dateFromDb, nameFromDb, attendeesFromDb, descriptionFromDb, ratings);

                events.Add(e);
            }
            connection.Close();
            return events;
        }

        public List<Event> GetEventByDate(DateTime date)
        {
            List<Event> events = new List<Event>();
            string sql = $"SELECT * FROM Events WHERE Date >= '{date.ToString("yyyy-MM-dd")}'";

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
                int idFromDb = reader.GetInt32(0);
                DateTime dateFromDb = reader.GetDateTime(1);
                string nameFromDb = reader.GetString(2);
                int attendeesFromDb = reader.GetInt32(3);
                string descriptionFromDb = reader.GetString(4);
                List<Rating> ratings = new List<Rating>();
                //nice job copiletting the code
                Event e = new Event(idFromDb, dateFromDb, nameFromDb, attendeesFromDb, descriptionFromDb, ratings);

                events.Add(e);
            }
            connection.Close();
            return events;
        }



        public int Save(Event newEvent)
        {
            try
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
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return 0;
            }

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

        public List<EventRating> GetAllEventRating()
        {
            List<EventRating> eventRatings = new List<EventRating>();
            string sql = "SELECT * FROM EventRating";
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
                EventRating er = new EventRating();
                er.Id = reader.GetInt32(0);
                er.EventId = reader.GetInt32(1);
                er.RatingId = reader.GetInt32(2);
                eventRatings.Add(er);
            }
            return eventRatings;
            connection.Close();
        }

        public List<EventRating> GetEventRating(int id)
        {
            List<EventRating> eventRatings = new List<EventRating>(); 
            string sql = $"SELECT * FROM EventRating WHERE EventId = {id}";
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
                EventRating er = new EventRating();
                er.Id = reader.GetInt32(0);
                er.EventId = reader.GetInt32(1);
                er.RatingId = reader.GetInt32(2);
                eventRatings.Add(er);
            }
            return eventRatings;
            connection.Close();
        }

        public int Save(EventRating newEventRating)
        { try
            {


                int newid = 1;
                string sql = $"INSERT INTO EventRating (EventId, RatingId) VALUES ({newEventRating.EventId},{newEventRating.RatingId}); SELECT SCOPE_IDENTITY();";
                // Do that db stuff

                //1: make a sql connection object
                SqlConnection connection = new SqlConnection(connectionString);

                //2: make a sqlcommand object

                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    newid = (int)reader.GetDecimal(0);
                }
                return newid;
                connection.Close();
            }

            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return 0;
            }
        }

       


    }
}


