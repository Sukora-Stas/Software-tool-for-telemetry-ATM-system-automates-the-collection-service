//подключаемые дерективы

using System;
using System.Windows.Forms;
using GoogleAPIroutes_GMap.Reports;

namespace GoogleAPIroutes_GMap.Forms
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        // дефолтная билиотека
        public string Shir, Dolg;
        public void Admin_Load(object sender, EventArgs e)
        {
            bankiTableAdapter.Fill(inkasaciaDataSet.Banki);
            infotableTableAdapter.Fill(inkasaciaDataSet.infotable);
            autoparkTableAdapter.Fill(inkasaciaDataSet.Autopark);
            sotrudnikTableAdapter.Fill(inkasaciaDataSet.Sotrudnik);
            infotableTableAdapter.Fill(inkasaciaDataSet.infotable);
            rKCTableAdapter.Fill(inkasaciaDataSet.RKC); 
            bankomatTableAdapter.Fill(inkasaciaDataSet.Bankomat);
        }
        public void Tabpage(string shirina, string dolgota)
        {     
            Search search = new Search();
            search.Owner = this;
            //открытие формы для работы с ней
            //страницы таб.контрола для работы с ними
            TabPage t0 = tabControl1.TabPages[0];
            TabPage t1 = tabControl1.TabPages[1];
            TabPage t2 = tabControl1.TabPages[2];
            TabPage t3 = tabControl1.TabPages[3];
            //условия для работы
            if(tabControl1.SelectedTab == t0)
            {
               ширинаTextBox.Text = shirina;
               долготаTextBox.Text = dolgota;
            }
            if (tabControl1.SelectedTab == t1)
            {
                //присваевание переменных
                ширинаTextBox1.Text = shirina;
                долготаTextBox1.Text = dolgota;
            }
            if (tabControl1.SelectedTab == t3)
            {
                //присваевание переменных
                ширинаTextBox2.Text = shirina;
                долготаTextBox2.Text = dolgota;
            }
            if (tabControl1.SelectedTab == t2)
            {
                //присваевание переменных
                ширинаTextBox3.Text = shirina;
                долготаTextBox3.Text = dolgota;
            }         
        }
        private void ширинаTextBox_TextChanged_2(object sender, EventArgs e)
        {
            // замена точек на запятую
            ширинаTextBox.Text = ширинаTextBox.Text.Replace('.', ',');
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (ширинаTextBox.Text == "")
            {
                ширинаTextBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (ширинаTextBox.Text != "")
            {
                ширинаTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void долготаTextBox_TextChanged_1(object sender, EventArgs e)
        {
            // замена точек на запятую
            долготаTextBox.Text = долготаTextBox.Text.Replace('.', ',');
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (долготаTextBox.Text == "")
            {
                долготаTextBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (долготаTextBox.Text != "")
            {
                долготаTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            //предварительные действия перед началом работы с формой
            bankomatBindingSource.AddNew();
            долготаTextBox.Enabled = true;
            ширинаTextBox.Enabled = true;
            долготаTextBox.Text = "";
            ширинаTextBox.Text = "";
            адресTextBox.Enabled = true;
            городComboBox.Enabled = true;
            button1.Visible = false;
            button2.Visible = true;
            button11.Visible = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //исключения 
            if (ширинаTextBox.Text == "" && долготаTextBox.Text == "" && адресTextBox.Text == "" && городComboBox.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ширинаTextBox.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (долготаTextBox.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (адресTextBox.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (городComboBox.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ширинаTextBox.Text != "" && долготаTextBox.Text != "" && адресTextBox.Text != "" && городComboBox.Text != "")
            {
                try
                {
                    //сохранение данных в БД
                   Validate();
                    bankomatBindingSource.EndEdit();
                    tableAdapterManager.UpdateAll(inkasaciaDataSet);
                    долготаTextBox.Enabled = false;
                    ширинаTextBox.Enabled = false;
                    адресTextBox.Enabled = false;
                    городComboBox.Enabled = false;
                    button1.Visible = true;
                    button2.Visible = false;
                    button11.Visible = false;
                }
                catch
                {
                    //исключение
                    MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //кнопка назад
            Main frm1 = new Main();
            frm1.Show();
            Hide();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //выход
            Application.Exit();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //добавление нового
            rKCBindingSource.AddNew();
            долготаTextBox1.Enabled = true;
            описаниеTextBox.Enabled = true;
            ширинаTextBox1.Enabled = true;
            долготаTextBox1.Text = "";
            ширинаTextBox1.Text = "";
            адресTextBox1.Enabled = true;
            городComboBox1.Enabled = true;
            button4.Visible = false;
            button3.Visible = true;
            button13.Visible = true;
        }
        private void ширинаTextBox1_TextChanged(object sender, EventArgs e)
        {
            // замена точек на запятую
            ширинаTextBox1.Text = ширинаTextBox1.Text.Replace('.', ',');
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (ширинаTextBox1.Text == "")
            {
                ширинаTextBox1.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (ширинаTextBox1.Text != "")
            {
                ширинаTextBox1.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void долготаTextBox1_TextChanged(object sender, EventArgs e)
        {
            // замена точек на запятую
          долготаTextBox1.Text = долготаTextBox1.Text.Replace('.', ',');
          //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (долготаTextBox1.Text == "")
            {
                долготаTextBox1.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (долготаTextBox1.Text != "")
            {
                долготаTextBox1.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //проработанные исключения
            if (ширинаTextBox1.Text == "" && долготаTextBox1.Text == "" &&описаниеTextBox.Text=="" && адресTextBox1.Text == "" && городComboBox1.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ширинаTextBox1.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (долготаTextBox1.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (описаниеTextBox.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (адресTextBox1.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (городComboBox1.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
            else if (ширинаTextBox1.Text != "" && долготаTextBox1.Text != "" && описаниеTextBox.Text != "" && адресTextBox1.Text != "" && городComboBox1.Text != "")
            {
                try
                {
                    //сохранение
                    Validate();
                    rKCBindingSource.EndEdit();
                    tableAdapterManager.UpdateAll(inkasaciaDataSet);
                    долготаTextBox1.Enabled = false;
                    описаниеTextBox.Enabled = false;
                    ширинаTextBox1.Enabled = false;
                    адресTextBox1.Enabled = false;
                    городComboBox1.Enabled = false;
                    button4.Visible = true;
                    button3.Visible = false;
                    button13.Visible = false;
                }
                catch
                {
                    //исключение
                    MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            //форма поиска
            Search search = new Search();
            search.Owner = this;
            search.ShowDialog();
        }    
        private void button6_Click(object sender, EventArgs e)
        {
            //добавление
            infotableBindingSource.AddNew();
            долготаTextBox2.Enabled = true;
            ширинаTextBox2.Enabled = true;
            долготаTextBox2.Text = "";
            ширинаTextBox2.Text = "";
            адресTextBox2.Enabled = true;
            городComboBox2.Enabled = true;
            button5.Visible = true;
            button6.Visible = false;
            button14.Visible = true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //исключения
            if (ширинаTextBox2.Text == "" && долготаTextBox2.Text == "" && адресTextBox2.Text == "" && городComboBox2.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ширинаTextBox2.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (долготаTextBox2.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (адресTextBox2.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (городComboBox2.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ширинаTextBox2.Text != "" && долготаTextBox2.Text != "" && адресTextBox2.Text != "" && городComboBox2.Text != "")
            {
                try
                {
                    //сохранение
                    Validate();
                    infotableBindingSource.EndEdit();
                    tableAdapterManager.UpdateAll(inkasaciaDataSet);
                    долготаTextBox2.Enabled = false;
                    ширинаTextBox2.Enabled = false;
                    адресTextBox2.Enabled = false;
                    городComboBox2.Enabled = false;
                    button5.Visible = false;
                    button6.Visible = true;
                    button14.Visible = false;
                }
                catch
                {
                    //исключение
                    MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            //добавление нового
            sotrudnikBindingSource.AddNew();
            фамилияTextBox.Enabled = true;
            имяTextBox.Enabled = true;
            отчествоTextBox.Enabled = true;
            инкасаторCheckBox.Enabled = true;
            водительCheckBox.Enabled = true;
            инженерCheckBox.Enabled = true;
            button7.Visible = true;
            button8.Visible = false;
            инкасаторCheckBox.Checked = false;
            водительCheckBox.Checked = false;
            инженерCheckBox.Checked = false;
            на_выездеCheckBox.Checked = false;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            //проверка данных
            if (имяTextBox.Text == "" && фамилияTextBox.Text == "" && отчествоTextBox.Text == "" && инкасаторCheckBox.Checked == false && водительCheckBox.Checked == false && инженерCheckBox.Checked == false)
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (имяTextBox.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (фамилияTextBox.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (отчествоTextBox.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (инкасаторCheckBox.Checked == false && водительCheckBox.Checked == false && инженерCheckBox.Checked == false)
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (имяTextBox.Text != "" && фамилияTextBox.Text != "" && отчествоTextBox.Text != "")
            {
                try
                {
                    //сохранение
                    Validate();
                    sotrudnikBindingSource.EndEdit();
                    tableAdapterManager.UpdateAll(inkasaciaDataSet);
                    фамилияTextBox.Enabled = false;
                    имяTextBox.Enabled = false;
                    отчествоTextBox.Enabled = false;
                    инкасаторCheckBox.Enabled = false;
                    водительCheckBox.Enabled = false;
                    инженерCheckBox.Enabled = false;
                    button7.Visible = false;
                    button8.Visible = true;
                }
                catch
                {
                    //исключение
                    MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void инкасаторCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //возможность выбрать только один чекбокс
            if (инкасаторCheckBox.Checked)
            {
                водительCheckBox.Checked = false;
                инженерCheckBox.Checked = false;
            }
        }
        private void водительCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //возможность выбрать только один чекбокс
            if (водительCheckBox.Checked)
            {
                инкасаторCheckBox.Checked = false;
                инженерCheckBox.Checked = false;
            }
        }
        private void инженерCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //возможность выбрать только один чекбокс
            if (инженерCheckBox.Checked)
            {
                инкасаторCheckBox.Checked = false;
                водительCheckBox.Checked = false;
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            //добавить нового
            autoparkBindingSource.AddNew();
            модельTextBox.Enabled = true;
            maskedTextBox1.Enabled = true;
            свободенCheckBox.Enabled = true;
            button9.Visible = true;
            button10.Visible = false;
            свободенCheckBox.Checked = false;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            //прописанные исключения
            if (модельTextBox.Text == "" && maskedTextBox1.Text == "" && свободенCheckBox.Checked == false)
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (модельTextBox.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (maskedTextBox1.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (свободенCheckBox.Checked == false)
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (модельTextBox.Text != "" && maskedTextBox1.Text != "" && свободенCheckBox.Checked)
            {
                try
                {
                    //сохранение
                    Validate();
                    autoparkBindingSource.EndEdit();
                    tableAdapterManager.UpdateAll(inkasaciaDataSet);
                    модельTextBox.Enabled = false;
                    maskedTextBox1.Enabled = false;
                    свободенCheckBox.Enabled = false;
                    button9.Visible = false;
                    button10.Visible = true;
                }
                catch
                {
                    //ошибкиа исключения
                    MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            //форма поиска координат
            Search search = new Search();
            search.Owner = this;
            search.ShowDialog();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            //форма поиска координат
            Search search = new Search();
            search.Owner = this;
            search.ShowDialog();
        }
        private void адресTextBox_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (адресTextBox.Text == "")
            {
                адресTextBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (адресTextBox.Text != "")
            {
                адресTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);

            }
        }
        private void городComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (городComboBox.Text == "")
            {
                городComboBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (городComboBox.Text != "")
            {
                городComboBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void описаниеTextBox_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (описаниеTextBox.Text == "")
            {
                описаниеTextBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (описаниеTextBox.Text != "")
            {
                описаниеTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void адресTextBox1_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (адресTextBox1.Text == "")
            {
                адресTextBox1.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (адресTextBox1.Text != "")
            {
                адресTextBox1.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void ширинаTextBox2_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            ширинаTextBox2.Text = ширинаTextBox2.Text.Replace('.', ',');
            if (ширинаTextBox2.Text == "")
            {
                ширинаTextBox2.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (ширинаTextBox2.Text != "")
            {
                ширинаTextBox2.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void долготаTextBox2_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            долготаTextBox2.Text = долготаTextBox2.Text.Replace('.', ',');
            if (долготаTextBox2.Text == "")
            {
                долготаTextBox2.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (долготаTextBox2.Text != "")
            {
                долготаTextBox2.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void адресTextBox2_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (адресTextBox2.Text == "")
            {
                адресTextBox2.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (адресTextBox2.Text != "")
            {
                адресTextBox2.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void фамилияTextBox_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (фамилияTextBox.Text == "")
            {
                фамилияTextBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (фамилияTextBox.Text != "")
            {
                фамилияTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void имяTextBox_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (имяTextBox.Text == "")
            {
                имяTextBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (имяTextBox.Text != "")
            {
                имяTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void отчествоTextBox_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (отчествоTextBox.Text == "")
            {
                отчествоTextBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (отчествоTextBox.Text != "")
            {
                отчествоTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void модельTextBox_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (модельTextBox.Text == "")
            {
                модельTextBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (модельTextBox.Text != "")
            {
                модельTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (maskedTextBox1.Text == @"       - ")
            {
                maskedTextBox1.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (maskedTextBox1.Text != @"       - ")
            {
                maskedTextBox1.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            //форма поиска координат
            Search search = new Search();
            search.Owner = this;
            search.ShowDialog();
        }
        private void button12_Click_1(object sender, EventArgs e)
        {
            //проработанные исключения 
            if (ширинаTextBox3.Text == "" && долготаTextBox3.Text == "" && описаниеTextBox1.Text == "" && адресTextBox3.Text == "" && городComboBox3.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ширинаTextBox3.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (долготаTextBox3.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (описаниеTextBox1.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (адресTextBox3.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (городComboBox3.Text == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ширинаTextBox3.Text != "" && долготаTextBox3.Text != "" && описаниеTextBox1.Text != "" && адресTextBox3.Text != "" && городComboBox3.Text != "")
            {
                try
                {
                    //сохранение
                    Validate();
                    bankiBindingSource.EndEdit();
                    tableAdapterManager.UpdateAll(inkasaciaDataSet);
                    долготаTextBox3.Enabled = false;
                    описаниеTextBox1.Enabled = false;
                    ширинаTextBox3.Enabled = false;
                    адресTextBox3.Enabled = false;
                    городComboBox3.Enabled = false;
                    button15.Visible =true;
                    button12.Visible = false;
                    button16.Visible = false;
                }
                catch
                {
                    //ошибка в случае сохранения
                    MessageBox.Show(@"Проверьте доступ к интернету!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
            //добавление нового
            bankiBindingSource.AddNew();
            долготаTextBox3.Enabled = true;
            описаниеTextBox1.Enabled = true;
            ширинаTextBox3.Enabled = true;
            долготаTextBox3.Text = "";
            ширинаTextBox3.Text = "";
            адресTextBox3.Enabled = true;
            городComboBox3.Enabled = true;
            button15.Visible = false;
            button12.Visible = true;
            button16.Visible = true;
        }
        private void ширинаTextBox3_TextChanged(object sender, EventArgs e)
        {
            //замена точки на запятую для БД
            ширинаTextBox3.Text = ширинаTextBox3.Text.Replace('.', ',');
            if (ширинаTextBox3.Text == "")
            {
                ширинаTextBox3.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (ширинаTextBox3.Text != "")
            {
                ширинаTextBox3.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void долготаTextBox3_TextChanged(object sender, EventArgs e)
        {
            //замена точки на запятую для БД
            долготаTextBox3.Text = долготаTextBox3.Text.Replace('.', ',');
            if (долготаTextBox3.Text == "")
            {
                долготаTextBox3.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (долготаTextBox3.Text != "")
            {
                долготаTextBox3.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void описаниеTextBox1_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (описаниеTextBox1.Text == "")
            {
                описаниеTextBox1.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (описаниеTextBox1.Text != "")
            {
                описаниеTextBox1.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void адресTextBox3_TextChanged(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (адресTextBox3.Text == "")
            {
                адресTextBox3.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (адресTextBox3.Text != "")
            {
                адресTextBox3.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void фамилияTextBox_TextChanged_1(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (фамилияTextBox.Text == "")
            {
                фамилияTextBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (фамилияTextBox.Text != "")
            {
                фамилияTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void имяTextBox_TextChanged_1(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (имяTextBox.Text == "")
            {
                имяTextBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (имяTextBox.Text != "")
            {
                имяTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void отчествоTextBox_TextChanged_1(object sender, EventArgs e)
        {
            //проверка на пустое поле, предупреждающая о необходимости заполнения полей
            if (отчествоTextBox.Text == "")
            {
                отчествоTextBox.BackColor = System.Drawing.Color.FromArgb(236, 174, 174);
            }
            else if (отчествоTextBox.Text != "")
            {
                отчествоTextBox.BackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            }
        }
        private void инкасаторCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            //возможность выбора только одно чекбокса
            if (инкасаторCheckBox.Checked)
            {
                водительCheckBox.Checked = false;
                инженерCheckBox.Checked = false;
            }
        }
        private void водительCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            //возможность выбора только одно чекбокса
            if (водительCheckBox.Checked)
            {
                инкасаторCheckBox.Checked = false;
                инженерCheckBox.Checked = false;
            }
        }
        private void инженерCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            //возможность выбора только одно чекбокса
            if (инженерCheckBox.Checked)
            {
                инкасаторCheckBox.Checked = false;
                водительCheckBox.Checked = false;
            }
        }
        public Search ПоискКоординат
        {
            //поиск координат
            get
            {
                throw new NotImplementedException();
            }
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

        public Search Поисккоординат
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private void отчётБанкоматовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OtchetBankomati bankomati = new OtchetBankomati();
            bankomati.Show();
        }

        private void отчётРКЦToolStripMenuItem_Click(object sender, EventArgs e)
        {
            otchet_RKC rkc = new otchet_RKC();
            rkc.Show();
        }

        private void отчётБанковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OtchetBanki banki = new OtchetBanki();
            banki.Show();
        }

        private void отчётИнфокиосковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            otchet_infokiosk infotable = new otchet_infokiosk();
            infotable.Show();
        }

        private void отчётСотрудниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            otchet_sotrudniki sotrudniki = new otchet_sotrudniki();
            sotrudniki.Show();
        }

        private void отчётАвтопаркаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OtchetAutopark autopark = new OtchetAutopark();
            autopark.Show();
        }
    }
}