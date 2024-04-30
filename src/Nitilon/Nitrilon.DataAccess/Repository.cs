using Microsoft.Data.SqlClient;
using Nitrilon.Entities;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Data;
namespace Nitrilon.DataAccess
{
    /// <summary>
    /// Represents a repository for accessing and manipulating events data.
    /// </summary>
    public class Repository
    {
      
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = NitrilonDB;Integrated Security = True; Connect Timeout = 30; Encrypt=True;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";

        /// <summary>
        /// Retrieves all events from the database.
        /// </summary>
        /// <returns>A list of events.</returns>
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

        /// <summary>
        /// Retrieves a specific event from the database based on the event ID.
        /// </summary>
        /// <param name="id">The ID of the event to retrieve.</param>
        /// <returns>The event with the specified ID.</returns>
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

                Event e = new Event(idFromDb, nameFromDb, dateFromDb, attendeesFromDb, descriptionFromDb);

                events.Add(e);
            }
            connection.Close();
            return events;
        }

        /// <summary>
        /// Retrieves events from the database based on the specified date or later.
        /// </summary>
        /// <param name="date">The date to filter events.</param>
        /// <returns>A list of events.</returns>
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
                Event e = new Event(idFromDb, nameFromDb, dateFromDb, attendeesFromDb, descriptionFromDb);

                events.Add(e);
            }
            connection.Close();
            return events;
        }

        /// <summary>
        /// Saves a new event to the database.
        /// </summary>
        /// <param name="newEvent">The event to save.</param>
        /// <returns>The ID of the newly saved event.</returns>
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

        /// <summary>
        /// Deletes an event from the database based on the event ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>The number of rows affected.</returns>
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

        /// <summary>
        /// Updates an existing event in the database.
        /// </summary>
        /// <param name="updatedEvent">The updated event.</param>
        /// <returns>The number of rows affected.</returns>
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

        /// <summary>
        /// Retrieves the event rating data for a specific event.
        /// </summary>
        /// <param name="eventId">The ID of the event.</param>
        /// <returns>The event rating data.</returns>
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

        /// <summary>
        /// Saves a new event rating to the database.
        /// </summary>
        /// <param name="newEventRating">The event rating to save.</param>
        /// <returns>The ID of the newly saved event rating.</returns>
        public int Save(EventRating newEventRating)
        {
            try
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

        /// <summary>
        /// Retrieves the ratings count for a specific event.
        /// </summary>
        /// <param name="ev">The event to retrieve ratings count for.</param>
        /// <returns>A tuple containing the counts of each rating.</returns>
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


        // member


        public int AddMember(Member newMember)
        {
            try
            {
                int newid = 1;
                string sql = $"INSERT INTO Members ( Name, PhoneNumber, Email, Date, MembershipId) VALUES ('{newMember.Name}', '{newMember.PhoneNumber}', '{newMember.Date.ToString("yyyy-MM-dd")}','{newMember.Membership}'); SELECT SCOPE_IDENTITY();";
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

        public List<Member> GetAllMembers()
        {
            List<Member> member = new List<Member>();

            string sql = $"SELECT * FROM Members;";

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
                int id = Convert.ToInt32(reader["MemberId"]);
                string name = Convert.ToString(reader["Name"]);
                string phoneNumber = Convert.ToString(reader["PhoneNumber"]);
                DateTime date = Convert.ToDateTime(reader["Date"]);
                string email = Convert.ToString(reader["Email"]);
                Membership membership = new Membership(Convert.ToInt32(reader["MembershipId"]), "MembershipType", "Description");



                Member e = new Member(id, name, phoneNumber, email, date, membership);

                member.Add(e);
            }

            // 6. Close the connection when it is not needed anymore:
            connection.Close();

            return member;
        }
    }
}


