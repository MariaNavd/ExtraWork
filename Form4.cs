using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ExtraWorkWindowsFormsApp
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            select();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void select()
        {
            string Connect = "Database = extrawork; Data Source = localhost; " +
                "UserId = root; Password = lglmchicha14MNp";

            MySqlConnection connection = new MySqlConnection(Connect);
            connection.Open();
            string query1 = "select empno from employee";

            MySqlCommand command1 = new MySqlCommand(query1, connection);
            MySqlDataReader reader1 = command1.ExecuteReader();

            List<string> EmpNo = new List<string>();

            while (reader1.Read())
            {
                EmpNo.Add(reader1[0].ToString());
            }
            connection.Close();

            var addCol = new DataGridViewComboBoxColumn()
            {
                DataSource = EmpNo
            };

            connection.Open();

            string query = "SELECT id, workcode, date_format(startdate, '%Y-%m-%d'), date_format(finishdate, '%Y-%m-%d') FROM workingteam WHERE empno is null;";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns["id"].Width = 30;
            dataGridView1.Columns.Add("workcode", "Work Code");
            dataGridView1.Columns["workcode"].Width = 60;
            dataGridView1.Columns.Add("startdate", "Start Date");
            dataGridView1.Columns["startdate"].Width = 85;
            dataGridView1.Columns.Add("finishdate", "Finish Date");
            dataGridView1.Columns["finishdate"].Width = 85;
            dataGridView1.Columns.Add(addCol);

            try
            {
                while (reader.Read())
                {

                    dataGridView1.Rows.Add(reader["id"].ToString(),
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

        private void button1_Click(object sender, EventArgs e)
        {
            string Connect = "Database = extrawork; Data Source = localhost; " +
                "UserId = root; Password = lglmchicha14MNp";
            try
            {
                MySqlConnection connection = new MySqlConnection(Connect);

                for (int counter = 0; counter < (dataGridView1.Rows.Count); counter++)
                {
                    if(dataGridView1.Rows[counter].Cells[4].Value != null)
                    {
                        string query = "UPDATE workingteam SET empno = '" + dataGridView1.Rows[counter].Cells[4].Value +
                        "' WHERE workcode = '" + dataGridView1.Rows[counter].Cells[1].Value + "' AND startdate = '" + dataGridView1.Rows[counter].Cells[2].Value +
                        "' AND id = " + dataGridView1.Rows[counter].Cells[0].Value + "; ";

                        MySqlCommand command = new MySqlCommand(query, connection);

                        connection.Open();
                        command.ExecuteReader();
                        connection.Close();
                    }
                }
                MessageBox.Show("Commited");                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
