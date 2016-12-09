//подключаемые дерективы
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GoogleAPIroutes_GMap
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }
        public Registration Registration
        {
            //ссылка на регистрацию
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        public Main Form1
        {
            get
            {
                //ссылка на главную форму
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Main авторизация
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        //подключение картинок для логина
        Image logtxt_green = Properties.Resources.LogTextbox_GREEN;
        Image logtxt_red = Properties.Resources.LogTextbox_RED;
        //подключение картинок для пароля
        Image pastxt_green = Properties.Resources.PasTextbox_GREEN;
        Image pastxt_red = Properties.Resources.PasTextbox_RED;
        //подключение картинок для глазка
        Image eye = Properties.Resources.eye;
        Image eye_checked = Properties.Resources.eye_checked3;
        private void button1_Click(object sender, EventArgs e)
        {
            //создание параметров для подключения к серверу
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            //подключение
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            //запрос
            string sQuery = string.Format(@" 
SELECT 
Логин, Пароль, ФИО
FROM LogPas");
            //использование запроса
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            //создание курсора
            foreach (DataRow dr in drArr)
            {
                //получение переменных из полученной строки
                string log = Convert.ToString(dr.ItemArray[0]);
                string pas = Convert.ToString(dr.ItemArray[1]);
                string fio = Convert.ToString(dr.ItemArray[2]);
                //условие для авторизации
                if (textBox1.Text==log && textBox2.Text==pas)
                {
                    //получение даты и времени на данный момент
                    DateTime now = DateTime.Now;
                    var myTbl1 = new DataTable();
                    //создание запроса для сохранения в БД LOG, позволяющая отслеживать регистрацию и авторизацию сотрудников в ПС
                    string sQuery1 = string.Format(@" 
insert into LOG 
values  (" + "'" + "Авторизация" + "'" + "," + "'" + fio + "'" + "," + "'" + now + "'" + "," + "'" + now.ToString("HH:mm:ss") + "'" + ")");
                    SqlDataAdapter adapter1 = new SqlDataAdapter(sQuery1, connection);
                    //выполнение
                    adapter1.Fill(myTbl1);
                    //приветствие
                    MessageBox.Show("Добро пожаловать "+fio,"Авторизация выполнена", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Main form1 = new Main();
                    form1.Show();
                    //закрытие подключения
                    connection.Close();
                    this.Hide();   
                    //остановка цикла
                    break;        
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //создание параметров для подключения к серверу
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            //подключение
            connection.Open();
            var myTbl = new DataTable();
            //запрос
            string sQuery = string.Format(@" 
SELECT 
Логин, Пароль 
FROM LogPas");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            //создание курсора
            foreach (DataRow dr in drArr)
            {
                //получение данных
                string log = Convert.ToString(dr.ItemArray[0]);
                string pas = Convert.ToString(dr.ItemArray[1]);
                //проверка, есть ли такой пользователь в БД
                if (textBox1.Text == log)
                { 
                    pictureBox2.Image = logtxt_green;
                    textBox1.BackColor = System.Drawing.Color.FromArgb(217, 244, 216);
                    break;
                }
                else
                { 
                    //исключение
                    pictureBox2.Image = logtxt_red;
                    textBox1.BackColor = System.Drawing.Color.FromArgb(248, 236, 236);
                }
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //создание параметров для подключения к серверу
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            //подключение
            connection.Open();
            var myTbl = new DataTable();
            //запрос
            string sQuery = string.Format(@" 
SELECT 
Логин, Пароль 
FROM LogPas");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            //создание курсора
            foreach (DataRow dr in drArr)
            {
                string log = Convert.ToString(dr.ItemArray[0]);
                string pas = Convert.ToString(dr.ItemArray[1]);
                //проверка на верность введённых данных
                if (textBox1.Text == log && textBox2.Text == pas)
                {  
                    pictureBox1.Image = pastxt_green;
                    textBox2.BackColor = System.Drawing.Color.FromArgb(217, 244, 216);
                    pictureBox3.BackColor = System.Drawing.Color.FromArgb(217, 244, 216);
                    break;                  
                }
                else
                {    
                    //исключение
                    pictureBox1.Image = pastxt_red;
                    textBox2.BackColor = System.Drawing.Color.FromArgb(248, 236, 236);
                    pictureBox3.BackColor = System.Drawing.Color.FromArgb(248, 236, 236);
                }
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //выход
            Application.Exit();
        }
        private void регистрацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //переход к форме регистрации
            this.Hide();
            Registration regist = new Registration();
            regist.Show();
        }
        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {        
            //просмотреть пароль
            pictureBox3.Image = eye_checked;
            textBox2.PasswordChar = '\0';
        }
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {          
            //скрыть пароль
            pictureBox3.Image = eye;
            textBox2.PasswordChar = '*';
        }
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //справка
            Help.ShowHelp(this, "ATM.chm");
        }
    }
}
