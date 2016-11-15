//подключаемые дерективы
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
using Word = Microsoft.Office.Interop.Word;

namespace GoogleAPIroutes_GMap
{
    public partial class Finery : Form
    {
        public Finery()
        {
            InitializeComponent();
        }
        private void fineryBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //сохранение
            this.Validate();
            this.fineryBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.inkasaciaDataSet);
        }
        //путь к файлу
        private string TemplateFileName = @"D:\Преддипломная практика\Diplom\GoogleAPIroutes_GMap\otchet.docx";
        private string TemplateFileName2 = @"D:\Преддипломная практика\Diplom\GoogleAPIroutes_GMap\otchet2.docx";
        private void ReplaceWordsStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;//перменная для хранения данных документа
            range.Find.ClearFormatting();//метод сброса всех натсроек текста
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);//находим ключевые слова и заменяем их
        }
        private void Finery_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "inkasaciaDataSet.finery". При необходимости она может быть перемещена или удалена.
            this.fineryTableAdapter.Fill(this.inkasaciaDataSet.finery);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //поиск по БД
            for (int i = 0; i < fineryDataGridView.RowCount; i++)
            {
                fineryDataGridView.Rows[i].Selected = false;
                int j = 0;
                if (fineryDataGridView.Rows[i].Cells[j].Value != null)
                    if (fineryDataGridView.Rows[i].Cells[j].Value.ToString().ToUpper().Contains(textBox1.Text.ToUpper()))
                    {
                        fineryDataGridView.Rows[i].Visible = true;
                    }
                    else
                    {
                        fineryDataGridView.CurrentCell = null;
                        fineryDataGridView.Rows[i].Visible = false;
                    }
            }
        }
        private void auto_search(string nomer)
        {
            //поиск автомобиля по номеру
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            //подключение
            connection.Open();
            var myTbl = new DataTable();
            //запрос
            string sQuery = string.Format(@" 
SELECT 
[Гос. номер], ID
FROM Autopark");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            foreach (DataRow dr in drArr)
            {
                string nom = Convert.ToString(dr.ItemArray[0]);
                string id = Convert.ToString(dr.ItemArray[1]);
                //условие
                if (nom == nomer)
                {
                    var myTbl2 = new DataTable();
                    //возврат значения свободен с false на true
                    string sQuery2 = string.Format(@" 
UPDATE Autopark
SET Свободен = 1
WHERE ID =" + "'" + id + "'");

                    SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                    adapter2.Fill(myTbl2);
                    //закрытие подключения
                    connection.Close();
                    break;
                }
            }
        }
        private void sotrudnik( string famil)
        {
            //параметры подключения
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            //подключение
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            //запрос
            string sQuery = string.Format(@" 
SELECT 
Фамилия,ID
FROM Sotrudnik");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            //курсор
            foreach (DataRow dr in drArr)
            {
                string fam = Convert.ToString(dr.ItemArray[0]);
                string id = Convert.ToString(dr.ItemArray[1]);
                //условие проверки фамилии
                if (famil==fam)
                {
                    var myTbl2 = new DataTable();
                    //возврат значения на выезде с true на false
                    string sQuery2 = string.Format(@" 
                UPDATE Sotrudnik
                SET На_выезде = 0
                WHERE ID =" + "'" + id + "'");
                    SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                    adapter2.Fill(myTbl2);
                    connection.Close();
                    break;
                }
            }
        }
        private void poiskIN(string familia)
        {
            //параметры подключения
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            string sQuery = string.Format(@" 
SELECT 
Фамилия, Имя, Отчество
FROM Sotrudnik");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            //создание курсора
            foreach (DataRow dr in drArr)
            {
                string fam = Convert.ToString(dr.ItemArray[0]);
                string nam = Convert.ToString(dr.ItemArray[1]);
                string otche = Convert.ToString(dr.ItemArray[2]);
                //условие
                if (familia == fam)
                {
                    Namee = Convert.ToString(nam[0]);
                    Otche = Convert.ToString(otche[0]);
                }
            }
        }
        string Namee, Otche;
        private void fineryDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //переменная для word
            var wordApp = new Word.Application();
            //word скрыт
            wordApp.Visible = false;
            var COM = fineryDataGridView.Rows[e.RowIndex].Cells[0].Value;
            int pod = Convert.ToInt32(COM);
            DialogResult result;
            result = MessageBox.Show("Подтвердить возвращение наряда?", "Запрос!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                //создание подключения
                string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                SqlConnection connection = new SqlConnection(sParamConnection);
                connection.Open();
                var myTbl = new DataTable();
                //запрос
                string sQuery = string.Format(@" 

SELECT 
Поздразделение, Автомобиль, [Гос.номер], Водитель,Главный_инкасатор,[Второй инкасатор],Инженер,Объект_обслуживания,id
FROM finery");
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                adapter.Fill(myTbl);
                DataRow[] drArr = myTbl.Select();
                //курсор
                foreach (DataRow dr in drArr)
                {
                    int podr = Convert.ToInt32(dr.ItemArray[0]);
                    string auto = Convert.ToString(dr.ItemArray[1]);
                    string nomer = Convert.ToString(dr.ItemArray[2]);
                    string voditel = Convert.ToString(dr.ItemArray[3]);
                    string glaw = Convert.ToString(dr.ItemArray[4]);
                    string vtorou = Convert.ToString(dr.ItemArray[5]);
                    string injener = Convert.ToString(dr.ItemArray[6]);
                    string obj = Convert.ToString(dr.ItemArray[7]);
                    string id = Convert.ToString(dr.ItemArray[8]);
                    //поиск подразделения
                    if (pod==podr)
                   {
                       var id_bankomat = id;
                       var data = dateTimePicker1.Text;
                       poiskIN(glaw);
                       var glawn = glaw + " " + Namee + "." + Otche + ".";
                       poiskIN(vtorou);
                       var glawn1 = vtorou + " " + Namee + "." + Otche + ".";
                       poiskIN(injener);
                        //задание переменных
                       var injener1 = injener + " " + Namee + "." + Otche + ".";
                       var auto1 = auto;
                       var numer = nomer;
                       poiskIN(voditel);
                       var voditel1 = voditel + " " + Namee + "." + Otche + ".";
                       var objec = obj;
                       var podraz = Convert.ToString(podr);
                        //получение даты и времени
                       DateTime now = DateTime.Now;
                       dateTimePicker1.Text = Convert.ToString(now);
                       dateTimePicker1.MinDate = DateTime.Now;
                       //переменная для хранения нашего документа
                       var wordDocument = wordApp.Documents.Add(TemplateFileName);
                        //поиск переменных в файле и замены их на переменные из программного средства
                       ReplaceWordsStub("{ID_bankomat}", id_bankomat, wordDocument);
                       ReplaceWordsStub("{Date}", data, wordDocument);
                       ReplaceWordsStub("{podraz}", podraz, wordDocument);
                       ReplaceWordsStub("{glawn}", glawn, wordDocument);
                       ReplaceWordsStub("{ID_bankomat}", id_bankomat, wordDocument);
                       ReplaceWordsStub("{auto}", auto1, wordDocument);
                       ReplaceWordsStub("{numer}", numer, wordDocument);
                       ReplaceWordsStub("{voditel}", voditel1, wordDocument);
                       ReplaceWordsStub("{object}", objec, wordDocument);
                       ReplaceWordsStub("{glawn}", glawn, wordDocument);
                       ReplaceWordsStub("{glawn1}", glawn1, wordDocument);
                       ReplaceWordsStub("{glawn1}", glawn1, wordDocument);
                       ReplaceWordsStub("{glawn}", glawn, wordDocument);
                       ReplaceWordsStub("{Date}", data, wordDocument);
                        //поиск автомобиля и его вывод из наряда
                       auto_search(nomer);
                       //поиск водителя и его вывод из наряда
                       sotrudnik(voditel);
                       //поиск главного инкассатора и его вывод из наряда
                       sotrudnik(glaw);
                       //поиск второго инкассатора и его вывод из наряда
                       sotrudnik(vtorou);
                       if (injener!="")
                       {
                           //поиск инженера и его вывод из наряда
                           sotrudnik(injener);
                       }
                        //открытие бланка
                       wordApp.Visible = true;
                       var myTbl2 = new DataTable();
                        //запрос
                       string sQuery2 = string.Format(@" 
UPDATE finery
SET Завершён = 1
WHERE Поздразделение=" + "'" + podr + "'");
                       SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                       adapter2.Fill(myTbl2);
                       connection.Close();
                       Finery din = new Finery();
                       din.Show();
                       this.Hide();
                       break;
                   }
                }
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
            }
        }
        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //возврат на главную форму
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //справка
            Help.ShowHelp(this, "ATM.chm");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //о программе
AboutBox1 about = new AboutBox1();
            about.Show();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            //переменная для word
            var wordApp = new Word.Application();
            //word скрыт
            wordApp.Visible = false;
           // var COM = fineryDataGridView.Rows[e.RowIndex].Cells[0].Value;
            int pod = podrrrr;

            DialogResult result;
            result = MessageBox.Show("Подтвердить возвращение наряда?", "Запрос!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                //создание подключения
                string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                SqlConnection connection = new SqlConnection(sParamConnection);
                connection.Open();
                var myTbl = new DataTable();
                //запрос
                string sQuery = string.Format(@" 

SELECT 
Поздразделение, Автомобиль, [Гос.номер], Водитель,Главный_инкасатор,[Второй инкасатор],Инженер,Объект_обслуживания,id
FROM finery");
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                adapter.Fill(myTbl);
                DataRow[] drArr = myTbl.Select();
                //курсор
                foreach (DataRow dr in drArr)
                {
                    int podr = Convert.ToInt32(dr.ItemArray[0]);
                    string auto = Convert.ToString(dr.ItemArray[1]);
                    string nomer = Convert.ToString(dr.ItemArray[2]);
                    string voditel = Convert.ToString(dr.ItemArray[3]);
                    string glaw = Convert.ToString(dr.ItemArray[4]);
                    string vtorou = Convert.ToString(dr.ItemArray[5]);
                    string injener = Convert.ToString(dr.ItemArray[6]);
                    string obj = Convert.ToString(dr.ItemArray[7]);
                    string id = Convert.ToString(dr.ItemArray[8]);
                    //поиск подразделения
                    if (pod == podr)
                    {
                        if (injener != "")
                        {
                            var id_bankomat = id;
                            var data = dateTimePicker1.Text;
                            poiskIN(glaw);
                            var glawn = glaw + " " + Namee + "." + Otche + ".";

                            poiskIN(injener);
                            //задание переменных
                            var injener1 = injener + " " + Namee + "." + Otche + ".";
                            var auto1 = auto;
                            var numer = nomer;
                            poiskIN(voditel);
                            var voditel1 = voditel + " " + Namee + "." + Otche + ".";
                            var objec = obj;
                            var podraz = Convert.ToString(podr);
                            //получение даты и времени
                            DateTime now = DateTime.Now;
                            dateTimePicker1.Text = Convert.ToString(now);
                            dateTimePicker1.MinDate = DateTime.Now;
                            //переменная для хранения нашего документа
                            var wordDocument = wordApp.Documents.Add(TemplateFileName2);
                            //поиск переменных в файле и замены их на переменные из программного средства
                            ReplaceWordsStub("{ID_bankomat}", id_bankomat, wordDocument);
                            ReplaceWordsStub("{Date}", data, wordDocument);
                            ReplaceWordsStub("{podraz}", podraz, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{ID_bankomat}", objec, wordDocument);
                            ReplaceWordsStub("{auto}", auto1, wordDocument);
                            ReplaceWordsStub("{numer}", numer, wordDocument);
                            ReplaceWordsStub("{inj}", injener1, wordDocument);
                            ReplaceWordsStub("{voditel}", voditel1, wordDocument);
                            ReplaceWordsStub("{object}", objec, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{inj}", injener1, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{Date}", data, wordDocument);
                            //поиск автомобиля и его вывод из наряда
                            auto_search(nomer);
                            //поиск водителя и его вывод из наряда
                            sotrudnik(voditel);
                            //поиск главного инкассатора и его вывод из наряда
                            sotrudnik(glaw);
                            //поиск второго инкассатора и его вывод из наряда
                            sotrudnik(vtorou);
                            if (injener != "")
                            {
                                //поиск инженера и его вывод из наряда
                                sotrudnik(injener);
                            }
                            //открытие бланка
                            wordApp.Visible = true;
                            var myTbl2 = new DataTable();
                            //запрос
                            string sQuery2 = string.Format(@" 
UPDATE finery
SET Завершён = 1
WHERE Поздразделение=" + "'" + podr + "'");
                            SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                            adapter2.Fill(myTbl2);
                            connection.Close();
                            Finery din = new Finery();
                            din.Show();
                            this.Hide();
                            break;
                        }
                        else if (vtorou != "")
                        {
                            var id_bankomat = id;
                            var data = dateTimePicker1.Text;
                            poiskIN(glaw);
                            var glawn = glaw + " " + Namee + "." + Otche + ".";
                            poiskIN(vtorou);
                            var glawn1 = vtorou + " " + Namee + "." + Otche + ".";
                            poiskIN(injener);
                            //задание переменных
                            var injener1 = injener + " " + Namee + "." + Otche + ".";
                            var auto1 = auto;
                            var numer = nomer;
                            poiskIN(voditel);
                            var voditel1 = voditel + " " + Namee + "." + Otche + ".";
                            var objec = obj;
                            var podraz = Convert.ToString(podr);
                            //получение даты и времени
                            DateTime now = DateTime.Now;
                            dateTimePicker1.Text = Convert.ToString(now);
                            dateTimePicker1.MinDate = DateTime.Now;
                            //переменная для хранения нашего документа
                            var wordDocument = wordApp.Documents.Add(TemplateFileName);
                            //поиск переменных в файле и замены их на переменные из программного средства
                            ReplaceWordsStub("{ID_bankomat}", id_bankomat, wordDocument);
                            ReplaceWordsStub("{Date}", data, wordDocument);
                            ReplaceWordsStub("{podraz}", podraz, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{ID_bankomat}", objec, wordDocument);
                            ReplaceWordsStub("{auto}", auto1, wordDocument);
                            ReplaceWordsStub("{numer}", numer, wordDocument);
                            ReplaceWordsStub("{voditel}", voditel1, wordDocument);
                            ReplaceWordsStub("{object}", objec, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{glawn1}", glawn1, wordDocument);
                            ReplaceWordsStub("{glawn1}", glawn1, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{Date}", data, wordDocument);
                            //поиск автомобиля и его вывод из наряда
                            auto_search(nomer);
                            //поиск водителя и его вывод из наряда
                            sotrudnik(voditel);
                            //поиск главного инкассатора и его вывод из наряда
                            sotrudnik(glaw);
                            //поиск второго инкассатора и его вывод из наряда
                            sotrudnik(vtorou);
                            if (injener != "")
                            {
                                //поиск инженера и его вывод из наряда
                                sotrudnik(injener);
                            }
                            //открытие бланка
                            wordApp.Visible = true;
                            var myTbl2 = new DataTable();
                            //запрос
                            string sQuery2 = string.Format(@" 
UPDATE finery
SET Завершён = 1
WHERE Поздразделение=" + "'" + podr + "'");
                            SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                            adapter2.Fill(myTbl2);
                            connection.Close();
                            Finery din = new Finery();
                            din.Show();
                            this.Hide();
                            break;
                        }

                    }
                }
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
            }
        }

        private void fineryDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //переменная для word
            var wordApp = new Word.Application();
            //word скрыт
            wordApp.Visible = false;
            var COM = fineryDataGridView.Rows[e.RowIndex].Cells[0].Value;
            int pod = Convert.ToInt32(COM);
            DialogResult result;
            result = MessageBox.Show("Подтвердить возвращение наряда?", "Запрос!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                //создание подключения
                string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                SqlConnection connection = new SqlConnection(sParamConnection);
                connection.Open();
                var myTbl = new DataTable();
                //запрос
                string sQuery = string.Format(@" 

SELECT 
Поздразделение, Автомобиль, [Гос.номер], Водитель,Главный_инкасатор,[Второй инкасатор],Инженер,Объект_обслуживания,id
FROM finery");
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                adapter.Fill(myTbl);
                DataRow[] drArr = myTbl.Select();
                //курсор
                foreach (DataRow dr in drArr)
                {
                    int podr = Convert.ToInt32(dr.ItemArray[0]);
                    string auto = Convert.ToString(dr.ItemArray[1]);
                    string nomer = Convert.ToString(dr.ItemArray[2]);
                    string voditel = Convert.ToString(dr.ItemArray[3]);
                    string glaw = Convert.ToString(dr.ItemArray[4]);
                    string vtorou = Convert.ToString(dr.ItemArray[5]);
                    string injener = Convert.ToString(dr.ItemArray[6]);
                    string obj = Convert.ToString(dr.ItemArray[7]);
                    string id = Convert.ToString(dr.ItemArray[8]);
                    //поиск подразделения
                    if (pod == podr)
                    {
                        if (injener!="")
                        {
                            var id_bankomat = id;
                            var data = dateTimePicker1.Text;
                            poiskIN(glaw);
                            var glawn = glaw + " " + Namee + "." + Otche + ".";
                            
                            poiskIN(injener);
                            //задание переменных
                            var injener1 = injener + " " + Namee + "." + Otche + ".";
                            var auto1 = auto;
                            var numer = nomer;
                            poiskIN(voditel);
                            var voditel1 = voditel + " " + Namee + "." + Otche + ".";
                            var objec = obj;
                            var podraz = Convert.ToString(podr);
                            //получение даты и времени
                            DateTime now = DateTime.Now;
                            dateTimePicker1.Text = Convert.ToString(now);
                            dateTimePicker1.MinDate = DateTime.Now;
                            //переменная для хранения нашего документа
                            var wordDocument = wordApp.Documents.Add(TemplateFileName2);
                            //поиск переменных в файле и замены их на переменные из программного средства
                            ReplaceWordsStub("{ID_bankomat}", id_bankomat, wordDocument);
                            ReplaceWordsStub("{Date}", data, wordDocument);
                            ReplaceWordsStub("{podraz}", podraz, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{ID_bankomat}", objec, wordDocument);
                            ReplaceWordsStub("{auto}", auto1, wordDocument);
                            ReplaceWordsStub("{numer}", numer, wordDocument);
                            ReplaceWordsStub("{inj}", injener1, wordDocument);
                            ReplaceWordsStub("{voditel}", voditel1, wordDocument);
                            ReplaceWordsStub("{object}", objec, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{inj}", injener1, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{Date}", data, wordDocument);
                            //поиск автомобиля и его вывод из наряда
                            auto_search(nomer);
                            //поиск водителя и его вывод из наряда
                            sotrudnik(voditel);
                            //поиск главного инкассатора и его вывод из наряда
                            sotrudnik(glaw);
                            //поиск второго инкассатора и его вывод из наряда
                            sotrudnik(vtorou);
                            if (injener != "")
                            {
                                //поиск инженера и его вывод из наряда
                                sotrudnik(injener);
                            }
                            //открытие бланка
                            wordApp.Visible = true;
                            var myTbl2 = new DataTable();
                            //запрос
                            string sQuery2 = string.Format(@" 
UPDATE finery
SET Завершён = 1
WHERE Поздразделение=" + "'" + podr + "'");
                            SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                            adapter2.Fill(myTbl2);
                            connection.Close();
                            Finery din = new Finery();
                            din.Show();
                            this.Hide();
                            break;
                        }
                        else if (vtorou != "")
                        {
                            var id_bankomat = id;
                            var data = dateTimePicker1.Text;
                            poiskIN(glaw);
                            var glawn = glaw + " " + Namee + "." + Otche + ".";
                            poiskIN(vtorou);
                            var glawn1 = vtorou + " " + Namee + "." + Otche + ".";
                            poiskIN(injener);
                            //задание переменных
                            var injener1 = injener + " " + Namee + "." + Otche + ".";
                            var auto1 = auto;
                            var numer = nomer;
                            poiskIN(voditel);
                            var voditel1 = voditel + " " + Namee + "." + Otche + ".";
                            var objec = obj;
                            var podraz = Convert.ToString(podr);
                            //получение даты и времени
                            DateTime now = DateTime.Now;
                            dateTimePicker1.Text = Convert.ToString(now);
                            dateTimePicker1.MinDate = DateTime.Now;
                            //переменная для хранения нашего документа
                            var wordDocument = wordApp.Documents.Add(TemplateFileName);
                            //поиск переменных в файле и замены их на переменные из программного средства
                            ReplaceWordsStub("{ID_bankomat}", id_bankomat, wordDocument);
                            ReplaceWordsStub("{Date}", data, wordDocument);
                            ReplaceWordsStub("{podraz}", podraz, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{ID_bankomat}", objec, wordDocument);
                            ReplaceWordsStub("{auto}", auto1, wordDocument);
                            ReplaceWordsStub("{numer}", numer, wordDocument);
                            ReplaceWordsStub("{voditel}", voditel1, wordDocument);
                            ReplaceWordsStub("{object}", objec, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{glawn1}", glawn1, wordDocument);
                            ReplaceWordsStub("{glawn1}", glawn1, wordDocument);
                            ReplaceWordsStub("{glawn}", glawn, wordDocument);
                            ReplaceWordsStub("{Date}", data, wordDocument);
                            //поиск автомобиля и его вывод из наряда
                            auto_search(nomer);
                            //поиск водителя и его вывод из наряда
                            sotrudnik(voditel);
                            //поиск главного инкассатора и его вывод из наряда
                            sotrudnik(glaw);
                            //поиск второго инкассатора и его вывод из наряда
                            sotrudnik(vtorou);
                            if (injener != "")
                            {
                                //поиск инженера и его вывод из наряда
                                sotrudnik(injener);
                            }
                            //открытие бланка
                            wordApp.Visible = true;
                            var myTbl2 = new DataTable();
                            //запрос
                            string sQuery2 = string.Format(@" 
UPDATE finery
SET Завершён = 1
WHERE Поздразделение=" + "'" + podr + "'");
                            SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                            adapter2.Fill(myTbl2);
                            connection.Close();
                            Finery din = new Finery();
                            din.Show();
                            this.Hide();
                            break;
                        }

                    }
                }
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
            }
        }
        public int podrrrr;
        private void fineryDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var COM = fineryDataGridView.Rows[e.RowIndex].Cells[0].Value;
            int pod = Convert.ToInt32(COM);
            podrrrr = pod;
        }
    }
}
