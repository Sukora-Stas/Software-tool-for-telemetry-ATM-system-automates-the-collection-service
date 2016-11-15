//подклбчаемые дерективы
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Diagnostics;

namespace GoogleAPIroutes_GMap
{
    public partial class frmMain : Form
    {
        string fileIni = Environment.CurrentDirectory + @"\tools.ini";
        //системная библиотека для работы с атрибутами
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);
        //задание нулевых путей
        String dir = "";
        String pathIn = "";
        String pathOut = "";
        String pathLog = "";
        DateTime dtLog;
        public frmMain()
        {
            InitializeComponent();
        }
        private void butstart_Click(object sender, EventArgs e)
        {
            startProgramm();
        }
        public int i = 0;
        public int n = 0;
        public int m = 0;
        public int j = 0;
        private void startProgramm()
        {
            //подготовка форма и переменных
            i = 0;
            n = 0;
            m = 0;
            j = 0;
            butstart.Enabled = false;
            butstart.Text = "Обработка...";
            this.Cursor = Cursors.WaitCursor;
            txtDir.Enabled = false;
            txtIn.Enabled = false;
            txtLog.Enabled = false;
            txtOut.Enabled = false;
            btnDir.Visible = false;
            btnIn.Visible = false;
            btnLog.Visible = false;
            btnOut.Visible = false;
            //очистка меню
            listBoxMain.Items.Clear();
            listBoxDir.Items.Clear();
            dtLog = DateTime.Now;
            //получение даты и поздних дат
            DateTime localDate = DateTime.Now;
            int now = localDate.Year;
            int last = localDate.Year - 1;
            int lastlast = localDate.Year - 2;
            DateTime dtPath;
            DateTime dtb, dte;
            ZipFile zf;
            String archive;
            caption(pathIn);
            //первая отрисовка на меню
            listBoxMain.Items.Add("Start... ");
            selectedMain();
            listBoxMain.Items.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            selectedMain();
            listBoxMain.Items.Add("                     Стартовая директория: " + pathIn);
            //запись в log файл
            selectedMain();
            try
            {
                foreach (string pathDir in Directory.GetDirectories(pathIn))
                {
                    DateTime dt = Directory.GetCreationTime(pathDir);
                    //получение даты из атрибута
                    archive = dt.ToString("yyyy") + pathDir.Substring(pathIn.Length);
                    Boolean flag = DateTime.TryParseExact(archive, "yyyyddMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtPath);
                    if (flag)
                    {
                        listBoxMain.Items.Add("<---- Директория для поиска:  " + pathDir + "  ---->");
                        selectedMain();
                        // удаляем
                        pathDelete(pathDir);
                        if (dt.Year == now)
                        {
                        }
                        else if (dt.Year == last)
                        {
                            listBoxMain.Items.Add("<==========================================================================>");
                            selectedMain();
                            listBoxMain.Items.Add("Архивация директории: " + pathDir);
                            selectedMain();
                            // архиввация
                            dtb = DateTime.Now;
                            zf = new ZipFile();
                            zf.AlternateEncoding = Encoding.GetEncoding("cp866");
                            zf.AddDirectory(pathDir);//Добавляем папку
                            listBoxMain.Items.Add("Подождите... ");
                            selectedMain();
                            zf.Save(pathOut + @"\" + archive + ".zip"); //Сохраняем архив
                            dte = DateTime.Now;
                            listBoxMain.Items.Add("Директоря заархивирована");
                            selectedMain();
                            listBoxMain.Items.Add("Затрачено времени: " + (dte - dtb).ToString());
                            selectedMain();
                            m++;
                            toolStripStatusLabel1.Visible = true;
                            toolStripStatusLabel1.Text = "Создано архивов: " + m;
                            statusStrip1.Refresh();
                            // delete
                            listBoxMain.Items.Add("Удаляем обработанную директорию: " + pathDir);
                            selectedMain();
                            Directory.Delete(pathDir, true);
                            // info
                            listBoxMain.Items.Add("Удалена заархивированная директория: ");
                            selectedMain();
                            listBoxMain.Items.Add("<==========================================================================>");
                            selectedMain();
                            j++;
                            toolStripStatusLabel2.Visible = true;
                            toolStripStatusLabel2.Text = "Удалено: " + j;
                            statusStrip1.Refresh();
                        }
                        else if (dt.Year <= lastlast)
                        {
                            //удаление по уловию
                            j++;
                            listBoxMain.Items.Add("<==========================================================================>");
                            listBoxMain.Items.Add("Удаление директории: " + pathDir);
                            listBoxMain.Items.Add("Дата создания: " + dt);
                            selectedMain();
                            Directory.Delete(pathDir, true);
                            listBoxMain.Items.Add("Удаление завершено! ");
                            selectedMain();
                            toolStripStatusLabel2.Visible = true;
                            toolStripStatusLabel2.Text = "Удалено: " + j;
                            statusStrip1.Refresh();
                        }
                    }
                    else
                    {
                        listBoxMain.Items.Add("Директория исключена из просмотра: " + pathDir);
                        selectedMain();
                        listBoxMain.Items.Add("Дата создания: " + dt);
                        selectedMain();
                        listBoxMain.Items.Add("<==========================================================================>");
                        selectedMain();
                        caption(pathDir);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
            butstart.Text = "Запуск";
            butstart.Enabled = true;
            listBoxMain.Items.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            selectedMain();
            listBoxMain.Items.Add("=Finish!=");
            selectedMain();
            MessageBox.Show("Готово!", "Работа завершена!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void selectedMain()
        {
            //запись в log файл
            listBoxMain.SelectedIndex = listBoxMain.Items.Count - 1;
            listBoxMain.Refresh();
        }
        private void selectedDir()
        {
            //вывод в меню
            listBoxDir.SelectedIndex = listBoxDir.Items.Count - 1;
            listBoxDir.Refresh();
        }
        void caption(String pathDir)
        {
            //икнремент
            i++;
            caption2(pathDir);
        }

        void caption2(String pathDir)
        {
            //статус
            toolStripStatusObr.Text = "Папок найдено: " + n;
            toolStripStatusFind.Text = "Папок обработанно: " + i;
            statusStrip1.Refresh();
        }
        void pathDelete(String pathDir)
        {
            //процедура удаления директорий
            foreach (string pathNew in Directory.GetDirectories(pathDir))
            {
                caption(pathNew);
                listBoxDir.Items.Add(pathNew);
                selectedDir();
                if (pathNew.Substring(pathDir.Length + 1).ToUpper() == dir)
                {
                    try
                    {
                        // delete
                        DateTime dt = Directory.GetCreationTime(pathNew);
                        string archive_name = new DirectoryInfo(pathNew).Name;
                        if (dt.Year == dtLog.Year)
                        {
                            listBoxMain.Items.Add("Директория " + archive_name + " подлежит удалению");
                            selectedMain();
                            listBoxMain.Items.Add("Путь: " + pathNew);
                            selectedMain();
                            listBoxMain.Items.Add("Подождите....");
                            selectedMain();
                            Directory.Delete(pathNew, true);
                            listBoxMain.Items.Add("Готово!");
                            selectedMain();
                            listBoxMain.Items.Add("<==========================================================================>");
                            selectedMain();
                            j++;
                            n++;
                        }
                        else
                        {
                            Directory.Delete(pathNew, true);
                            // info
                            listBoxMain.Items.Add("Удалена директория: " + pathNew);
                            selectedMain();
                            toolStripStatusLabel2.Visible = true;
                            toolStripStatusLabel2.Text = "Удалено: " + j;
                            statusStrip1.Refresh();
                            j++;
                            n++;
                            caption2(pathNew);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        listBoxMain.Items.Add("ERROR:" + ex.Message);
                        selectedMain();
                        listBoxMain.Items.Add("Ошибка удаления директории: " + pathNew);
                        selectedMain();
                    }
                }
                else
                    pathDelete(pathNew);
            }
        }
        private void writeFile(ListBox list, Boolean flag)
        {
            if (list.Items.Count <= 0)
                return;
            String file = pathLog + getPathLog(flag);
            try
            {
                using (System.IO.StreamWriter fileStream =
                new System.IO.StreamWriter(file, true, Encoding.GetEncoding(1251)))
                {
                    fileStream.WriteLine(list.Items[list.Items.Count - 1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (!File.Exists(file))
            {
                MessageBox.Show("Файл логов не создан", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private String getPathLog(Boolean flag)
        {
            return dtLog.ToString("yyyy_MM_dd_HH_mm_ss_") + (flag ? "main" : "path") + ".log";
        }
        private void loadForm()
        {
            dir = "";
            pathIn = "";
            pathOut = "";
            pathLog = "";
            listBoxDir.Items.Clear();
            listBoxMain.Items.Clear();
            if (!File.Exists("Ionic.Zip.dll"))
            {
                MessageBox.Show("Библиотека Ionic.Zip.dll не найдена, дальнейшая работа невозможна", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }

            if (!loadIni(fileIni))
            {
                MessageBox.Show("Отсуствует файл настроек - tools.ini", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                butstart.Enabled = false;
            }
            pathIn = getPath(pathIn);
            pathOut = getPath(pathOut);
            pathLog = getPath(pathLog);
            if (dir == "")
            {
                MessageBox.Show("Директория для поиска не определена", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                butstart.Enabled = false;
                btnDir.Visible = true;
            }
            if (!Directory.Exists(pathIn))
            {
                MessageBox.Show("Отсуствует IN директория", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                butstart.Enabled = false;
                btnIn.Visible = true;
            }
            if (!Directory.Exists(pathOut))
            {
                MessageBox.Show("Отсуствует OUT директория", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                butstart.Enabled = false;
                btnOut.Visible = true;
            }
        }
        private String getPath(String pathDir)
        {
            if (pathDir != "" && pathDir.Substring(pathDir.Length - 1) != @"\")
                return pathDir + @"\";
            return pathDir;
        }
        private String getName(String str)
        {
            return str.Substring(0, str.IndexOf("=")).Trim();
        }
        private String getValue(String str)
        {
            return str.Substring(str.IndexOf("=") + 1).Trim();
        }
        private Boolean loadIni(String fileIni)
        {
            if (!File.Exists(fileIni))
                return false;
            //разделение имени надату для работы с атрибутами
            byte[] buffer = new byte[2048];
            GetPrivateProfileSection("path", buffer, 2048, fileIni);
            String[] tmp = Encoding.GetEncoding(1251).GetString(buffer).Trim('\0').Split('\0');
            foreach (String entry in tmp)
                if (getValue(entry) != "")
                {
                    if (getName(entry).ToLower() == "dir".ToLower() && dir == "")
                    {
                        dir = getValue(entry).ToUpper();
                        txtDir.Text = dir;
                    }
                    if (getName(entry).ToLower() == "pathIn".ToLower() && pathIn == "")
                    {
                        pathIn = getValue(entry);
                        txtIn.Text = pathIn;
                    }
                    if (getName(entry).ToLower() == "pathOut".ToLower() && pathOut == "")
                    {
                        pathOut = getValue(entry);
                        txtOut.Text = pathOut;
                    }
                    if (getName(entry).ToLower() == "pathLog".ToLower() && pathLog == "")
                    {
                        pathLog = getValue(entry);
                        txtLog.Text = pathLog;
                    }
                }
            return true;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //загрузка формы
            loadForm();
        }
        private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //о программе
            AboutBox1 box = new AboutBox1();
            box.Show();
        }
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //о программе
            AboutBox1 box = new AboutBox1();
            box.Show();
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //выход
            Application.Exit();
        }
        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //выход
            Application.Exit();
        }
        private void dsjlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //переход на главную форму
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        private void listBoxMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //запись в файл
            writeFile(sender as ListBox, true);
        }
        private void listBoxDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            //запись в файл
            writeFile(sender as ListBox, false);
        }
        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //редактирование
            if (!loadIni(fileIni))
            {
                MessageBox.Show("Отсуствует файл настроек - tools.ini", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                butstart.Enabled = false;
            }
            if (btnDir.Visible == true && btnIn.Visible == true && btnLog.Visible == true && btnOut.Visible == true)
            {
                btnDir.Visible = false;
                btnIn.Visible = false;
                btnLog.Visible = false;
                btnOut.Visible = false;
            }
            else if (btnDir.Visible == true)
            {
                btnDir.Visible = true;
                btnIn.Visible = true;
                btnLog.Visible = true;
                btnOut.Visible = true;
            }
            else if (btnIn.Visible == true)
            {
                btnDir.Visible = true;
                btnIn.Visible = true;
                btnLog.Visible = true;
                btnOut.Visible = true;
            }
            else if (btnOut.Visible == true)
            {
                btnDir.Visible = true;
                btnIn.Visible = true;
                btnLog.Visible = true;
                btnOut.Visible = true;
            }
            else if (btnLog.Visible == true)
            {
                btnDir.Visible = true;
                btnIn.Visible = true;
                btnLog.Visible = true;
                btnOut.Visible = true;
            }
            else
            {
                btnDir.Visible = true;
                btnIn.Visible = true;
                btnLog.Visible = true;
                btnOut.Visible = true;
            }
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            Process.Start("tools.ini");
            DialogResult res = MessageBox.Show("Редактирование tools.ini", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                loadForm();
                butstart.Enabled = true;
            }
        }
        internal AboutBox1 О_программе
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        internal GoogleAPIroutes_GMap.Properties.Settings Settings
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //справка
            Help.ShowHelp(this, "ATM.chm");
        }
        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            string key = Convert.ToString(e.KeyData);
            if (key == "F1")
            {
                //справка
                Help.ShowHelp(this, "ATM.chm");
            }
        }
    }
}
