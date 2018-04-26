using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

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

        public int GetLoggedInUserID(string employee_Name, string employee_Password)
        {
            int result;

            using (MySqlCommand cmd = new MySqlCommand(@"
                SELECT
                    employee_ID
                FROM
                    tbl_employee
                WHERE BINARY
                    employee_Name = @employee_Name
                AND BINARY
                    employee_Password = @employee_Password", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_Name", employee_Name);
                cmd.Parameters.AddWithValue("employee_Password", employee_Password);
                result = (int)cmd.ExecuteScalar();
            }
            return result;
        }

        public string GetUsername(int employee_ID)
        {
            string result;

            using (MySqlCommand cmd = new MySqlCommand(@"
                SELECT 
                    employee_Name
                FROM
                    tbl_employee
                WHERE
                    employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                result = (string)cmd.ExecuteScalar();
            }
            return result;
        }

        public string GetEmployeeAddress(int employee_ID)
        {
            string result;

            using (MySqlCommand cmd = new MySqlCommand(@"
                SELECT 
                    employee_Address
                FROM
                    tbl_employee
                WHERE
                    employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                result = (string)cmd.ExecuteScalar();
            }
            return result;
        }

        public string GetEmployeeZipcode(int employee_ID)
        {
            string result;

            using (MySqlCommand cmd = new MySqlCommand(@"
                SELECT 
                    CONCAT(employee_Zipcode, ' ', employee_Residence)
                FROM
                    tbl_employee
                WHERE
                    employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                result = (string)cmd.ExecuteScalar();
            }
            return result;
        }

        public ObservableCollection<Domain.Person> SearchForEmployee(string searchTerm)
        {
            ObservableCollection<EMS_System.Domain.Person> result = new ObservableCollection<EMS_System.Domain.Person>();

            using (MySqlCommand cmd = new MySqlCommand(@"
                SELECT
                    employee_Name
                FROM
                    tbl_employee
                WHERE
                    employee_Name LIKE '%" + searchTerm + "%'", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("searchTerm", searchTerm);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result.Add(new Domain.Person { Name = reader.GetString(i) });
                    }
                }
            }
                return result;
        }

        public string GetEmployeeEmail(int employee_ID)
        {
            string result;

            using (MySqlCommand cmd = new MySqlCommand(@"
                SELECT 
                    employee_Email
                FROM
                    tbl_employee
                WHERE
                    employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                result = (string)cmd.ExecuteScalar();
            }
            return result;
        }

        public ObservableCollection<string> GetClockHours(int employee_ID)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();

            using (MySqlCommand cmd = new MySqlCommand(@"
            SELECT
                operatinghours_MondayStartTime, operatinghours_MondayEndTime, operatinghours_TuesdayStartTime, operatinghours_TuesdayEndTime, operatinghours_WednesdayStartTime, operatinghours_WednesdayEndTime, operatinghours_ThursdayStartTime, operatinghours_ThursdayEndTime, operatinghours_FridayStartTime, operatinghours_FridayEndTime
            FROM
                tbl_operatinghours
            WHERE
                employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount - 1; i+=2)
                    {
                        result.Add(reader[i].ToString() + "-" + reader[i + 1].ToString());
                    }
                }
            }
            return result;
        }
    }
}
