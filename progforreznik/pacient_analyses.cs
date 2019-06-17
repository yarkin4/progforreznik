using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace progforreznik
{
    public partial class pacient_analyses : Form
    {
        bool[] Index;
        string[] Row;
        SqlConnection sqlConnection;
        public pacient_analyses(bool[] index, string[] row, SqlConnection connect)
        {
            InitializeComponent();
            Index = index;
            Row = row;
            sqlConnection = connect;
            InitializeTextBox();
        }

        private void InitializeTextBox()
        {
            textBox4.Text = Row[0];
            textBox9.Text = Row[1];
            textBox10.Text = Row[2];
            textBox5.Text = Row[3];
            textBox6.Text = Row[4];
            textBox8.Text = Row[5];
        }
    }
}
