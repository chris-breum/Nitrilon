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
                //Event e = new Event();
                //e.Id = reader.GetInt32(0);
                //e.Date = reader.GetDateTime(1);
                //e.Name = reader.GetString(2);
                //e.Attendees = reader.GetInt32(3);
                //e.Description = reader.GetString(4);
                //events.Add(e);

                int id = Convert.ToInt32(reader["EventId"]);
                DateTime date = Convert.ToDateTime(reader["Date"]);
                string name = reader["Name"].ToString();
                int attendees = Convert.ToInt32(reader["Attendees"]);
                string description = reader["Description"].ToString();


                Event e = new()
                {
                    Id = id,
                    Date = date,
                    Name = name,
                    Attendees = attendees,
                    Description = description;

            }
            connection.Close();


            return events;

        }









        public int Save(Event newEvent)
        {
            string sql = $"INSERT INTO Events (Date, Name, Attendees, Description) " +
              $"VALUES ('{newEvent.Date.ToString("yyyy-MM-dd")}', '{newEvent.Name}',{newEvent.Attendees},'{newEvent.Description}')";
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
            command.ExecuteNonQuery();

            //5. close the connection when it is not needed anymore

            connection.Close();



            return -1;


        }

    }
}

