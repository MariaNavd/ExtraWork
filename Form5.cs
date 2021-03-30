using System;
using System.Collections.Generic;
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
                "UserId = root; Password = ****";
            try
            {
                MySqlConnection connection = new MySqlConnection(Connect);
                connection.Open();

                string query = "SELECT DISTINCT workcode FROM workingteam; ";

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
                "UserId = root; Password = ****";

            MySqlConnection connection = new MySqlConnection(Connect);
            connection.Open();

            string query = "SELECT DISTINCT date_format(finishdate, '%Y-%m-%d') FROM workingteam WHERE workcode = '" + chooseWCode + "'; ";

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
                "UserId = root; Password = ****";
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
