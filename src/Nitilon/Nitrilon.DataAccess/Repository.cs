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
