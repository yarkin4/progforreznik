using System;
using System.Configuration;
using System.Collections.Specialized;
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
    
    public partial class Form1 : Form
    {
        public int reg1;
        SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["progforreznik.Properties.Settings.DatabaseConnectionString"].ConnectionString;

            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            LoadData();
        }

        private async void button5_Click(object sender, EventArgs e)
        {

            SqlCommand command = new SqlCommand(
                "INSERT INTO Pacient(FName, LName, OName, Birsday, Pol, Date_tb, Comment) VALUES (@FName, @LName, @OName, @Birsday, @Pol, @Date_tb, @Comment);"

                , sqlConnection);

            command.Parameters.AddWithValue("FName", textBox4.Text);
            command.Parameters.AddWithValue("LName", textBox9.Text);
            command.Parameters.AddWithValue("OName", textBox10.Text);
            command.Parameters.AddWithValue("Birsday", textBox6.Text);
            command.Parameters.AddWithValue("Pol", textBox5.Text);
            command.Parameters.AddWithValue("Date_tb", DateTime.Now.ToString("dd MMMM yyyy"));
            command.Parameters.AddWithValue("Comment", textBox8.Text);

            await command.ExecuteNonQueryAsync();

        }

        //Закрытие приложения
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();

            SqlDataReader sqlReader = null;
            //////////////////// Пациенты


            SqlCommand command = new SqlCommand("SELECT * FROM Pacient ORDER BY id DESC;", sqlConnection);

            try
            {
                //Выполнение команды и получение результата в переменную sqlReader
                sqlReader = await command.ExecuteReaderAsync();
                //Цикл заполнения 
                while (await sqlReader.ReadAsync())
                {
                    //Добавление элемента в ЛистБокс. Конвертируем в строку и обращаемся к индексатору элемента sqlReader, куда указываем имя столбца.
                    //listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "    " + Convert.ToString(sqlReader["Name"]) + "    " + Convert.ToString(sqlReader["Money"]));
                    dataGridView1.Rows.Add();
                    dataGridView1["Column2", dataGridView1.Rows.Count - 1].Value = Convert.ToString(sqlReader["Id"]);
                    dataGridView1["c1", dataGridView1.Rows.Count - 1].Value = Convert.ToString(sqlReader["FName"]);
                    dataGridView1["c2", dataGridView1.Rows.Count - 1].Value = Convert.ToString(sqlReader["LName"]);
                    dataGridView1["c3", dataGridView1.Rows.Count - 1].Value = Convert.ToString(sqlReader["OName"]);
                    dataGridView1["Column4", dataGridView1.Rows.Count - 1].Value = Convert.ToString(sqlReader["Pol"]);
                    dataGridView1["Column5", dataGridView1.Rows.Count - 1].Value = Convert.ToString(sqlReader["Birsday"]);
                    dataGridView1["Column8", dataGridView1.Rows.Count - 1].Value = Convert.ToString(sqlReader["Date_tb"]);
                    //dataGridView1["Column6", dataGridView1.Rows.Count - 1].Value = Convert.ToString(sqlReader["Id"]);
                    dataGridView1["Column7", dataGridView1.Rows.Count - 1].Value = Convert.ToString(sqlReader["Comment"]);

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
            ///////////////////////// t-системы
            sqlReader = null;

            command = new SqlCommand("SELECT * FROM T_system ;", sqlConnection);

            try
            {
                //Выполнение команды и получение результата в переменную sqlReader
                sqlReader = await command.ExecuteReaderAsync();
                //Цикл заполнения 
                while (await sqlReader.ReadAsync())
                {
                    dataGridView2.Rows.Add();
                    dataGridView2["Column3", dataGridView2.Rows.Count -1].Value = Convert.ToString(sqlReader["Id"]);
                    dataGridView2["Column9", dataGridView2.Rows.Count -1].Value = Convert.ToString(sqlReader["Name"]);
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
            /////////////////////////Ед.изм
            sqlReader = null;

            command = new SqlCommand("SELECT * FROM Ed ;", sqlConnection);

            try
            {
                //Выполнение команды и получение результата в переменную sqlReader
                sqlReader = await command.ExecuteReaderAsync();
                //Цикл заполнения 
                while (await sqlReader.ReadAsync())
                {
                    dataGridView4.Rows.Add();
                    dataGridView4["Column12", dataGridView4.Rows.Count - 1].Value = Convert.ToString(sqlReader["Id"]);
                    dataGridView4["Column13", dataGridView4.Rows.Count - 1].Value = Convert.ToString(sqlReader["Name"]);


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

            /////////////////////////Обследования
            sqlReader = null;

            command = new SqlCommand("SELECT * FROM Obsled ;", sqlConnection);

            try
            {
                //Выполнение команды и получение результата в переменную sqlReader
                sqlReader = await command.ExecuteReaderAsync();
                //Цикл заполнения 
                while (await sqlReader.ReadAsync())
                {
                   
                    dataGridView5.Rows.Add();
                    dataGridView5["Column19", dataGridView5.Rows.Count - 1].Value = Convert.ToString(sqlReader["Id"]);
                    dataGridView5["Column15", dataGridView5.Rows.Count - 1].Value = Convert.ToString(sqlReader["KName"]);
                    dataGridView5["Column16", dataGridView5.Rows.Count - 1].Value = Convert.ToString(sqlReader["FName"]);
                    dataGridView5["Column17", dataGridView5.Rows.Count - 1].Value = Convert.ToString(sqlReader["Norm_ot"]);
                    dataGridView5["Column18", dataGridView5.Rows.Count - 1].Value = Convert.ToString(sqlReader["Norm_do"]);

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

            /////////////////////////Врачи
            sqlReader = null;

            command = new SqlCommand("SELECT * FROM Doctor;", sqlConnection);

            try
            {
                //Выполнение команды и получение результата в переменную sqlReader
                sqlReader = await command.ExecuteReaderAsync();
                //Цикл заполнения 
                while (await sqlReader.ReadAsync())
                {
                    dataGridView3.Rows.Add();
                    dataGridView3["Column21", dataGridView3.Rows.Count - 1].Value = Convert.ToString(sqlReader["Id"]);
                    dataGridView3["Column22", dataGridView3.Rows.Count - 1].Value = Convert.ToString(sqlReader["FIO"]);


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

        private void button6_Click(object sender, EventArgs e)
        {
            /*
            if (monthCalendar1.Visible)
                monthCalendar1.Visible = false;
            else
                monthCalendar1.Visible = true;
                */
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                "INSERT INTO T_system(Name) VALUES (@Name);"

                , sqlConnection);

            command.Parameters.AddWithValue("Name", textBox2.Text);
            

            await command.ExecuteNonQueryAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                "INSERT INTO Ed(Name) VALUES (@Name);"

                , sqlConnection);

            command.Parameters.AddWithValue("Name", textBox1.Text);


            await command.ExecuteNonQueryAsync();
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                "INSERT INTO Obsled(KName, FName, Norm_ot, Norm_do) VALUES (@KName, @FName, @Norm_ot, @Norm_do);"

                , sqlConnection);

            command.Parameters.AddWithValue("KName", textBox11.Text);
            command.Parameters.AddWithValue("FName", textBox12.Text);
            command.Parameters.AddWithValue("Norm_ot", Convert.ToDouble(textBox13.Text));
            command.Parameters.AddWithValue("Norm_do", Convert.ToDouble(textBox14.Text));


            await command.ExecuteNonQueryAsync();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                "INSERT INTO Doctor(FIO) VALUES (@FIO);"

                , sqlConnection);

            command.Parameters.AddWithValue("FIO", textBox3.Text);
            


            await command.ExecuteNonQueryAsync();
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            bool[] index = new bool[dataGridView1.Rows.Count];

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1["Column1", dataGridView1.Rows.Count - (i + 1)].Value))
                {
                    index[i] = true;
                }
                else
                    index[i] = false;
            }

            for (int i = 0; i < index.Length; i++)
            {
                if (index[i])
                {
                    index[i] = false;

                    i = index.Length - i - 1;

                    SqlCommand command = new SqlCommand(
                "DELETE FROM Pacient WHERE id=@id;"

                , sqlConnection);

                    command.Parameters.AddWithValue("id", Convert.ToInt32(dataGridView1["Column2", i].Value));

                    await command.ExecuteNonQueryAsync();

                    i = 0;
                }

            }

        }
            

        private void Button10_Click(object sender, EventArgs e)
        {
            bool[] index = new bool[dataGridView1.Rows.Count];
            string[] row = new string[7];

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1["Column1", dataGridView1.Rows.Count - (i+1)].Value))
                {
                    index[i] = true;
                }
                else
                    index[i] = false;
            }

            for (int i = 0; i < index.Length; i++)
            {
                if (index[i])
                {
                    index[i] = false;

                    i = index.Length - i - 1;
                    row[0] = Convert.ToString(dataGridView1["c1", i].Value);
                    row[1] = Convert.ToString(dataGridView1["c2", i].Value);
                    row[2] = Convert.ToString(dataGridView1["c3", i].Value);
                    row[3] = Convert.ToString(dataGridView1["Column4", i].Value);
                    row[4] = Convert.ToString(dataGridView1["Column5", i].Value);
                    row[5] = Convert.ToString(dataGridView1["Column7", i].Value);
                    row[6] = Convert.ToString(dataGridView1["Column2", i].Value);
                    pacient_analyses form2 = new pacient_analyses(row, sqlConnection);
                    form2.Owner = this;
                    form2.ShowDialog();
                    ///Форма///
                    form2.RemoveOwnedForm(form2);
                    
                    i = 0;

                }

            }

            
           
        }
        
    }
}
