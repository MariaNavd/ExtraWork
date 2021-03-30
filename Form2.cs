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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(@"C:\Users\Мария\OneDrive\Документы\Инфа\1537189754.jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Connect = "Database = extrawork; Data Source = localhost; " +
                "UserId = root; Password = lglmchicha14MNp";

            try
            {
                MySqlConnection connection = new MySqlConnection(Connect);

                connection.Open();
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();
                MySqlCommand cmd;

                string query = "INSERT INTO employeewithphoto(empno, secondname, firstname, " +
                    "salary, photo) VALUES(?empNo, ?secondName, ?firstName, ?salary, ?photo)";

                cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("?empNo", MySqlDbType.String);
                cmd.Parameters.Add("?secondName", MySqlDbType.String);
                cmd.Parameters.Add("?firstName", MySqlDbType.String);
                cmd.Parameters.Add("?salary", MySqlDbType.String);
                cmd.Parameters.Add("?photo", MySqlDbType.LongBlob);

                cmd.Parameters["?empNo"].Value = empNo.Text;
                cmd.Parameters["?secondName"].Value = secondName.Text;
                cmd.Parameters["?firstName"].Value = firstName.Text;
                cmd.Parameters["?salary"].Value = salary.Text;
                cmd.Parameters["?photo"].Value = img;

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Data Inserted");
                }

                connection.Close();

                string path = @"C:\Users\Мария\OneDrive\Документы\Инфа\Допы\EmpPhoto";

                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                    MessageBox.Show(@"C:\Users\Мария\OneDrive\Документы\Инфа\EmpPhoto");
                }

                pictureBox1.Image.Save(@"C:\Users\Мария\OneDrive\Документы\Инфа\Допы\EmpPhoto\" + firstName.Text + " " + secondName.Text + ".jpg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openPicture = new OpenFileDialog();
            openPicture.Filter = "JPG|*.jpg;*.jpeg|PNG|*.png";

            if (openPicture.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openPicture.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
