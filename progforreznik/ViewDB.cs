using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progforreznik
{
    class ViewDB : DBConnect
    {
        SqlCommand cmd { get; set; }
        string strCommand { get; set; }
        public ViewDB()
        {
            DataConnect();
        }

        //SELECT
        public int View()
        {
            return 0;
        }

        //DELETE, INSERT, UPDATE
        public async Task<int> Edit(string command, string[] param)
        {
            strCommand = command;
            cmd = new SqlCommand(strCommand, connect);
            return await cmd.ExecuteNonQueryAsync();
        }


    }
}
