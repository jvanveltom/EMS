using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EMS_System
{
    static class DatabaseHandler
    {
        static private MySqlConnection conn;

        public static void CreateConnection()
        {
            conn = new MySqlConnection("server=localhost;user id=root;database=db_ems_bcsi");
        }
    }
}
