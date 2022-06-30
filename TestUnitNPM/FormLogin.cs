using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TestUnitNPM
{
    public partial class FormLogin : Form
    {
        string server = "localhost";
        string username = "alcheman";
        string pass = "Alchemanmysql1";
        string db = "camera";
        string sqlQuery;
        MySqlConnection sqlConn = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();

        string[] CSmain = { "localhost", "alcheman", "Alchemanmysql1", "camera" };
        string path1 = "";
        string path2 = "";

        public FormLogin()
        {
            InitializeComponent();
            ServiceController service = new ServiceController("MySQL80");

            try
            {
                if (service.Status.Equals(ServiceControllerStatus.Stopped) ||

                    service.Status.Equals(ServiceControllerStatus.StopPending))

                {
                    service.Start();

                    //MessageBox.Show("Mysql services status running");
                }
                else
                {
                    service.Refresh();
                    //MessageBox.Show("service telah di refresh");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message);
            }
            upload();

            // Specify the directory you want to manipulate.
            string path3 = @"D:\Key\";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path1))
                {
                    Console.WriteLine("That path1 exists already.");
                    //return;
                }
                if (Directory.Exists(path2))
                {
                    Console.WriteLine("That path2 exists already.");
                    //return;
                }
                if (Directory.Exists(path3))
                {
                    Console.WriteLine("That path3 exists already.");
                    //return;
                }
                // Try to create the directory.
                //DirectoryInfo di = Directory.CreateDirectory(path2);
                Directory.CreateDirectory(path2);
                // Try to create the directory.
                Directory.CreateDirectory(path1);
                Directory.CreateDirectory(path2);
                Directory.CreateDirectory(path3);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("The process failed: {0}", ex.ToString());
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            sqlConn.ConnectionString = "server=" + server + ";" +
                                       "user id=" + username + ";" +
                                       "password=" + pass + ";" +
                                       "database=" + db;
            try
            {
                sqlConn.Open();
                sqlCmd.Connection = sqlConn;
                sqlQuery = "select * from camera.operator where operator_name = '" + tbUser.Text + "' and password = '" + tbPassword.Text + "' ";
                sqlCmd.CommandText = sqlQuery;
                sqlCmd = new MySqlCommand(sqlQuery, sqlConn);
                MySqlDataReader sqlReader = sqlCmd.ExecuteReader();


                if (sqlReader.Read() == true)
                {
                    //Hide();



                    string a = sqlReader.GetString(0);
                    string b = sqlReader.GetString(2);
                    //FormMainMenu formmain = new FormMainMenu(a, b);
                    //formmain.Show();

                    MessageBox.Show($"Data1: {a}. Data2: {b}");

                }
                else
                {
                    MessageBox.Show("Wrong Username or Password, please try again.");
                    tbUser.Text = "";
                    tbPassword.Text = "";
                    tbUser.Focus();
                }
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            //DebugInfo.GetInfo("masuk form login");

            upload();
        }

        private void FormLogin_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void FormLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void upload()
        {
            DataTable sqlData = new DataTable();
            string sqlQuery1 = "SELECT * FROM camera.setting";
            try
            {
                sqlData = MyDB.ExReader(CSmain, sqlQuery1);
                if (sqlData != null)
                {
                    path1 = sqlData.Rows[0].ItemArray[16].ToString();
                    path2 = sqlData.Rows[0].ItemArray[17].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string connectionS = "server=" + server + ";" +
                                       "user id=" + username + ";" +
                                       "password=" + pass + ";" +
                                       "database=" + db;
            sqlConn.ConnectionString = connectionS;
            try
            {
                sqlConn.Open();
                MessageBox.Show($"Connection Success. \n {connectionS}");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable sqlData = new DataTable();
            string sqlQuery1 = "SELECT * FROM camera.setting";
            try
            {
                sqlData = MyDB.ExReader(CSmain, sqlQuery1);
                if (sqlData != null)
                {
                    path1 = sqlData.Rows[0].ItemArray[16].ToString();
                    path2 = sqlData.Rows[0].ItemArray[17].ToString();
                    MessageBox.Show($"{path1} \n{path2}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

