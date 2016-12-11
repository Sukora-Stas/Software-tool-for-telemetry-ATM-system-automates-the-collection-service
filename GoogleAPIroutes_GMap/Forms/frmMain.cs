//подклбчаемые дерективы

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Ionic.Zip;

namespace GoogleAPIroutes_GMap.Forms
{
    public partial class FrmMain : Form
    {
        string fileIni = Environment.CurrentDirectory + @"\tools.ini";
        //системная библиотека для работы с атрибутами
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);
        //задание нулевых путей
        String _dir = "";
        String _pathIn = "";
        String _pathOut = "";
        String _pathLog = "";
        DateTime _dtLog;
        public FrmMain()
        {
            InitializeComponent();
        }
        private void butstart_Click(object sender, EventArgs e)
        {
            StartProgramm();
        }
        public int i = 0;
        public int n = 0;
        public int m = 0;
        public int j = 0;
        private void StartProgramm()
        {
            //подготовка форма и переменных
            i = 0;
            n = 0;
            m = 0;
            j = 0;
            butstart.Enabled = false;
            butstart.Text = @"Обработка...";
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
            _dtLog = DateTime.Now;
            //получение даты и поздних дат
            DateTime localDate = DateTime.Now;
            int now = localDate.Year;
            int last = localDate.Year - 1;
            int lastlast = localDate.Year - 2;
            DateTime dtPath;
            DateTime dtb, dte;
            ZipFile zf;
            String archive;
            Caption();
            //первая отрисовка на меню
            listBoxMain.Items.Add("Start... ");
            SelectedMain();
            listBoxMain.Items.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            SelectedMain();
            listBoxMain.Items.Add("                     Стартовая директория: " + _pathIn);
            //запись в log файл
            SelectedMain();
            try
            {
                foreach (string pathDir in Directory.GetDirectories(_pathIn))
                {
                    DateTime dt = Directory.GetCreationTime(pathDir);
                    //получение даты из атрибута
                    archive = dt.ToString("yyyy") + pathDir.Substring(_pathIn.Length);
                    Boolean flag = DateTime.TryParseExact(archive, "yyyyddMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtPath);
                    if (flag)
                    {
                        listBoxMain.Items.Add("<---- Директория для поиска:  " + pathDir + "  ---->");
                        SelectedMain();
                        // удаляем
                        PathDelete(pathDir);
                        if (dt.Year == now)
                        {
                        }
                        else if (dt.Year == last)
                        {
                            listBoxMain.Items.Add("<==========================================================================>");
                            SelectedMain();
                            listBoxMain.Items.Add("Архивация директории: " + pathDir);
                            SelectedMain();
                            // архиввация
                            dtb = DateTime.Now;
                            zf = new ZipFile {AlternateEncoding = Encoding.GetEncoding("cp866")};
                            zf.AddDirectory(pathDir);//Добавляем папку
                            listBoxMain.Items.Add("Подождите... ");
                            SelectedMain();
                            zf.Save(_pathOut + @"\" + archive + ".zip"); //Сохраняем архив
                            dte = DateTime.Now;
                            listBoxMain.Items.Add("Директоря заархивирована");
                            SelectedMain();
                            listBoxMain.Items.Add("Затрачено времени: " + (dte - dtb).ToString());
                            SelectedMain();
                            m++;
                            toolStripStatusLabel1.Visible = true;
                            toolStripStatusLabel1.Text = @"Создано архивов: " + m;
                            statusStrip1.Refresh();
                            // delete
                            listBoxMain.Items.Add("Удаляем обработанную директорию: " + pathDir);
                            SelectedMain();
                            Directory.Delete(pathDir, true);
                            // info
                            listBoxMain.Items.Add("Удалена заархивированная директория: ");
                            SelectedMain();
                            listBoxMain.Items.Add("<==========================================================================>");
                            SelectedMain();
                            j++;
                            toolStripStatusLabel2.Visible = true;
                            toolStripStatusLabel2.Text = @"Удалено: " + j;
                            statusStrip1.Refresh();
                        }
                        else if (dt.Year <= lastlast)
                        {
                            //удаление по уловию
                            j++;
                            listBoxMain.Items.Add("<==========================================================================>");
                            listBoxMain.Items.Add("Удаление директории: " + pathDir);
                            listBoxMain.Items.Add("Дата создания: " + dt);
                            SelectedMain();
                            Directory.Delete(pathDir, true);
                            listBoxMain.Items.Add("Удаление завершено! ");
                            SelectedMain();
                            toolStripStatusLabel2.Visible = true;
                            toolStripStatusLabel2.Text = @"Удалено: " + j;
                            statusStrip1.Refresh();
                        }
                    }
                    else
                    {
                        listBoxMain.Items.Add("Директория исключена из просмотра: " + pathDir);
                        SelectedMain();
                        listBoxMain.Items.Add("Дата создания: " + dt);
                        SelectedMain();
                        listBoxMain.Items.Add("<==========================================================================>");
                        SelectedMain();
                        Caption();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Cursor = Cursors.Default;
            butstart.Text = @"Запуск";
            butstart.Enabled = true;
            listBoxMain.Items.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            SelectedMain();
            listBoxMain.Items.Add("=Finish!=");
            SelectedMain();
            MessageBox.Show(@"Проверьте доступ к интернету!", @"Работа завершена!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void SelectedMain()
        {
            //запись в log файл
            listBoxMain.SelectedIndex = listBoxMain.Items.Count - 1;
            listBoxMain.Refresh();
        }
        private void SelectedDir()
        {
            //вывод в меню
            listBoxDir.SelectedIndex = listBoxDir.Items.Count - 1;
            listBoxDir.Refresh();
        }
        void Caption()
        {
            //икнремент
            i++;
            Caption2();
        }

        void Caption2()
        {
            //статус
            toolStripStatusObr.Text = @"Папок найдено: " + n;
            toolStripStatusFind.Text = @"Папок обработанно: " + i;
            statusStrip1.Refresh();
        }
        void PathDelete(String pathDir)
        {
            //процедура удаления директорий
            foreach (string pathNew in Directory.GetDirectories(pathDir))
            {
                Caption();
                listBoxDir.Items.Add(pathNew);
                SelectedDir();
                if (pathNew.Substring(pathDir.Length + 1).ToUpper() == _dir)
                {
                    try
                    {
                        // delete
                        DateTime dt = Directory.GetCreationTime(pathNew);
                        string archiveName = new DirectoryInfo(pathNew).Name;
                        if (dt.Year == _dtLog.Year)
                        {
                            listBoxMain.Items.Add("Директория " + archiveName + " подлежит удалению");
                            SelectedMain();
                            listBoxMain.Items.Add("Путь: " + pathNew);
                            SelectedMain();
                            listBoxMain.Items.Add("Подождите....");
                            SelectedMain();
                            Directory.Delete(pathNew, true);
                            listBoxMain.Items.Add("Готово!");
                            SelectedMain();
                            listBoxMain.Items.Add("<==========================================================================>");
                            SelectedMain();
                            j++;
                            n++;
                        }
                        else
                        {
                            Directory.Delete(pathNew, true);
                            // info
                            listBoxMain.Items.Add("Удалена директория: " + pathNew);
                            SelectedMain();
                            toolStripStatusLabel2.Visible = true;
                            toolStripStatusLabel2.Text = @"Удалено: " + j;
                            statusStrip1.Refresh();
                            j++;
                            n++;
                            Caption2();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        listBoxMain.Items.Add("ERROR:" + ex.Message);
                        SelectedMain();
                        listBoxMain.Items.Add("Ошибка удаления директории: " + pathNew);
                        SelectedMain();
                    }
                }
                else
                    PathDelete(pathNew);
            }
        }
        private void WriteFile(ListBox list, Boolean flag)
        {
            if (list.Items.Count <= 0)
                return;
            String file = _pathLog + getPathLog(flag);
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
                MessageBox.Show(@"Проверьте доступ к интернету!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private String getPathLog(Boolean flag)
        {
            return _dtLog.ToString("yyyy_MM_dd_HH_mm_ss_") + (flag ? "main" : "path") + ".log";
        }
        private void loadForm()
        {
            _dir = "";
            _pathIn = "";
            _pathOut = "";
            _pathLog = "";
            listBoxDir.Items.Clear();
            listBoxMain.Items.Clear();
            if (!File.Exists("Ionic.Zip.dll"))
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }

            if (!loadIni(fileIni))
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                butstart.Enabled = false;
            }
            _pathIn = getPath(_pathIn);
            _pathOut = getPath(_pathOut);
            _pathLog = getPath(_pathLog);
            if (_dir == "")
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                butstart.Enabled = false;
                btnDir.Visible = true;
            }
            if (!Directory.Exists(_pathIn))
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                butstart.Enabled = false;
                btnIn.Visible = true;
            }
            if (!Directory.Exists(_pathOut))
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (getName(entry).ToLower() == "dir".ToLower() && _dir == "")
                    {
                        _dir = getValue(entry).ToUpper();
                        txtDir.Text = _dir;
                    }
                    if (getName(entry).ToLower() == "pathIn".ToLower() && _pathIn == "")
                    {
                        _pathIn = getValue(entry);
                        txtIn.Text = _pathIn;
                    }
                    if (getName(entry).ToLower() == "pathOut".ToLower() && _pathOut == "")
                    {
                        _pathOut = getValue(entry);
                        txtOut.Text = _pathOut;
                    }
                    if (getName(entry).ToLower() == "pathLog".ToLower() && _pathLog == "")
                    {
                        _pathLog = getValue(entry);
                        txtLog.Text = _pathLog;
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
            Main form1 = new Main();
            form1.Show();
            this.Hide();
        }
        private void listBoxMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //запись в файл
            WriteFile(sender as ListBox, true);
        }
        private void listBoxDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            //запись в файл
            WriteFile(sender as ListBox, false);
        }
        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //редактирование
            if (!loadIni(fileIni))
            {
                MessageBox.Show(@"Проверьте доступ к интернету!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DialogResult res = MessageBox.Show(@"Проверьте доступ к интернету!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
