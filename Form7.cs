using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ExtraWorkWindowsFormsApp
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            select();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
        }

        public void select()
        {
            string Connect = "Database = extrawork; Data Source = localhost; " +
                "UserId = root; Password = ****";

            MySqlConnection connection = new MySqlConnection(Connect);
            connection.Open();

            string query = "SELECT empno, secondname, firstname, salary FROM employeewithphoto;";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            dataGridView1.Columns.Add("empno", "Employee No");
            dataGridView1.Columns["empno"].Width = 90;
            dataGridView1.Columns.Add("secondname", "Second Name");
            dataGridView1.Columns["secondname"].Width = 90;
            dataGridView1.Columns.Add("firstname", "First Name");
            dataGridView1.Columns["firstname"].Width = 90;
            dataGridView1.Columns.Add("salary", "Salary");
            dataGridView1.Columns["salary"].Width = 70;

            try
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["empno"].ToString(),
                   reader["secondname"].ToString(), reader["firstname"].ToString(), reader["salary"].ToString());

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                command.Connection.Close();
            }
        }

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            ms.Position = 0;
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            string Connect = "Database = extrawork; Data Source = localhost; " +
                "UserId = root; Password = ****";
            string s = dataGridView1.CurrentCell.Value.ToString();

            try
            {
                MySqlConnection connection = new MySqlConnection(Connect);
                connection.Open();

                string query = "select * from employeewithphoto where empno='" + s + "';";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    pictureBox1.Image = byteArrayToImage(reader["photo"] as byte[]);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
