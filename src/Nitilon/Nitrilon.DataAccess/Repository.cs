using Microsoft.Data.SqlClient;
using Nitrilon.Entities;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Data;
namespace Nitrilon.DataAccess
{
    public class Repository
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = NitrilonDB;Integrated Security = True; Connect Timeout = 30; Encrypt=True;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";
        public List<Event> GetAllEvents()
        {
            List<Event> events = new List<Event>();

            string sql = $"SELECT * FROM Events;";

            // 1: make a SqlConnection object:
            SqlConnection connection = new SqlConnection(connectionString);

            // 2: make a SqlCommand object:
            SqlCommand command = new SqlCommand(sql, connection);

            // TODO: try catchify this:
            // 3. Open the connection:
            connection.Open();

            // 4. Execute query:
            SqlDataReader reader = command.ExecuteReader();

            // 5. Retrieve data from the data reader:
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["EventId"]);
                DateTime date = Convert.ToDateTime(reader["Date"]);
                string name = Convert.ToString(reader["Name"]);
                int attendees = Convert.ToInt32(reader["Attendees"]);
                string description = Convert.ToString(reader["Description"]);

                Event e = new(id, name, date, attendees, description);

                events.Add(e);
            }

            // 6. Close the connection when it is not needed anymore:
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
                
                Event e = new Event(idFromDb, nameFromDb, dateFromDb, attendeesFromDb, descriptionFromDb );

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
                //List<EventRatingData> ratings = new List<EventRatingData>();
                //nice job copiletting the code
                Event e = new Event(idFromDb, nameFromDb, dateFromDb,  attendeesFromDb, descriptionFromDb);

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

        //public List<EventRating> GetAllEventRating()
        //{
        //    List<EventRating> eventRatings = new List<EventRating>();
        //    string sql = "SELECT * FROM EventRating";
        //    // Do that db stuff

        //    //1: make a sql connection object
        //    SqlConnection connection = new SqlConnection(connectionString);

        //    //2: make a sqlcommand object

        //    SqlCommand command = new SqlCommand(sql, connection);

        //    //3: open the connection

        //    connection.Open();

        //    //4: Execute Query
        //    SqlDataReader reader = command.ExecuteReader();
        //    //5: Read the results
        //    while (reader.Read())
        //    {
        //        var idFromDb = reader.GetInt32(0);
        //        var eventIdFromDb = reader.GetInt32(1);
        //        var ratingIdFromDb = reader.GetInt32(2);


        //        EventRating er = new EventRating(idFromDb, eventIdFromDb, ratingIdFromDb);
               
        //        eventRatings.Add(er);
        //    }
        //    connection.Close();
        //    return eventRatings;
           
        //}

        public EventRatingData GetEventRatingDataBy(int eventId)
        {
            int badRatingCount = 0;
            int neutralRatingCount = 0;
            int goodRatingCount = 0;
            EventRatingData eventRatingData = default;

            string sql = $"EXEC CountAllowedRatingsForEvent @EventId = {eventId}";

            // 1: make a SqlConnection object:
            SqlConnection connection = new SqlConnection(connectionString);

            // 2: make a SqlCommand object:
            SqlCommand command = new SqlCommand(sql, connection);

            // TODO: try catchify this:
            // 3. Open the connection:
            connection.Open();

            // 4. Execute query:
            SqlDataReader reader = command.ExecuteReader();

            // 5. Retrieve data from the data reader:
            while (reader.Read())
            {
                badRatingCount = Convert.ToInt32(reader["RatingId3Count"]);
                neutralRatingCount = Convert.ToInt32(reader["RatingId2Count"]);
                goodRatingCount = Convert.ToInt32(reader["RatingId1Count"]);
                eventRatingData = new(badRatingCount, neutralRatingCount, goodRatingCount);
            }
            connection.Close();

            return eventRatingData;
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
                connection.Close();
                return newid;
                
            }

            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return 0;
            }
        }

        public (int, int, int) GetRatingsFor(Event ev)
        {
            // 1: make a SqlConnection object:
            SqlConnection connection = new SqlConnection(connectionString);

            // 2: make a SqlCommand object:
            SqlCommand command = new SqlCommand("CountAllowedRatingsForEvent", connection);

            connection.Open();
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@EventId", ev.Id);
            int ratingId1Count = 0, ratingId2Count = 0, ratingId3Count = 0;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    ratingId1Count = Convert.ToInt32(reader["RatingId1Count"]);
                    ratingId2Count = Convert.ToInt32(reader["RatingId2Count"]);
                    ratingId3Count = Convert.ToInt32(reader["RatingId3Count"]);

                    Console.WriteLine($"RatingId 1 count: {ratingId1Count}");
                    Console.WriteLine($"RatingId 2 count: {ratingId2Count}");
                    Console.WriteLine($"RatingId 3 count: {ratingId3Count}");
                }
                else
                {
                    Console.WriteLine("No data found for the specified EventId.");
                }
            }

            return (ratingId1Count, ratingId2Count, ratingId3Count);
        }





    }
}


