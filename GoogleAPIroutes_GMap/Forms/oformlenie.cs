using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace GoogleAPIroutes_GMap
{
    public partial class Oformlenie : Form
    {
        public Oformlenie()
        {
            InitializeComponent();
        }
        private void fineryBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.fineryBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.inkasaciaDataSet);
        }
        public void oform_load()
        {
            this.fineryTableAdapter.Fill(this.inkasaciaDataSet.finery);
            this.fineryBindingSource.AddNew();
        }
        int _m;
        private void Randomchik()
        {
            Random rnd = new Random();
            _m = rnd.Next(1, 999);
            Proverka(_m);
        }
        private void Proverka(int m)
        {
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            string sQuery = string.Format(@" 
SELECT 
Поздразделение
FROM finery");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            foreach (DataRow dr in drArr)
            {
                int podr = Convert.ToInt32(dr.ItemArray[0]);
                if (podr==m)
                {
                    Randomchik();
                }
            }
        }
        private void oformlenie_Load(object sender, EventArgs e)
        {
            this.fineryTableAdapter.Fill(this.inkasaciaDataSet.finery);
            this.fineryBindingSource.AddNew();
            завершёнCheckBox.Checked = false;
            Randomchik();
            поздразделениеTextBox.Text = Convert.ToString(_m);
        }
        public string id;
        private string TemplateFileName = @"D:\Преддипломная практика\Diplom\GoogleAPIroutes_GMap\inkass.docx";//путь к файлу
        private string TemplateFileName2 = @"D:\Преддипломная практика\Diplom\GoogleAPIroutes_GMap\injener.docx";//путь к файлу

        private void ReplaceWordsStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;//перменная для хранения данных документа
            range.Find.ClearFormatting();//метод сброса всех натсроек текста
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);//находим ключевые слова и заменяем их
        }
        private void PoiskIn(string familia)
        {
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
                foreach (DataRow dr in drArr)
                {
                    string fam = Convert.ToString(dr.ItemArray[0]);
                    string nam = Convert.ToString(dr.ItemArray[1]);
                    string otche = Convert.ToString(dr.ItemArray[2]);
                    if (familia==fam)
                    {
                       _name = Convert.ToString(nam[0]);
                        _otche = Convert.ToString(otche[0]);
                    }
                }
            }
        string _name,_otche;
        public void save()
        {
            DateTime now = DateTime.Now;
            dateTimePicker1.Text = Convert.ToString(now, CultureInfo.InvariantCulture);
            dateTimePicker1.MinDate = DateTime.Now;
            var id_bankomat = id;
            var data = dateTimePicker1.Text;
            PoiskIn(главный_инкасаторTextBox.Text);
            var glawn = главный_инкасаторTextBox.Text + " " + _name + "." + _otche + ".";
            PoiskIn(второй_инкасаторTextBox.Text);
            var glawn1 = второй_инкасаторTextBox.Text + " " + _name + "." + _otche + ".";
            PoiskIn(инженерTextBox.Text);
            var injener = инженерTextBox.Text + " " + _name + "." + _otche + ".";
            var auto = автомобильTextBox.Text;
            var numer = гос_номерTextBox.Text;
            PoiskIn(водительTextBox.Text);
            var voditel = водительTextBox.Text + " " + _name + "." + _otche + ".";
            var objec = объект_обслуживанияTextBox.Text;
            var podraz = поздразделениеTextBox.Text;
            var wordApp = new Word.Application();//переменная для word
            wordApp.Visible = false;//word скрыт
            if (textBox1.Text == "1")
            {
                try
                {
                    var wordDocument = wordApp.Documents.Add(TemplateFileName);//переменная для хранения нашего документа
                    ReplaceWordsStub("{ID_bankomat}", id_bankomat, wordDocument);
                    ReplaceWordsStub("{Date}", data, wordDocument);
                    ReplaceWordsStub("{podraz}", podraz, wordDocument);
                    ReplaceWordsStub("{glawn}", glawn, wordDocument);
                    ReplaceWordsStub("{ID_bankomat}", id_bankomat, wordDocument);
                    ReplaceWordsStub("{auto}", auto, wordDocument);
                    ReplaceWordsStub("{numer}", numer, wordDocument);
                    ReplaceWordsStub("{voditel}", voditel, wordDocument);
                    ReplaceWordsStub("{object}", objec, wordDocument);
                    ReplaceWordsStub("{glawn}", glawn, wordDocument);
                    ReplaceWordsStub("{glawn1}", glawn1, wordDocument);
                    ReplaceWordsStub("{glawn1}", glawn1, wordDocument);
                    ReplaceWordsStub("{glawn}", glawn, wordDocument);
                    ReplaceWordsStub("{Date}", data, wordDocument);
                    wordApp.Visible = true;
                }
                catch
                {
                    MessageBox.Show(@"Проверьте доступ к интернету!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);//окно ошибки
                }
            }
            if (textBox1.Text == "2")
            {
                try
                {
                    randomchik_sis_oshibka();
                    var ochibka = random;
                    var wordDocument = wordApp.Documents.Add(TemplateFileName2);//переменная для хранения нашего документа
                    ReplaceWordsStub("{ID_bankomat}", id_bankomat, wordDocument);
                    ReplaceWordsStub("{Date}", data, wordDocument);
                    ReplaceWordsStub("{podraz}", podraz, wordDocument);
                    ReplaceWordsStub("{glawn}", glawn, wordDocument);
                    ReplaceWordsStub("{ID_bankomat}", id_bankomat, wordDocument);
                    ReplaceWordsStub("{auto}", auto, wordDocument);
                    ReplaceWordsStub("{numer}", numer, wordDocument);
                    ReplaceWordsStub("{voditel}", voditel, wordDocument);
                    ReplaceWordsStub("{object}", objec, wordDocument);
                    ReplaceWordsStub("{injener}", injener, wordDocument);
                    ReplaceWordsStub("{glawn}", glawn, wordDocument);
                    if (ochibka == 1)
                    {
                        string oshibka = "Системная ошибка, требуеется инженер для работы";
                        ReplaceWordsStub("{oshibka}", oshibka, wordDocument);
                    }
                    else if (ochibka == 2)
                    {
                        string oshibka = "Зажевало карточку, работа приостановлена";
                        ReplaceWordsStub("{oshibka}", oshibka, wordDocument);
                    }
                    else if (ochibka == 3)
                    {
                        string oshibka = "Нет соеденения с сервером";
                        ReplaceWordsStub("{oshibka}", oshibka, wordDocument);
                    }
                    else if (ochibka == 4)
                    {
                        string oshibka = "Неообходимо обслуживание банкомата";
                        ReplaceWordsStub("{oshibka}", oshibka, wordDocument);
                    }
                    else if (ochibka == 5)
                    {
                        string oshibka = "Диагностика системы";
                        ReplaceWordsStub("{oshibka}", oshibka, wordDocument);
                    }                 
                    ReplaceWordsStub("{glawn}", glawn, wordDocument);
                    ReplaceWordsStub("{inj}", injener, wordDocument);
                    
                    ReplaceWordsStub("{glawn}", glawn, wordDocument);
                    ReplaceWordsStub("{Date}", data, wordDocument);
                    wordApp.Visible = true;
                }
                catch
                {
                    MessageBox.Show(@"Проверьте доступ к интернету!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);//окно ошибки
                }
            }
                try
                {
                    this.Validate();
                    this.fineryBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.inkasaciaDataSet);
                }
                catch
                {
                    MessageBox.Show(@"Проверьте доступ к интернету!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);//окно ошибки
                }
                Main form1 = new Main();
                form1.Owner = this;
                this.Hide();
        }
        int random;
        private void randomchik_sis_oshibka()
        {
            Random rnd = new Random();
            random = rnd.Next(1, 6); ;
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
