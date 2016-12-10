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
using GoogleAPIroutes_GMap.Forms;

namespace GoogleAPIroutes_GMap
{
    public partial class Registration : Form
    {
        public Registration()
        {      
            InitializeComponent();
        }

        public Authorization регистрация
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        Image logtxt = Properties.Resources.LogTextbox;
        Image logtxt_green = Properties.Resources.LogTextbox_GREEN;
        Image logtxt_red = Properties.Resources.LogTextbox_RED;
        Image pastxt = Properties.Resources.PasTextbox;
        Image pastxt_green = Properties.Resources.PasTextbox_GREEN;
        Image pastxt_red = Properties.Resources.PasTextbox_RED;
        Image eye = Properties.Resources.eye;
        Image eye_checked = Properties.Resources.eye_checked3;
        Boolean a = false;
        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "inkasaciaDataSet.LogPas". При необходимости она может быть перемещена или удалена.
            this.logPasTableAdapter.Fill(this.inkasaciaDataSet.LogPas);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "inkas_V1DataSet.Authorization". При необходимости она может быть перемещена или удалена.
            this.logPasBindingSource.AddNew();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string q = фИОTextBox.Text;
            connect();
            if (a == false)
            {
                DateTime now = DateTime.Now;
                // MessageBox.Show(now.ToString("HH:mm:ss") + "");
                string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                SqlConnection connection = new SqlConnection(sParamConnection);
                connection.Open();
                var myTbl1 = new DataTable();
                string sQuery1 = string.Format(@" 
insert into LOG 
values  (" + "'" + "Регистрация" + "'" + "," + "'" + q + "'" + "," + "'" + now + "'" + "," + "'" + now.ToString("HH:mm:ss") + "'" + ")");
                SqlDataAdapter adapter1 = new SqlDataAdapter(sQuery1, connection);
                adapter1.Fill(myTbl1);
                this.Validate();
                this.logPasBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.inkasaciaDataSet);
                button1.Visible = true;
                button2.Visible = true;
                MessageBox.Show("Пользователь " + q + " добавлен в БД","Регистрация выполнена");
            }           
        }
        private void logPasBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.logPasBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.inkasaciaDataSet);
        }
        private void connect()
        {
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            string sQuery = string.Format(@" 
SELECT 
Логин, Пароль 
FROM LogPas");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            foreach (DataRow dr in drArr)
            {
                string log = Convert.ToString(dr.ItemArray[0]);
                if (логинTextBox.Text == log)
                {
                    MessageBox.Show("Ошибка, данный логин уже занят");
                    a = true;
                    connection.Close();
                    break;
                }
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void регистрацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Authorization aut = new Authorization();
            aut.Show();
        }
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = eye;
            парольTextBox.PasswordChar = '*';
        }
        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Image = eye_checked;
            парольTextBox.PasswordChar = '\0';
        }
        private void логинTextBox_TextChanged(object sender, EventArgs e)
        {
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            // MessageBox.Show(sParamConnection+""); 
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            string sQuery = string.Format(@" 
SELECT 
Логин, Пароль 
FROM LogPas");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            foreach (DataRow dr in drArr)
            {
                string log = Convert.ToString(dr.ItemArray[0]);
                if (логинTextBox.Text == "")
                {
                    pictureBox2.Image = logtxt;
                    логинTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
                }
                else if (логинTextBox.Text == log)
                    {
                        pictureBox2.Image = logtxt_red;
                        логинTextBox.BackColor = System.Drawing.Color.FromArgb(248, 236, 236);
                      break;
                    }
                    else
                    {
                        pictureBox2.Image = logtxt_green;
                        логинTextBox.BackColor = System.Drawing.Color.FromArgb(217, 244, 216);
                    }
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Authorization aut = new Authorization();
            aut.Show();
        }
        private void парольTextBox_TextChanged(object sender, EventArgs e)
        {
            if (парольTextBox.Text == "")
            {
                pictureBox1.Image = pastxt_red;
                парольTextBox.BackColor = System.Drawing.Color.FromArgb(248, 236, 236);
                pictureBox3.BackColor = System.Drawing.Color.FromArgb(248, 236, 236);
            }
            else
            {
                pictureBox1.Image = pastxt_green;
                парольTextBox.BackColor = System.Drawing.Color.FromArgb(217, 244, 216);
                pictureBox3.BackColor = System.Drawing.Color.FromArgb(217, 244, 216);
            }
        }
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //справка
            Help.ShowHelp(this, "ATM.chm");
        }
    }
}
