using System;
using System.Data.SqlClient;

namespace progforreznik
{
    class DBConnect : ConfigCrypt
    {
        protected SqlConnection connect { get; set; }

        protected void DataConnect()
        {
            ConfigConnectionStringCrypt();
            connect = new SqlConnection(connectionString);
            connect.Open();
        }
    }
}
