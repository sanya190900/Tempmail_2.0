using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace TempMail
{
    public class Database
    {
        private string connString;
        public Database(string connString) {
            this.connString = connString;
        }
        public void addClient(string email) {
            using (SqlConnection conn = new SqlConnection(connString)) {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Clients (email) VALUES (@email)", conn);
                cmd.Parameters.AddWithValue("@email", email);
                var reader = cmd.ExecuteReader();
                reader.Close();
            }
        }

        public void addMessage(string email, string sender, string date, string subject, string content, bool status, string idRealMsg) {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("AddMessage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@sender", sender));
                cmd.Parameters.Add(new SqlParameter("@date", date));
                cmd.Parameters.Add(new SqlParameter("@subject", subject));
                cmd.Parameters.Add(new SqlParameter("@message", content));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                cmd.Parameters.Add(new SqlParameter("@idRealMsg", idRealMsg));
                cmd.ExecuteReader();
            }
        }
    }
}