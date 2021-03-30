using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ExtraWorkWindowsFormsApp
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            select();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
        }

        public void select()
        {
            string Connect = "Database = extrawork; Data Source = localhost; " +
                "UserId = root; Password = ****";

            MySqlConnection connection = new MySqlConnection(Connect);
            connection.Open();

            string query = "SELECT id, empno, workcode, date_format(startdate, '%Y-%m-%d'), date_format(finishdate, '%Y-%m-%d') FROM workingteam;";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns["id"].Width = 90;
            dataGridView1.Columns.Add("empno", "Employee No");
            dataGridView1.Columns["empno"].Width = 90;
            dataGridView1.Columns.Add("workcode", "Work Code");
            dataGridView1.Columns["workcode"].Width = 90;
            dataGridView1.Columns.Add("date_format(startdate, '%Y-%m-%d')", "Start Date");
            dataGridView1.Columns["date_format(startdate, '%Y-%m-%d')"].Width = 90;
            dataGridView1.Columns.Add("date_format(finishdate, '%Y-%m-%d')", "Finish Date");
            dataGridView1.Columns["date_format(finishdate, '%Y-%m-%d')"].Width = 90;

            try
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["id"].ToString(), reader["empno"].ToString(),
                   reader["workcode"].ToString(), reader["date_format(startdate, '%Y-%m-%d')"].ToString(), reader["date_format(finishdate, '%Y-%m-%d')"].ToString());

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
    }
}
