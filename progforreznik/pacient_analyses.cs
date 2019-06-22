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
        
        string[] Row;
        SqlConnection sqlConnection;
        public pacient_analyses( string[] row, SqlConnection connect)
        {
            InitializeComponent();
            
            Row = row;
            sqlConnection = connect;
            InitializeTextBox();
        }

        private async void InitializeTextBox()
        {
            textBox4.Text = Row[0];
            textBox9.Text = Row[1];
            textBox10.Text = Row[2];
            textBox5.Text = Row[3];
            textBox6.Text = Row[4];
            textBox8.Text = Row[5];

            SqlDataReader sqlReader = null;
            /////////////////////////Обследования
            sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM Obsled ;", sqlConnection);

            try
            {
                //Выполнение команды и получение результата в переменную sqlReader
                sqlReader = await command.ExecuteReaderAsync();
                //Цикл заполнения 
                while (await sqlReader.ReadAsync())
                {
                    checkedListBox1.Items.Add(Convert.ToString(sqlReader["KName"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            object[] cheked = new object[checkedListBox1.CheckedItems.Count];

            checkedListBox1.CheckedItems.CopyTo(cheked, 0);
            //checkedListBox1.CheckedIndices.CopyTo(cheked, 0);
            SqlCommand command;
            SqlDataReader sqlReader = null;

            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                command = new SqlCommand(
                "SELECT Id FROM Obsled WHERE KName=@kname ;"
                , sqlConnection);
                string str = Convert.ToString(cheked[i]);
                command.Parameters.AddWithValue("kname", str);
                sqlReader = await command.ExecuteReaderAsync();
                await sqlReader.ReadAsync();
                string id = Convert.ToString(sqlReader["Id"]);
                sqlReader.Close();
                command = new SqlCommand(
                "INSERT INTO Analys(Id_obsled, Id_pacient) VALUES (@id_obsled, @id_pacient);"
                , sqlConnection);

                command.Parameters.AddWithValue("id_obsled", Convert.ToInt32(id));
                command.Parameters.AddWithValue("id_pacient", Convert.ToInt32(Row[6]));

                //textBox1.Text += cheked[i]
                await command.ExecuteNonQueryAsync();
            }

            command = new SqlCommand(
                "SELECT Id_obsled,Id_pacient FROM Analys;"
                , sqlConnection);

            sqlReader = await command.ExecuteReaderAsync();
            while (await sqlReader.ReadAsync())
                textBox1.Text = Convert.ToString(sqlReader["Id_obsled"] + " : "+ Convert.ToString(sqlReader["Id_pacient"]));
            sqlReader.Close();
        }
    }
}
