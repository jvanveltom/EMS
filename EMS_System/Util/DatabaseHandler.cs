using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EMS_System.Util
{
    public class DatabaseHandler
    {
        private MySqlConnection conn;

        public DatabaseHandler()
        {
            conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;user id=root;database=db_ems_bcsi";
        }

        public DatabaseHandler(string connectionString)
        {
            conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
        }

        public void OpenConnection()
        {
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public MySqlConnection GetConnection()
        {
            return this.conn;
        }

        public bool Login(string employee_Name, string employee_Password)
        {
            bool exist = false;

            using (MySqlCommand cmd = new MySqlCommand(@"
                SELECT 
                    COUNT(*)
                FROM
                    tbl_employee
                WHERE BINARY 
                    employee_Name = @employee_Name
                AND BINARY
                    employee_Password = @employee_Password", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_Name", employee_Name);
                cmd.Parameters.AddWithValue("employee_Password", employee_Password);
                exist = (Int64)cmd.ExecuteScalar() > 0;
            }
            return exist;
        }

    }
}
