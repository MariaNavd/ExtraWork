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
    public partial class Form3 : Form
    {
        string chooseWCode;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string Connect = "Database = extrawork; Data Source = localhost; " +
                "UserId = root; Password = lglmchicha14MNp";
            try
            {
                MySqlConnection connection = new MySqlConnection(Connect);
                connection.Open();

                string query = "select workcode from extrawork";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                List<string> WorkCode = new List<string>();

                while (reader.Read())
                {
                    WorkCode.Add(reader[0].ToString());
                }
                wCode.DataSource = WorkCode;
                wCode.SelectedIndex = 0;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void wCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            chooseWCode = wCode.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Connect = "Database = extrawork; Data Source = localhost; " +
                "UserId = root; Password = lglmchicha14MNp";
            try
            {
                MySqlConnection connection = new MySqlConnection(Connect);
                string query = "CALL new_extrawork('" + chooseWCode + "', '" + sDate.Text + "'); ";

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                command.ExecuteReader();
                connection.Close();
                MessageBox.Show("Data Inserted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
