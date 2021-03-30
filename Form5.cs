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
    public partial class Form5 : Form
    {
        string chooseWCode, chooseFDate;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string Connect = "Database = extrawork; Data Source = localhost; " +
                "UserId = root; Password = lglmchicha14MNp";
            try
            {
                MySqlConnection connection = new MySqlConnection(Connect);
                connection.Open();

                string query = "select distinct workcode from workingteam; ";

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

            string Connect = "Database = extrawork; Data Source = localhost; " +
                "UserId = root; Password = lglmchicha14MNp";

            MySqlConnection connection = new MySqlConnection(Connect);
            connection.Open();

            string query = "select distinct date_format(finishdate, '%Y-%m-%d') from workingteam where workcode = '" + chooseWCode + "'; ";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            List<string> FinishDate = new List<string>();

            while (reader.Read())
            {
                FinishDate.Add(reader[0].ToString());
            }
            fDate.DataSource = FinishDate;
            fDate.SelectedIndex = 0;

            connection.Close();
        }

        private void fDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            chooseFDate = fDate.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Connect = "Database = extrawork; Data Source = localhost; " +
                "UserId = root; Password = lglmchicha14MNp";
            try
            {
                MySqlConnection connection = new MySqlConnection(Connect);
                string query = "CALL close_extrawork('" + chooseWCode + "', '" + fDate.Text + "'); ";

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                command.ExecuteReader();
                connection.Close();
                MessageBox.Show("Closed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
