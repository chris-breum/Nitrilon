using Microsoft.Data.SqlClient;
using Nitrilon.Entities;
using System.Text.RegularExpressions;

namespace Nitrilon.DataAccess
{
    public class MemberRepository : Repository
    {
        public MemberRepository() : base()
        {

        }
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Regular expression for email validation
                var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public int AddMember(Member newMember)
        {
            try
            {
                int newid = 1;
               
                string sql = $"INSERT INTO Members ( Name, PhoneNumber, Email,  MembershipId) VALUES ('{newMember.Name}', '{newMember.PhoneNumber}','{newMember.Email}','{newMember.Membership.MembershipId}'); SELECT SCOPE_IDENTITY();";
                

                // Do that db stuff

               
                SqlDataReader sqlDataReader = Execute(sql);
                while (sqlDataReader.Read())
                {
                    newid = (int)sqlDataReader.GetDecimal(0);
                }

                // close the connection when it is not needed anymore

                CloseConnection();

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
            List<Member> member = new();

            string sql = $"SELECT        dbo.Members.*, dbo.MembershipTypes.membershipType, dbo.MembershipTypes.Description\r\nFROM            dbo.Members INNER JOIN\r\n       dbo.MembershipTypes ON dbo.Members.MembershipId = dbo.MembershipTypes.MembershipId";

            

            //  Execute query:
            SqlDataReader reader = Execute(sql);

            // 5. Retrieve data from the data reader:
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["MemberId"]);
                string name = Convert.ToString(reader["Name"]);
                string phoneNumber = Convert.ToString(reader["PhoneNumber"]);
                string email = Convert.ToString(reader["Email"]);
                DateTime date = Convert.ToDateTime(reader["Date"]);
                Membership membership = new (Convert.ToInt32(reader["MembershipId"]), Convert.ToString(reader["MembershipType"]), Convert.ToString(reader["Description"]));

                Member m = new(id, name, phoneNumber, email, date, membership);

                member.Add(m);
            }

            // 6. Close the connection when it is not needed anymore:
            CloseConnection();

            return member;
        }

        public List<Member> GetMember(string name)
        {
            List<Member> member = new ();
            string sql = $"SELECT        dbo.Members.*, dbo.MembershipTypes.membershipType, dbo.MembershipTypes.Description\r\nFROM            dbo.Members INNER JOIN\r\n       dbo.MembershipTypes ON dbo.Members.MembershipId = dbo.MembershipTypes.MembershipId WHERE Name = {name}";
            // Do that db stuff

            

            // Execute Query
            SqlDataReader reader = Execute(sql);
            // Read the results
            while (reader.Read())
            {

                int idFromDb = reader.GetInt32(0);
                string nameFromDb = reader.GetString(1);
                string phoneNumberFromDb = reader.GetString(2);
                string emailFromDb = reader.GetString(3);
                DateTime dateFromDb = reader.GetDateTime(4);
                Membership membership = new (Convert.ToInt32(reader["MembershipId"]), Convert.ToString(reader["MembershipType"]), Convert.ToString(reader["Description"]));

                Member m = new (idFromDb, nameFromDb, phoneNumberFromDb, emailFromDb, dateFromDb, membership);

                member.Add(m);
            }
            CloseConnection();
            return member;
        }
        public int DeleteMember(int id)
        {
            string sql = $"DELETE FROM Members WHERE MemberId = {id}";
            int rowsAffected = 0;
            // Do that db stuff
            using SqlConnection connection = new(connectionString);
            using (SqlCommand command = new(sql, connection))
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
        public List<Membership> GetMembershipTypes()
        {
            List<Membership> membership = new();
            string sql = $"SELECT * FROM MembershipTypes";
            // Do that db stuff

            // Execute Query
            SqlDataReader reader = Execute(sql);
            // Read the results
            while (reader.Read())
            {
                int membershipId = Convert.ToInt32(reader["MembershipId"]);
                string membershipType = Convert.ToString(reader["MembershipType"]);
                string description = Convert.ToString(reader["Description"]);

                Membership m = new(membershipId, membershipType, description);

                membership.Add(m);
            }
            CloseConnection();
            return membership;
        }
    }
}


