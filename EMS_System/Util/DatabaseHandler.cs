using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Windows;

namespace EMS_System.Util
{
    public class DatabaseHandler
    {
        private MySqlConnection conn;

        public DatabaseHandler()
        {
            conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;user id=root;database=db_ems_bcsi;SslMode=none";
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

        public ObservableCollection<string> GetDepartment(int employee_ID)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();

            using (MySqlCommand cmd = new MySqlCommand(@"
                SELECT 
                    employee_Department
                FROM
                    tbl_employee
                WHERE
                    employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
            }

            return result;
        }

        public ObservableCollection<string> GetFunction(int employee_ID)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();

            using (MySqlCommand cmd = new MySqlCommand(@"
                SELECT
                    employee_Function
                FROM
                    tbl_employee
                WHERE
                    employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(reader[0].ToString());
                }
            }
            return result;
        }

        public ObservableCollection<ObservableCollection<string>> GetCheckin(int employee_ID)
        {
            ObservableCollection<ObservableCollection<string>> result = new ObservableCollection<ObservableCollection<string>>();
            ObservableCollection<string> checkinDates = new ObservableCollection<string>();
            ObservableCollection<string> CheckinTimes = new ObservableCollection<string>();

            using (MySqlCommand cmd = new MySqlCommand(@"
            SELECT 
                DATE_FORMAT(checkin_Date, '%d-%m-%Y'), TIME_FORMAT(checkin_Time, '%H:%i')
            FROM
                tbl_checkin
            WHERE
                employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount - 1; i+=2)
                    {
                        checkinDates.Add(reader[i].ToString());
                        CheckinTimes.Add(reader[i+1].ToString());
                    }
                }
            }
            result.Add(checkinDates);
            result.Add(CheckinTimes);

            return result;
        }

        public ObservableCollection<ObservableCollection<string>> GetCheckOut(int employee_ID)
        {
            ObservableCollection<ObservableCollection<string>> result = new ObservableCollection<ObservableCollection<string>>();
            ObservableCollection<string> checkoutDates = new ObservableCollection<string>();
            ObservableCollection<string> checkoutTimes = new ObservableCollection<string>();

            using (MySqlCommand cmd = new MySqlCommand(@"
            SELECT 
                DATE_FORMAT(checkout_Date, '%d-%m-%Y'), TIME_FORMAT(checkout_Time, '%H:%i')
            FROM
                tbl_checkout
            WHERE
                employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount - 1; i += 2)
                    {
                        checkoutDates.Add(reader[i].ToString());
                        checkoutTimes.Add(reader[i + 1].ToString());
                    }
                }
            }
            result.Add(checkoutDates);
            result.Add(checkoutTimes);

            return result;
        }

        public ObservableCollection<string> GetCheckin(int employee_ID, int month)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();

            //Wat wil ik: lijst van dagen + checkins van bepaalde maand

            using (MySqlCommand cmd = new MySqlCommand(@"
            SELECT
                DATE_FORMAT(checkin_Date, '%d-%m-%Y'), TIME_FORMAT(checkin_Time, '%H:%i')
            FROM
                tbl_Checkin  
            WHERE
                employee_ID = @employee_ID
            AND
                MONTH(checkin_Date) = @month", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                cmd.Parameters.AddWithValue("month", month);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                }
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

        public string GetEmployeeResidence(int employee_ID)
        {
            string result;

            using (MySqlCommand cmd = new MySqlCommand(@"
                SELECT 
                    employee_Residence
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
                TIME_FORMAT(operatinghours_MondayStartTime, '%H:%i'), TIME_FORMAT(operatinghours_MondayEndTime, '%H:%i'), TIME_FORMAT(operatinghours_TuesdayStartTime, '%H:%i'), TIME_FORMAT(operatinghours_TuesdayEndTime, '%H:%i'), TIME_FORMAT(operatinghours_WednesdayStartTime, '%H:%i'), TIME_FORMAT(operatinghours_WednesdayEndTime, '%H:%i'), TIME_FORMAT(operatinghours_ThursdayStartTime, '%H:%i'), TIME_FORMAT(operatinghours_ThursdayEndTime, '%H:%i'), TIME_FORMAT(operatinghours_FridayStartTime, '%H:%i'), TIME_FORMAT(operatinghours_FridayEndTime, '%H:%i')
            FROM
                tbl_operatinghours
            WHERE
                employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount - 1; i += 2)
                    {
                        result.Add(reader[i].ToString() + "-" + reader[i + 1].ToString());
                    }
                }
            }
            return result;
        }

        public bool Checkin(int employee_ID)
        {
            bool done;

            using (MySqlCommand cmd = new MySqlCommand(@"
            INSERT INTO
                tbl_checkin (checkin_Date, checkin_Time, employee_ID)
            VALUES
                (@date, @time, @employee_ID)", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("time", DateTime.Now.ToString("hh:mm:ss tt"));
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);

                try
                {
                    done = (Int64)cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception e)
                {
                    done = false;
                    MessageBox.Show(e.ToString());
                }
            }

            return done;
        }

        public bool Checkout(int employee_ID)
        {
            bool done;

            using (MySqlCommand cmd = new MySqlCommand(@"
            INSERT INTO
                tbl_checkout (checkout_Date, checkout_Time, employee_ID)
            VALUES
                (@date, @time, @employee_ID)", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("time", DateTime.Now.ToString("hh:mm:ss tt"));
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);

                try
                {
                    done = (Int64)cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception e)
                {
                    done = false;
                    MessageBox.Show(e.ToString());
                }
            }

            return done;
        }

        //public ObservableCollection<string> GetAbsence()
        //{
        //    ObservableCollection<string> result = new ObservableCollection<string>();

        //    using (MySqlCommand cmd = new MySqlCommand(@"
        //    SELECT
        //        employee_ID, DATE_FORMAT(absence_Date, '%d-%m-%Y'), absence_IsAuthorized
        //    FROM
        //        tbl_Absence", this.GetConnection()))
        //    {
        //        MySqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            for (int i = 0; i < reader.FieldCount - 2; i+=3)
        //            {
        //                result.Add(this.GetUsername(Convert.ToInt32(reader[i])) + "\t\t\t" + reader[i+1].ToString() + "\t\t\t" + reader[i+2].ToString());
        //            }
        //        }
        //    }
        //    return result;
        //}

        public List<List<string>> GetAbsence()
        {
            List<List<string>> result = new List<List<string>>();
            ObservableCollection<string> result2 = new ObservableCollection<string>();
            List<string> usernames = new List<string>();
            List<string> absencesDates = new List<string>();
            List<string> absencesAuthorized = new List<string>();

            using (MySqlCommand cmd = new MySqlCommand(@"
            SELECT
                employee_ID, DATE_FORMAT(absence_Date, '%d-%m-%Y'), absence_IsAuthorized
            FROM
                tbl_Absence", this.GetConnection()))
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount - 2; i+=3)
                    {
                        usernames.Add(reader[i].ToString());
                        absencesDates.Add(reader[i+1].ToString());
                        absencesAuthorized.Add(reader[i+2].ToString());
                    }
                }
                result.Add(usernames);
                result.Add(absencesDates);
                result.Add(absencesAuthorized);
            }
            return result;
        }

        public ObservableCollection<ObservableCollection<string>> GetAbsence(int employee_ID)
        {
            ObservableCollection<ObservableCollection<string>> result = new ObservableCollection<ObservableCollection<string>>();
            ObservableCollection<string> absenceDate = new ObservableCollection<string>();
            ObservableCollection<string> absenceAuthorization = new ObservableCollection<string>();

            using (MySqlCommand cmd = new MySqlCommand(@"
            SELECT
                DATE_FORMAT(absence_Date, '%d-%m-%Y'), absence_IsAuthorized
            FROM
                tbl_Absence
            WHERE
                employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount - 1; i+=2)
                    {
                        //result.Add(reader[i].ToString() + "\t\t" + reader[i+1].ToString());
                        absenceDate.Add(reader[i].ToString());
                        absenceAuthorization.Add(reader[i+1].ToString());
                    }
                }
                result.Add(absenceDate);
                result.Add(absenceAuthorization);
            }
            return result;
        }

        public ObservableCollection<string> GetEmployableEmployees(string unEmployableEmployee)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();

            using (MySqlCommand cmd = new MySqlCommand(@"
            SELECT 
                employee_Name
            FROM 
                tbl_employee
            WHERE
                employee_Name != @employee_Name", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_Name", unEmployableEmployee);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        result.Add(reader[i].ToString());
                    }

                }
            }
            return result;
        }

        public int CheckForAdminRights(string employee_ID)
        {
            int result; 

            using (MySqlCommand cmd = new MySqlCommand(@"
            SELECT
                employee_IsAdmin
            FROM
                tbl_employee
            WHERE
                employee_ID = @employee_ID", this.GetConnection()))
            {
                cmd.Parameters.AddWithValue("employee_ID", employee_ID);
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return result;
        }
    }
}