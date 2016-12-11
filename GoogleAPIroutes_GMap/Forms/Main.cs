//подключаемые дериктивы
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
//директива для работы с картами

namespace GoogleAPIroutes_GMap.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public int i = 0;
        [Flags]
        //флаги для красивого закрытия формы
        enum AnimateWindowFlags
        {
            AwHide = 0x00010000,
            AwBlend = 0x00080000
        }
        [DllImport("user32.dll")]
        static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);
        private void Form1_Load(object sender, EventArgs e)
        {
            //проверка подключения к интернету
            IPStatus status = IPStatus.TimedOut;
            try
            {
                Ping ping = new Ping();
                //сайт для проверки доступа к интернету
                PingReply reply = ping.Send(@"google.com");
                if (reply != null) status = reply.Status;
            }
            catch
            {
                // ignored
            }
            if (status != IPStatus.Success)
            {
                DialogResult result;
                result = MessageBox.Show(@"Проверьте доступ к интернету!", @"Ошибка подключения!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                //повторное подключение
                if (result == DialogResult.Retry)
                {
                    Form1_Load(sender, e);
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                //загрузка формы
                form_load();
            }
        }
        DataTable _dtRouter;
        //GMap.NET.WindowsForms.GMapOverlay overlayOne;
        private void button1_Click(object sender, EventArgs e)
        {
            _dtRouter.Rows.Clear();
            //Фрмируем запрос к API маршрутов Google.
            string url = string.Format(
                "http://maps.googleapis.com/maps/api/directions/xml?origin={0},&destination={1}&sensor=false&language=ru&mode={2}",
                Uri.EscapeDataString(textBox1.Text), Uri.EscapeDataString(textBox2.Text), Uri.EscapeDataString("driving"));
            //Выполняем запрос к универсальному коду ресурса (URI).
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(url);
            //Получаем ответ от интернет-ресурса.
            WebResponse response =
                request.GetResponse();
            //Экземпляр класса System.IO.Stream 
            //для чтения данных из интернет-ресурса.
            System.IO.Stream dataStream =
                response.GetResponseStream();
            //Инициализируем новый экземпляр класса 
            //System.IO.StreamReader для указанного потока.
            if (dataStream != null)
            {
                System.IO.StreamReader sreader =
                    new System.IO.StreamReader(dataStream);
                //Считываем поток от текущего положения до конца.            
                string responsereader = sreader.ReadToEnd();
                //Закрываем поток ответа.
                response.Close();
                System.Xml.XmlDocument xmldoc =
                    new System.Xml.XmlDocument();
                xmldoc.LoadXml(responsereader);
                if (xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
                {
                    System.Xml.XmlNodeList nodes =
                        xmldoc.SelectNodes("//leg//step");
                    //Формируем строку для добавления в таблицу.
                    object[] dr;
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        //Указываем что массив будет состоять из 
                        //восьми значений.
                        dr = new object[8];
                        //Номер шага.
                        dr[0] = i;
                        //Получение координат начала отрезка.
                        dr[1] = xmldoc.SelectNodes("//start_location").Item(i).SelectNodes("lat").Item(0).InnerText.ToString();
                        dr[2] = xmldoc.SelectNodes("//start_location").Item(i).SelectNodes("lng").Item(0).InnerText.ToString();
                        //Получение координат конца отрезка.
                        dr[3] = xmldoc.SelectNodes("//end_location").Item(i).SelectNodes("lat").Item(0).InnerText.ToString();
                        dr[4] = xmldoc.SelectNodes("//end_location").Item(i).SelectNodes("lng").Item(0).InnerText.ToString();
                        //Получение времени необходимого для прохождения этого отрезка.
                        dr[5] = xmldoc.SelectNodes("//duration").Item(i).SelectNodes("text").Item(0).InnerText.ToString();
                        //Получение расстояния, охватываемое этим отрезком.
                        dr[6] = xmldoc.SelectNodes("//distance").Item(i).SelectNodes("text").Item(0).InnerText.ToString();
                        //Получение инструкций для этого шага, представленные в виде текстовой строки HTML.
                        dr[7] = HtmlToPlainText(xmldoc.SelectNodes("//html_instructions").Item(i).InnerText.ToString());
                        //Добавление шага в таблицу.
                        _dtRouter.Rows.Add(dr);
                    }
                    //Выводим в текстовое поле адрес начала пути.
                    textBox1.Text = xmldoc.SelectNodes("//leg//start_address").Item(0).InnerText.ToString();
                    //Выводим в текстовое поле адрес конца пути.
                    textBox2.Text = xmldoc.SelectNodes("//leg//end_address").Item(0).InnerText.ToString();
                    //Выводим в текстовое поле время в пути.
                    textBox3.Text = xmldoc.GetElementsByTagName("duration")[nodes.Count].ChildNodes[1].InnerText;
                    //Выводим в текстовое поле расстояние от начальной до конечной точки.
                    textBox4.Text = xmldoc.GetElementsByTagName("distance")[nodes.Count].ChildNodes[1].InnerText;
                    //Переменные для хранения координат начала и конца пути.
                    double latStart = 0.0;
                    double lngStart = 0.0;
                    double latEnd = 0.0;
                    double lngEnd = 0.0;
                    //Получение координат начала пути.
                    latStart = System.Xml.XmlConvert.ToDouble(xmldoc.GetElementsByTagName("start_location")[nodes.Count].ChildNodes[0].InnerText);
                    lngStart = System.Xml.XmlConvert.ToDouble(xmldoc.GetElementsByTagName("start_location")[nodes.Count].ChildNodes[1].InnerText);
                    //Получение координат конечной точки.
                    latEnd = System.Xml.XmlConvert.ToDouble(xmldoc.GetElementsByTagName("end_location")[nodes.Count].ChildNodes[0].InnerText);
                    lngEnd = System.Xml.XmlConvert.ToDouble(xmldoc.GetElementsByTagName("end_location")[nodes.Count].ChildNodes[1].InnerText);
                    //Выводим в текстовое поле координаты начала пути.
                    textBox5.Text = @"Широта - "+latStart + @", Долгота - " + lngStart;
                    //Выводим в текстовое поле координаты конечной точки.
                    textBox6.Text = @"Широта - " + latEnd + @", Долгота - " + lngEnd;
                    //Устанавливаем заполненную таблицу в качестве источника.
                    dataGridView1.DataSource = _dtRouter;
                    //Устанавливаем позицию карты на начало пути.
                    gMapControl1.Position = new GMap.NET.PointLatLng(latStart, lngStart);
                    //Создаем новый список маркеров, с указанием компонента 
                    //в котором они будут использоваться и названием списка.
                    GMapOverlay markersOverlay =
                        new GMapOverlay(gMapControl1, "marker");
                    //Инициализация нового ЗЕЛЕНОГО маркера, с указанием координат начала пути.
                    GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen markerG =
                        new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(
                            new GMap.NET.PointLatLng(latStart, lngStart));
                    markerG.ToolTip =
                        new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(markerG);
                    //Указываем, что подсказку маркера, необходимо отображать всегда.
                    markerG.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    //Формируем подсказку для маркера.
                    string[] wordsG = textBox1.Text.Split(',');
                    string dataMarkerG = string.Empty;
                    foreach (string word in wordsG)
                    {
                        dataMarkerG += word + " \n";
                    }
                    //Устанавливаем текст подсказки маркера.               
                    markerG.ToolTipText = dataMarkerG;
                    //Инициализация нового Красного маркера, с указанием координат конца пути.
                    GMap.NET.WindowsForms.Markers.GMapMarkerGoogleRed markerR =
                        new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleRed(
                            new GMap.NET.PointLatLng(latEnd, lngEnd));
                    markerG.ToolTip =
                        new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(markerG);
                    //Указываем, что подсказку маркера, необходимо отображать всегда.
                    markerR.ToolTipMode = MarkerTooltipMode.Always;
                    //Формируем подсказку для маркера.
                    string[] wordsR = textBox2.Text.Split(',');
                    string dataMarkerR = string.Empty;
                    foreach (string word in wordsR)
                    {
                        dataMarkerR += word + ";\n";
                    }
                    //Текст подсказки маркера.               
                    markerR.ToolTipText = dataMarkerR;
                    //Добавляем маркеры в список маркеров.
                    markersOverlay.Markers.Add(markerG);
                    markersOverlay.Markers.Add(markerR);
                    //Очищаем список маркеров компонента.
                    //  gMapControl1.Overlays.Clear(markersOverlay);               
                    //Создаем список контрольных точек для прокладки маршрута.
                    List<GMap.NET.PointLatLng> list = new List<GMap.NET.PointLatLng>();
                    //Проходимся по определенным столбцам для получения
                    //координат контрольных точек маршрута и занесением их
                    //в список координат.
                    for (int i = 0; i < _dtRouter.Rows.Count; i++)
                    {
                        double dbStartLat = double.Parse(_dtRouter.Rows[i].ItemArray[1].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        double dbStartLng = double.Parse(_dtRouter.Rows[i].ItemArray[2].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        list.Add(new GMap.NET.PointLatLng(dbStartLat, dbStartLng));
                        double dbEndLat = double.Parse(_dtRouter.Rows[i].ItemArray[3].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        double dbEndLng = double.Parse(_dtRouter.Rows[i].ItemArray[4].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        list.Add(new GMap.NET.PointLatLng(dbEndLat, dbEndLng));
                    }
                    //Очищаем все маршруты.
                    markersOverlay.Routes.Clear();
                    //Создаем маршрут на основе списка контрольных точек.
                    GMapRoute r = new GMapRoute(list, "Route");
                    //Указываем, что данный маршрут должен отображаться.
                    r.IsVisible = true;
                    //Устанавливаем цвет маршрута.
                    r.Stroke.Color = Color.DarkGreen;
                    //Добавляем маршрут.
                    markersOverlay.Routes.Add(r);
                    //Добавляем в компонент, список маркеров и маршрутов.
                    gMapControl1.Overlays.Add(markersOverlay);
                    //Указываем, что при загрузке карты будет использоваться 
                    //9ти кратное приближение.
                    gMapControl1.Zoom = 11;
                    //Обновляем карту.
                    gMapControl1.Refresh();
                }
            }
        }
        //Удаляем HTML теги.
        public string HtmlToPlainText(string html)
        {
            html = html.Replace("</b>", "");
            return html.Replace("<b>", "");
        }
        private void MarkerClick(object sender, EventArgs e)
        {
             //Registration form2 = new Registration();
            //form2.Show();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBox7.Text == @"Беларусь" && comboBox1.Text == @"Минск")
            {
                //наведение на выбранную терретироию
                gMapControl1.Position = new GMap.NET.PointLatLng(53.902257, 27.561831);
                gMapControl1.Zoom = 11;
                comboBox2_SelectedIndexChanged(sender, e);
            }
            else if (textBox7.Text == @"Беларусь" && comboBox1.Text == @"Могилёв")
            {
                //наведение на выбранную терретироию
                gMapControl1.Position = new GMap.NET.PointLatLng(53.894617, 30.331014);
                gMapControl1.Zoom = 12;
                comboBox2_SelectedIndexChanged(sender, e);
            }
            else if (textBox7.Text == @"Беларусь" && comboBox1.Text == @"Гомель")
            {
                //наведение на выбранную терретироию
                gMapControl1.Position = new GMap.NET.PointLatLng(52.42416, 31.014272);
                gMapControl1.Zoom = 12;
                comboBox2_SelectedIndexChanged(sender, e);
            }
            else if (textBox7.Text == @"Беларусь" && comboBox1.Text == @"Брест")
            {
                //наведение на выбранную терретироию
                gMapControl1.Position = new GMap.NET.PointLatLng(52.08951, 23.71202);
                gMapControl1.Zoom = 12;
                comboBox2_SelectedIndexChanged(sender, e);
            }
            else if (textBox7.Text == @"Беларусь" && comboBox1.Text == @"Гродно")
            {
                //наведение на выбранную терретироию
                gMapControl1.Position = new GMap.NET.PointLatLng(53.678122, 23.829807);
                gMapControl1.Zoom = 12;
                comboBox2_SelectedIndexChanged(sender, e);
            }
            else if (textBox7.Text == @"Беларусь" && comboBox1.Text == @"Витебск")
            {
                //наведение на выбранную терретироию
                gMapControl1.Position = new GMap.NET.PointLatLng(55.192672, 30.206337);
                gMapControl1.Zoom = 12;
                comboBox2_SelectedIndexChanged(sender, e);
            }
            else
            {

            }
        }
        public void form_load()
        {
            //Настройки для компонента GMap.
            gMapControl1.Bearing = 0;
            //CanDragMap - Если параметр установлен в True,
            //пользователь может перетаскивать карту 
            //с помощью правой кнопки мыши. 
            gMapControl1.CanDragMap = true;
            //Указываем, что перетаскивание карты осуществляется 
            //с использованием левой клавишей мыши.
            //По умолчанию - правая.
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.GrayScaleMode = true;
            //MarkersEnabled - Если параметр установлен в True,
            //любые маркеры, заданные вручную будет показаны.
            //Если нет, они не появятся.
            gMapControl1.MarkersEnabled = true;
            //Указываем значение максимального приближения.
            gMapControl1.MaxZoom = 18;
            //Указываем значение минимального приближения.
            gMapControl1.MinZoom = 2;
            //Устанавливаем центр приближения/удаления для
            //курсора мыши.
            gMapControl1.MouseWheelZoomType =
                GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            //Отказываемся от негативного режима.
            gMapControl1.NegativeMode = false;
            //Разрешаем полигоны.
            gMapControl1.PolygonsEnabled = true;
            //Разрешаем маршруты.
            gMapControl1.RoutesEnabled = true;
            //Скрываем внешнюю сетку карты
            //с заголовками.
            gMapControl1.ShowTileGridLines = false;
            //Указываем, что при загрузке карты будет использоваться 
            //2х кратное приближение.
            // gMapControl1.Zoom = 2;
            //Указываем что будем использовать карты Google.
            gMapControl1.MapProvider =
                GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode =
                GMap.NET.AccessMode.ServerAndCache;
           WindowState = FormWindowState.Minimized;
            //this.WindowState = FormWindowState.Maximized;
             WindowState = FormWindowState.Normal;
            //Если вы используете интернет через прокси сервер,
            //указываем свои учетные данные.
            GMap.NET.MapProviders.GMapProvider.WebProxy =
                WebRequest.GetSystemWebProxy();
            GMap.NET.MapProviders.GMapProvider.WebProxy.Credentials =
                CredentialCache.DefaultCredentials;
            //инициализируем новую таблицу,
            //для хранения данных о маршруте.
            _dtRouter = new DataTable();
            //Добавляем в инициализированную таблицу,
            //новые колонки.
            _dtRouter.Columns.Add("Шаг");
            _dtRouter.Columns.Add("Нач. точка (latitude)");
            _dtRouter.Columns.Add("Нач. точка (longitude)");
            _dtRouter.Columns.Add("Кон. точка (latitude)");
            _dtRouter.Columns.Add("Кон. точка (longitude)");
            _dtRouter.Columns.Add("Время пути");
            _dtRouter.Columns.Add("Расстояние");
            _dtRouter.Columns.Add("Описание маршрута");
            //Задаем источник данных, для объекта
            //System.Windows.Forms.DataGridView.            
            dataGridView1.DataSource = _dtRouter;
            //Задаем ширину седьмого столбца.
            dataGridView1.Columns[7].Width = 1145;
            //Задаем значение, указывающее, что необходимо скрыть 
            //для пользователя параметр добавления строк.
            dataGridView1.AllowUserToAddRows = false;
            //Задаем значение, указывающее, что пользователю
            //запрещено удалять строки.
            dataGridView1.AllowUserToDeleteRows = false;
            //Задаем значение, указывающее, что пользователь
            //не может изменять ячейки элемента управления.
            dataGridView1.ReadOnly = false;
            gMapControl1.Position = new GMap.NET.PointLatLng(53.902257, 27.561831);
            gMapControl1.Zoom = 11;
            //создание меток банкомата
            bankomati_creat();
        }
        private void bankomati_creat()
        {
            //очистка карты
            gMapControl1.Overlays.Clear();      
            i = 1;
            if (comboBox2.Text == @"Банкоматы")
            {
                //подключение
                string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                SqlConnection connection = new SqlConnection(sParamConnection);
                connection.Open();
                var myTbl = new DataTable();
                //запрос
                string sQuery = (@" 
SELECT 
Ширина, Долгота, Адрес, Город, Банкомат_ID
FROM Bankomat");
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                adapter.Fill(myTbl);
                DataRow[] drArr = myTbl.Select();
                //курсор
                foreach (DataRow dr in drArr)
                {
                    i++;
                    string citi = Convert.ToString(dr.ItemArray[3]);
                    if (comboBox1.Text == citi)
                    {
                        //получение координат для создания меток
                        double shirina = Convert.ToDouble(dr.ItemArray[0]);
                        double dolgota = Convert.ToDouble(dr.ItemArray[1]);
                        string adres = Convert.ToString(dr.ItemArray[2]);
                        //  вызов прцедуры создания меток
                        bankomat_metka_creat(shirina, dolgota, adres);
                    }
                }
            }
        }
        class GMapMarkerImage : GMapMarker
        {
            //получение картинок
            private Image _image;
            public Image Image
            {
                get
                {
                    return _image;
                }
                set
                {
                    _image = value;
                    if (_image != null)
                    {
                        Size =
                            new Size(_image.Width,
                                _image.Height);
                    }
                }
            }
            public GMapMarkerImage(
                //размер картинок
                GMap.NET.PointLatLng p,
                Image image)
                : base(p)
            {
                Size =
                    new Size(
                        image.Width,
                        image.Height);
                Offset =
                    new Point(
                        -Size.Width / 2,
                        -Size.Height / 2);
                _image = image;
            }
            public override void OnRender(Graphics g)
            {
                if (_image != null)
                {
                    Rectangle rect =
                        new Rectangle(LocalPosition.X,
                                      LocalPosition.Y,
                                      Size.Width,
                                      Size.Height);
                    g.DrawImage(_image, rect);
                }
            }
        }
            public void bankomat_metka_creat(double shir, double dolg, string adress)
            {
                //Создаем новый список маркеров, с указанием компонента 
                //в котором они будут использоваться и названием списка.
                GMapOverlay markersOverlay1 =
                    new GMapOverlay(gMapControl1, "marker");
                //Инициализация нового ЗЕЛЕНОГО маркера, с указанием его координат.
                GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen markerG1 =
                    new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(
                    new GMap.NET.PointLatLng(shir, dolg));
                markerG1.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(markerG1);
                //Указываем, что подсказку маркера, необходимо отображать всегда.
                markerG1.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                gMapControl1.OnMarkerClick += MarkerClick;
                string dataMarker = string.Empty;
                //Текст подсказки маркера.               
                dataMarker += adress;
                markerG1.ToolTipText = dataMarker;
                //Добавляем маркеры в список маркеров.
                markersOverlay1.Markers.Add(markerG1);
                //Добавляем в компонент, список маркеров.
                gMapControl1.Overlays.Add(markersOverlay1);
                //Устанавливаем позицию карты.
            }
            public void test_metka_creat2(double shir, double dolg, string adress)
            {
                //Создаем новый список маркеров, с указанием компонента 
                //в котором они будут использоваться и названием списка.
                GMapOverlay markersOverlayR =
                    new GMapOverlay(gMapControl1, "marker");
                //Инициализация нового ЗЕЛЕНОГО маркера, с указанием его координат.
                GMap.NET.WindowsForms.Markers.GMapMarkerGoogleRed markerR =
                    new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleRed(
                    new GMap.NET.PointLatLng(shir, dolg));
                markerR.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(markerR);
                //Указываем, что подсказку маркера, необходимо отображать всегда.
                markerR.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                gMapControl1.OnMarkerClick += MarkerClick;
                string dataMarker = string.Empty;
                //Текст подсказки маркера.               
                dataMarker += adress;
                markerR.ToolTipText = dataMarker;
                //Добавляем маркеры в список маркеров.
                markersOverlayR.Markers.Add(markerR);
                //  test = markersOverlayR;
                //Добавляем в компонент, список маркеров.
                gMapControl1.Overlays.Add(markersOverlayR);
               //Устанавливаем позицию карты.
                //  gMapControl1.Position = new GMap.NET.PointLatLng(shir, dolg);
                // gMapControl1.Overlays.();
            }
            private void Form1_FormClosing(object sender, FormClosingEventArgs e)
            {
                AnimateWindow(Handle, 1000, AnimateWindowFlags.AwBlend | AnimateWindowFlags.AwHide);
            }
            public int BankomatSum1 = 1;
            public int InfotableSum1 = 1;
            public int RkcSum1 = 1;
            public int BankSum1 = 1;
            private void bankomat_sum()
            {
                BankomatSum1 = 1;
                //параметры подключения
                 string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                    SqlConnection connection = new SqlConnection(sParamConnection);
                    connection.Open();
                    var myTbl = new DataTable();
                    string sQuery = (@" 
SELECT 
Город
FROM Bankomat");
                    SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                    adapter.Fill(myTbl);
                    DataRow[] drArr = myTbl.Select();
                    foreach (DataRow dr in drArr)
                    {
                        //получения суумы банкоматов для рандома
                            BankomatSum1++;
                    }
            }
        private void infotable_sum()
            {
                InfotableSum1 = 1;
                string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                // MessageBox.Show(sParamConnection+""); 
                SqlConnection connection = new SqlConnection(sParamConnection);
                connection.Open();
                var myTbl = new DataTable();
                string sQuery = (@" 
SELECT 
Город
FROM infotable");
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                adapter.Fill(myTbl);
                DataRow[] drArr = myTbl.Select();
                foreach (DataRow dr in drArr)
                {
                 //получение суммы инфокиосков
                        InfotableSum1++;
                }
            }
        private void RKC_sum()
        {
            RkcSum1 = 1;         
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            // MessageBox.Show(sParamConnection+""); 
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            string sQuery = (@" 
SELECT 
Город
FROM RKC");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            foreach (DataRow dr in drArr)
            {
                //сумма ркц
                    RkcSum1++;
            }
        }
        private void bank_sum()
        {
            BankSum1 = 1;
           
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            string sQuery = (@" 
SELECT 
Город
FROM RKC");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            foreach (DataRow dr in drArr)
            {
                //сумма банков
                    BankSum1++;
            }
            connection.Close();
        }
        private void test_creat_bank()
            {
            //параметры подключения
                string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                SqlConnection connection = new SqlConnection(sParamConnection);
                connection.Open();
                var myTbl = new DataTable();
                string sQuery = (@" 
SELECT 
Ширина, Долгота, Адрес, Город, Описание
FROM Banki");
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                adapter.Fill(myTbl);
                DataRow[] drArr = myTbl.Select();

                foreach (DataRow dr in drArr)
                {
                    BankSum1++;
                }
                connection.Close();
                bank_test();
            }
       public int m = 0;
        private void randomchik(int sum)
        {
            Random rnd = new Random();
            m = rnd.Next(1, sum); ;
        }
        private void bank_test()
        {
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            string sQuery = (@" 
SELECT 
Ширина, Долгота, Адрес, Город, Описание, ID
FROM Banki");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            foreach (DataRow dr in drArr)
            {
               // randomchik(bank_sum1);
                int id = Convert.ToInt32(dr.ItemArray[5]);
                string citi = Convert.ToString(dr.ItemArray[3]);
                if (comboBox1.Text == citi)
                {
                    double shirina = Convert.ToDouble(dr.ItemArray[0]);
                    double dolgota = Convert.ToDouble(dr.ItemArray[1]);
                    string adres = Convert.ToString(dr.ItemArray[2]);
                    string opis = Convert.ToString(dr.ItemArray[4]);
                    textBox1.Text = citi + " " + adres;
                    bank_metka_test(shirina, dolgota, adres, opis);
                    break;
                }
                else if (comboBox1.Text != citi)
                {
                    connection.Close();
                    bank_test();
                }
            }
        }
        private void bank_metka_test(double shir, double dolg, string adress, string opiss)
        {
            GMapOverlay test =
                    new GMapOverlay(gMapControl1, "marker");
            Image bank_met = Properties.Resources.marker2_11;
            GMapMarkerImage rkc =
             new GMapMarkerImage(new GMap.NET.PointLatLng(shir, dolg), bank_met);
            rkc.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(rkc);
            //Указываем, что подсказку маркера, необходимо отображать всегда.
            rkc.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            gMapControl1.OnMarkerClick += MarkerClick;
            string dataMarker = string.Empty;
            //Текст подсказки маркера.               
            dataMarker += opiss + " " + Environment.NewLine + " " + adress;
            rkc.ToolTipText = dataMarker;
            //Добавляем маркеры в список маркеров.
            test.Markers.Add(rkc);
            //Добавляем в компонент, список маркеров.
            gMapControl1.Overlays.Add(test);
        }
        string _idBankomat, _adresss, _citii;
            private void test_bankomat(object sender, EventArgs e)
            {
                gMapControl1.Overlays.Clear();
                bankomati_creat();
                test_creat_bank();
                int mi = 0;
                Random rnd = new Random();
                mi = rnd.Next(1, i);
                string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                // MessageBox.Show(sParamConnection+""); 
                SqlConnection connection = new SqlConnection(sParamConnection);
                connection.Open();
                var myTbl = new DataTable();
                string sQuery = (@" 

SELECT 
Ширина, Долгота, Город, Банкомат_ID, Адрес
FROM Bankomat");
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                adapter.Fill(myTbl);
                DataRow[] drArr = myTbl.Select();
                foreach (DataRow dr in drArr)
                {
                    //  i++;
                    int id = Convert.ToInt32(dr.ItemArray[3]);
                    string citi = Convert.ToString(dr.ItemArray[2]);
                    if (comboBox1.Text == citi && id==mi)
                    {
                        double shirina = Convert.ToDouble(dr.ItemArray[0]);
                        double dolgota = Convert.ToDouble(dr.ItemArray[1]);
                        string adres = Convert.ToString(dr.ItemArray[4]);
                        _adresss = adres;
                        _citii = citi;
                        _idBankomat = Convert.ToString(id);
                        textBox2.Text = _citii + " " + _adresss;
                        //  MessageBox.Show(" "+ i); 
                        test_metka_creat2(shirina, dolgota, adres);
                        button1_Click(sender, e);
                        break;
                    }
                }
            }
            string _model, _nomer;
        private void oformlenie_auto()
            {
                string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                SqlConnection connection = new SqlConnection(sParamConnection);
                connection.Open();
                var myTbl = new DataTable();
                string sQuery = (@" 
SELECT 
Модель, [Гос. номер], Свободен, ID
FROM Autopark");
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                adapter.Fill(myTbl);
                DataRow[] drArr = myTbl.Select();
                foreach (DataRow dr in drArr)
                {
                    string mod = Convert.ToString(dr.ItemArray[0]);
                    string nom = Convert.ToString(dr.ItemArray[1]);
                    _model = mod;
                    _nomer = nom;
                    bool svo = Convert.ToBoolean(dr.ItemArray[2]);
                    string id = Convert.ToString(dr.ItemArray[3]);
                    if (svo)
                    {
                        var myTbl2 = new DataTable();
                        string sQuery2 = string.Format(@" 
UPDATE Autopark
SET Свободен = 0
WHERE ID =" + "'" + id + "'");

                        SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                        adapter2.Fill(myTbl2);
                        connection.Close();
                        break;
                    }
                }
            }
        string familia, familia1, familia2, name, name1, name2, otchestvo, otchestvo1, otchestvo2;
        private void voditel_inkasacii()
        {
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            string sQuery = (@" 
SELECT 
Фамилия, Имя, Отчество, Инкасатор, Водитель, Инженер, На_выезде, ID
FROM Sotrudnik");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            foreach (DataRow dr in drArr)
            {
                string fam = Convert.ToString(dr.ItemArray[0]);
                string nam = Convert.ToString(dr.ItemArray[1]);
                string otche = Convert.ToString(dr.ItemArray[2]);
                bool vod = Convert.ToBoolean(dr.ItemArray[4]);
                bool viesd = Convert.ToBoolean(dr.ItemArray[6]);
                string id = Convert.ToString(dr.ItemArray[7]);
                if (familia2 != "" && vod == true && viesd == false)
                {
                    familia2 = fam;
                    name2 = nam;
                    otchestvo2 = otche;
                    var myTbl2 = new DataTable();
                    string sQuery2 = string.Format(@" 
                UPDATE Sotrudnik
                SET На_выезде = 1
                WHERE ID =" + "'" + id + "'");
                    SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                    adapter2.Fill(myTbl2);
                    connection.Close();
                    break;
                }
            }
        }
        string nameJ,familiaJ,otchestvoJ;
        private void injener()
        {
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            string sQuery = (@" 
SELECT 
Фамилия, Имя, Отчество, Инкасатор, Водитель, Инженер, На_выезде, ID
FROM Sotrudnik");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            foreach (DataRow dr in drArr)
            {
                string fam = Convert.ToString(dr.ItemArray[0]);
                string nam = Convert.ToString(dr.ItemArray[1]);
                string otche = Convert.ToString(dr.ItemArray[2]);
                bool inje = Convert.ToBoolean(dr.ItemArray[5]);
                bool viesd = Convert.ToBoolean(dr.ItemArray[6]);
                string id = Convert.ToString(dr.ItemArray[7]);
                if (familia2 != "" && inje && viesd == false)
                {
                    familiaJ = fam;
                    nameJ = nam;
                    otchestvoJ = otche;
                    var myTbl2 = new DataTable();
                    string sQuery2 = string.Format(@" 
                UPDATE Sotrudnik
                SET На_выезде = 1
                WHERE ID =" + "'" + id + "'");
                    SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                    adapter2.Fill(myTbl2);
                    connection.Close();
                    break;
                }
            }
        }
        private void sotrudnik_inkasacii()
        {
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            string sQuery = (@" 
SELECT 
Фамилия, Имя, Отчество, Инкасатор, Водитель, Инженер, На_выезде, ID
FROM Sotrudnik");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            foreach (DataRow dr in drArr)
            {
                string fam = Convert.ToString(dr.ItemArray[0]);
                string nam = Convert.ToString(dr.ItemArray[1]);
                string otche = Convert.ToString(dr.ItemArray[2]);
                bool inkas = Convert.ToBoolean(dr.ItemArray[3]);
                bool viesd = Convert.ToBoolean(dr.ItemArray[6]);
                string id = Convert.ToString(dr.ItemArray[7]);
                if (familia1 != "" && inkas && viesd == false)
                {
                    familia1 = fam;
                    name1 = nam;
                    otchestvo1 = otche;
                    var myTbl2 = new DataTable();
                    string sQuery2 = string.Format(@" 
                UPDATE Sotrudnik
                SET На_выезде = 1
                WHERE ID =" + "'" + id + "'");
                    SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                    adapter2.Fill(myTbl2);
                    connection.Close();
                    break;
                }
            }
        }
        private void glawn_sotrudnik_inkasacii()
        {
            string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(sParamConnection);
            connection.Open();
            var myTbl = new DataTable();
            string sQuery = (@" 
SELECT 
Фамилия, Имя, Отчество, Инкасатор, Водитель, Инженер, На_выезде, ID
FROM Sotrudnik");
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
            adapter.Fill(myTbl);
            DataRow[] drArr = myTbl.Select();
            foreach (DataRow dr in drArr)
            {
                string fam = Convert.ToString(dr.ItemArray[0]);
                string nam = Convert.ToString(dr.ItemArray[1]);
                string otche = Convert.ToString(dr.ItemArray[2]);
                bool inkas = Convert.ToBoolean(dr.ItemArray[3]);
                bool viesd = Convert.ToBoolean(dr.ItemArray[6]);
                string id = Convert.ToString(dr.ItemArray[7]);
                if (familia !=""&& inkas && viesd == false) 
                {
                    familia = fam;
                    name = nam;
                    otchestvo = otche;
                    var myTbl2 = new DataTable();
                    string sQuery2 = string.Format(@" 
UPDATE Sotrudnik
SET На_выезде = 1
WHERE ID =" + "'" + id + "'");
                    SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, connection);
                    adapter2.Fill(myTbl2);
                    connection.Close();
                    break;
                }
            }
        }
        int _random;
        private void random_oshibka()
        {
            Random rnd = new Random();
             _random = rnd.Next(1, 3); ;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            random_oshibka();
            if (_random == 1)
            {
                MessageBox.Show(@"Код операции: " + _random + @" заканчиваются деньги, требуются касеты для замены", @"Обработка банкомата", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                 if (comboBox2.Text == @"Банкоматы")
                 {
                     test_bankomat(sender, e);
                     oformlenie_auto();
                     glawn_sotrudnik_inkasacii();
                     sotrudnik_inkasacii();
                     voditel_inkasacii();
                     Oformlenie oformlenie = new Oformlenie();
                     oformlenie.Show();
                     oformlenie.автомобильTextBox.Text = _model;
                     oformlenie.гос_номерTextBox.Text = _nomer;
                     oformlenie.водительTextBox.Text = familia2;
                     oformlenie.главный_инкасаторTextBox.Text = familia;
                     oformlenie.второй_инкасаторTextBox.Text = familia1;
                     oformlenie.объект_обслуживанияTextBox.Text = @"Банкомат № " + _idBankomat + @" Адрес: " + _adresss + @" " + _citii;
                     oformlenie.Id = _idBankomat;
                     oformlenie.textBox1.Text = Convert.ToString(_random);
                     oformlenie.Save();
                 }
            }
                 if(_random == 2)
                {
                    MessageBox.Show(@"Код операции: " + _random + @" требуется инженер для работы с банкоматом", @"Обработка банкомата", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (comboBox2.Text == @"Банкоматы")
                    {
                        test_bankomat(sender, e);
                        oformlenie_auto();
                        glawn_sotrudnik_inkasacii();
                        voditel_inkasacii();
                         injener();
                        Oformlenie oformlenie = new Oformlenie();
                        oformlenie.Show();
                        oformlenie.автомобильTextBox.Text = _model;
                        oformlenie.гос_номерTextBox.Text = _nomer;
                        oformlenie.водительTextBox.Text = familia2;
                        oformlenie.главный_инкасаторTextBox.Text = familia;
                        oformlenie.инженерTextBox.Text = familiaJ;
                        oformlenie.объект_обслуживанияTextBox.Text = @"Банкомат № " + _idBankomat + @" Адрес: " + _adresss + @" " + _citii;
                        oformlenie.Id = _idBankomat;
                        oformlenie.textBox1.Text = Convert.ToString(_random);
                        oformlenie.Save();
                    }
                }
        }
            private void bank_creat()
            {
                gMapControl1.Overlays.Clear();
                i = 1;
                string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                SqlConnection connection = new SqlConnection(sParamConnection);
                connection.Open();
                var myTbl = new DataTable();
                string sQuery = (@" 
SELECT 
Ширина, Долгота, Адрес, Город, Описание
FROM Banki");
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                adapter.Fill(myTbl);
                DataRow[] drArr = myTbl.Select();
                foreach (DataRow dr in drArr)
                {
                    i++;
                    string citi = Convert.ToString(dr.ItemArray[3]);
                    if (comboBox1.Text == citi)
                    {
                        double shirina = Convert.ToDouble(dr.ItemArray[0]);
                        double dolgota = Convert.ToDouble(dr.ItemArray[1]);
                        string adres = Convert.ToString(dr.ItemArray[2]);
                        string opis = Convert.ToString(dr.ItemArray[4]);
                        Bank_metka(shirina, dolgota, adres, opis);
                    }
                }
            }
        private void Bank_metka(double shir, double dolg, string adress, string opiss)
            {
                GMapOverlay markersrkc =
                       new GMapOverlay(gMapControl1, "marker");
                Image bankMet = Properties.Resources.marker2_11;
                GMapMarkerImage rkc =
                 new GMapMarkerImage(new GMap.NET.PointLatLng(shir, dolg), bankMet);
                rkc.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(rkc);
                //Указываем, что подсказку маркера, необходимо отображать всегда.
                rkc.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                gMapControl1.OnMarkerClick += MarkerClick;
                string dataMarker = string.Empty;
                //Текст подсказки маркера.               
                dataMarker += opiss + " " + Environment.NewLine + " " + adress;
                rkc.ToolTipText = dataMarker;
                //Добавляем маркеры в список маркеров.
                markersrkc.Markers.Add(rkc);
                //Добавляем в компонент, список маркеров.
                gMapControl1.Overlays.Add(markersrkc);
            }
            private void RKC_creat()
            {
                gMapControl1.Overlays.Clear();
                i = 1;
                    string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                    // MessageBox.Show(sParamConnection+""); 
                    SqlConnection connection = new SqlConnection(sParamConnection);
                    connection.Open();
                    var myTbl = new DataTable();
                    string sQuery = (@" 
SELECT 
Ширина, Долгота, Адрес, Город, Описание
FROM RKC");
                    SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                    adapter.Fill(myTbl);
                    DataRow[] drArr = myTbl.Select();
                    foreach (DataRow dr in drArr)
                    {
                        i++;
                        string citi = Convert.ToString(dr.ItemArray[3]);
                        if (comboBox1.Text == citi)
                        {
                            double shirina = Convert.ToDouble(dr.ItemArray[0]);
                            double dolgota = Convert.ToDouble(dr.ItemArray[1]);
                            string adres = Convert.ToString(dr.ItemArray[2]);
                            string opis = Convert.ToString(dr.ItemArray[4]);
                            RKC_metka(shirina, dolgota, adres,opis);
                        }
                    }
                }         
            private void RKC_metka(double shir, double dolg, string adress, string opiss)
            {
                    GMapOverlay markersrkc =
                    new GMapOverlay(gMapControl1, "marker");
                    Image rkcMet = Properties.Resources.marker1;
                    GMapMarkerImage rkc =
                     new GMapMarkerImage(new GMap.NET.PointLatLng(shir, dolg), rkcMet);
                    rkc.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(rkc);
                    //Указываем, что подсказку маркера, необходимо отображать всегда.
                    rkc.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    gMapControl1.OnMarkerClick += MarkerClick;
                    string dataMarker = string.Empty;
                    //Текст подсказки маркера.               
                    dataMarker += opiss + " " + Environment.NewLine + " " + adress;
                    rkc.ToolTipText = dataMarker;
                    //Добавляем маркеры в список маркеров.
                    markersrkc.Markers.Add(rkc);
                    //Добавляем в компонент, список маркеров.
                    gMapControl1.Overlays.Add(markersrkc);                
            }
        private void infotable_create()
            {
                gMapControl1.Overlays.Clear();
                i = 1;
                if (comboBox2.Text == @"Инфокиоски")
                {
                    string sParamConnection = @" 
Data Source=STAS-PK;Initial Catalog=inkasacia;Integrated Security=SSPI";
                    // MessageBox.Show(sParamConnection+""); 
                    SqlConnection connection = new SqlConnection(sParamConnection);
                    connection.Open();
                    var myTbl = new DataTable();
                    string sQuery = (@" 
SELECT 
Ширина, Долгота, Адрес, Город
FROM infotable");
                    SqlDataAdapter adapter = new SqlDataAdapter(sQuery, connection);
                    adapter.Fill(myTbl);
                    DataRow[] drArr = myTbl.Select();
                    foreach (DataRow dr in drArr)
                    {
                          i++;
                        string citi = Convert.ToString(dr.ItemArray[3]);
                        if (comboBox1.Text == citi)
                        {
                            double shirina = Convert.ToDouble(dr.ItemArray[0]);
                            double dolgota = Convert.ToDouble(dr.ItemArray[1]);
                            string adres = Convert.ToString(dr.ItemArray[2]);                        
                            infotable_metka(shirina, dolgota, adres);
                        }
                    }
                }
            }
        public void infotable_metka(double shir, double dolg, string adress)
        {
            //Создаем новый список маркеров, с указанием компонента 
            //в котором они будут использоваться и названием списка.
            GMapOverlay markersOverlay1 =
                new GMapOverlay(gMapControl1, "marker");
            //Инициализация нового ЗЕЛЕНОГО маркера, с указанием его координат.
            Image info = Properties.Resources.MapMarker_Board_Azure;
            GMapMarkerImage infotable =
             new GMapMarkerImage(new GMap.NET.PointLatLng(shir, dolg), info);
            infotable.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(infotable);
            //Указываем, что подсказку маркера, необходимо отображать всегда.
            infotable.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            gMapControl1.OnMarkerClick += MarkerClick;
            string dataMarker = string.Empty;
            //Текст подсказки маркера.               
            dataMarker += adress;
            infotable.ToolTipText = dataMarker;
            //Добавляем маркеры в список маркеров.
            markersOverlay1.Markers.Add(infotable);
            //Добавляем в компонент, список маркеров.
            gMapControl1.Overlays.Add(markersOverlay1);
            //Устанавливаем позицию карты.
        }
            private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (comboBox2.Text == @"Банкоматы")
                {
                    bankomati_creat();
                }
                else if (comboBox2.Text == @"РКЦ")
                {
                    RKC_creat();
                }
                else if (comboBox2.Text == @"Отделения банка")
                {
                    bank_creat();
                }
                else if (comboBox2.Text == @"Инфокиоски")
                {
                    infotable_create();
                }
            }
            private void pictureBox2_Click(object sender, EventArgs e)
            {
                Admin admin = new Admin();
                admin.Show();
                Hide();
            }
            private void назадToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Authorization aut = new Authorization();
                aut.Show();
                Hide();
            }
            private void выходToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Application.Exit();
            }
            public Admin Admin
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
            private void возвращениеНарядаToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Finery finery = new Finery();
                finery.Show();
                Hide();
            }
            private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
            {
                AboutBox1 box = new AboutBox1();
                box.Show();
            }
            private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Help.ShowHelp(this, "ATM.chm");
            }
            private void рекурсивнаяОбработкаToolStripMenuItem_Click(object sender, EventArgs e)
            {
                FrmMain frmain = new FrmMain();
                frmain.Show();
                Hide();
            }
            private void Form1_KeyUp(object sender, KeyEventArgs e)
            {
                string key = Convert.ToString(e.KeyData);
                if (key == "F1")
                {
                    Help.ShowHelp(this, "ATM.chm");
                }
            }
            private void отчётИспользованияToolStripMenuItem_Click(object sender, EventArgs e)
            {
                otchet_ispolz ispolz = new otchet_ispolz();
                ispolz.Show();
            }
            private void отчётНарядаToolStripMenuItem_Click(object sender, EventArgs e)
            {
                otchet_narad inkass = new otchet_narad();
                inkass.Show();
            }
            private void button3_Click(object sender, EventArgs e)
            {
                IPStatus status = IPStatus.TimedOut;
                try
                {
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(@"google.com");
                    status = reply.Status;
                }
                catch { }
                if (status != IPStatus.Success)
                {
                    DialogResult result;
                    result = MessageBox.Show(@"Проверьте доступ к интернету!", @"Ошибка подключения!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                    if (result == DialogResult.Retry)
                    {
                        Form1_Load(sender, e);
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    form_load();
                }
            }

            public Admin Настройка
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public Oformlenie Оформление
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public FrmMain Рекурсия
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public otchet_narad Отчёт
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public otchet_ispolz Отчёт2
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public Finery Подразделение
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            internal AboutBox1 Опрограмме
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }
    }