//подключаемые директивы

using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace GoogleAPIroutes_GMap.Forms
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }
        private void Search_Load(object sender, EventArgs e)
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
                //проверка подключения к интернету
                DialogResult result;
                result = MessageBox.Show(@"Проверьте доступ к интернету!", "Ошибка подключения!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if (result == System.Windows.Forms.DialogResult.Retry)
                {
                    Search_Load(sender, e);
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
        private void form_load()
        {
            gMapControl1.Bearing = 0;
            //CanDragMap - Если параметр установлен в True,
            //пользователь может перетаскивать карту 
            ///с помощью правой кнопки мыши. 
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
            //Устанавливаем центр приближения/удаления
            //курсор мыши.
            gMapControl1.MouseWheelZoomType =
                GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            //Отказываемся от негативного режима.
            gMapControl1.NegativeMode = false;
            //Разрешаем полигоны.
            gMapControl1.PolygonsEnabled = true;
            //Разрешаем маршруты
            gMapControl1.RoutesEnabled = true;
            //Скрываем внешнюю сетку карты
            //с заголовками.
            gMapControl1.ShowTileGridLines = false;
            //Указываем, что при загрузке карты будет использоваться 
            //18ти кратной приближение.
            gMapControl1.Zoom = 2;
            //Указываем что будем использовать карты Yandex.
            gMapControl1.MapProvider =
                GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode =
                GMap.NET.AccessMode.ServerOnly;
            //Если вы используете интернет через прокси сервер,
            //указываем свои учетные данные.
            GMap.NET.MapProviders.GMapProvider.WebProxy =
                System.Net.WebRequest.GetSystemWebProxy();
            GMap.NET.MapProviders.GMapProvider.WebProxy.Credentials =
                System.Net.CredentialCache.DefaultCredentials;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Запрос к API геокодирования Google.
            string url =string.Format(
                "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=true_or_false&language=ru",
                Uri.EscapeDataString(textBox1.Text));
            //Выполняем запрос к универсальному коду ресурса (URI).
            System.Net.HttpWebRequest request = 
                (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            //Получаем ответ от интернет-ресурса.
            System.Net.WebResponse response = 
                request.GetResponse();
            //Экземпляр класса System.IO.Stream 
            //для чтения данных из интернет-ресурса.
            System.IO.Stream dataStream = 
                response.GetResponseStream();
            //Инициализируем новый экземпляр класса 
            //System.IO.StreamReader для указанного потока.
            System.IO.StreamReader sreader = 
                new System.IO.StreamReader(dataStream);
            //Считывает поток от текущего положения до конца.            
            string responsereader = sreader.ReadToEnd();
            //Закрываем поток ответа.
            response.Close();
            System.Xml.XmlDocument xmldoc =
                new System.Xml.XmlDocument();
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
            {
                //Получение широты и долготы.
                System.Xml.XmlNodeList nodes =
                    xmldoc.SelectNodes("//location");
                //Переменны широты и долготы.
                double latitude = 0.0;
                double longitude = 0.0;
                //Получаем широту и долготу.
                foreach (System.Xml.XmlNode node in nodes)
                {
                    latitude = System.Xml.XmlConvert.ToDouble(node.SelectSingleNode("lat").InnerText.ToString());
                    longitude = System.Xml.XmlConvert.ToDouble(node.SelectSingleNode("lng").InnerText.ToString());
                }
                string formatted_address = xmldoc.SelectNodes("//formatted_address").Item(0).InnerText.ToString();
                //Массив, элементы которого содержат подстроки данного экземпляра, разделенные
                //одним или более знаками из separator. Дополнительные сведения см. в разделе
                //"Примечания".
                string[] words = formatted_address.Split(',');
                string dataMarker = string.Empty;
                foreach (string word in words)
                {
                    dataMarker += word + Environment.NewLine;
                }
                GMap.NET.WindowsForms.GMapOverlay markersOverlay =
                   new GMap.NET.WindowsForms.GMapOverlay(gMapControl1, "marker");
                //Инициализация нового ЗЕЛЕНОГО маркера, с указанием его координат.
                GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen markerG =
                    new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(
                    new GMap.NET.PointLatLng(latitude, longitude));
                markerG.ToolTip =
                    new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(markerG);
                shir = Convert.ToString(latitude);
                dolg = Convert.ToString(longitude);
                //Указываем, что подсказку маркера, необходимо отображать всегда.
                markerG.ToolTipMode = GMap.NET.WindowsForms.MarkerTooltipMode.Always;
                //Текст подсказки маркера.
                //Для Варианта 1,2.
                markerG.ToolTipText = dataMarker;
                markersOverlay.Markers.Add(markerG);
                //Очищаем список маркеров.
                gMapControl1.Overlays.Clear();
                //Добавляем в компонент, список маркеров.
                gMapControl1.Overlays.Add(markersOverlay);
                //Устанавливаем позицию карты.
                gMapControl1.Position = new GMap.NET.PointLatLng(latitude, longitude);
                //Указываем, что при загрузке карты будет использоваться 
                //17ти кратное приближение.
                gMapControl1.Zoom = 17;
                //Обновляем карту.
                gMapControl1.Refresh();
            }
        }
        private void MarkerClick(object sender, EventArgs e)
        {
            Admin admin = this.Owner as Admin;
            admin.Tabpage(shir, dolg);
            this.Close();
        }
     public   string shir, dolg;
        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            //Переменные для хранения 
            //координат устанавливаемого маркера.
            double lat = 0.0;
            double lng = 0.0;
            //Проверяем, что нажата правая клавиша мыши.
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //Получаем координаты, где устанавливается новый маркер.
                lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
                lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
                shir = Convert.ToString(lat);
                dolg = Convert.ToString(lng);
                //MessageBox.Show(shir + " " + dolg);
                //Запрос к API геокодирования Google.
                string url = string.Format(
                    "http://maps.google.com/maps/api/geocode/xml?latlng={0},{1}&sensor=true_or_false&language=ru",
                    lat.ToString().Replace(",", "."), lng.ToString().Replace(",", "."));
                //Выполняем запрос к универсальному коду ресурса (URI).
                System.Net.HttpWebRequest request =
                    (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                //Получаем ответ от интернет-ресурса.
                System.Net.WebResponse response =
                    request.GetResponse();
                //Экземпляр класса System.IO.Stream 
                //для чтения данных из интернет-ресурса.
                System.IO.Stream dataStream =
                    response.GetResponseStream();
                //Инициализируем новый экземпляр класса 
                //System.IO.StreamReader для указанного потока.
                System.IO.StreamReader sreader =
                    new System.IO.StreamReader(dataStream);
                //Считывает поток от текущего положения до конца.            
                string responsereader = sreader.ReadToEnd();
                //Закрываем поток ответа.
                response.Close();
                //Инициализируем новый документ Xml.
                System.Xml.XmlDocument xmldoc =
                    new System.Xml.XmlDocument();
                xmldoc.LoadXml(responsereader);
                if (xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
                {
                    //Получение информации о найденном объекте.
                    //Берем первый возвращаемый адрес.
                    string formatted_address =
                       xmldoc.SelectNodes("//formatted_address").Item(0).InnerText.ToString();
                    //Получаем массив, элементы которого содержат подстроки 
                    //данного экземпляра, разделенные
                    //одним или более знаками из separator. 
                    string[] words = formatted_address.Split(',');
                    string dataMarker = string.Empty;
                    //Получаем строку с переходами.
                    foreach (string word in words)
                    {
                        dataMarker += word + " " + Environment.NewLine;
                    }
                    //Создаем новый список маркеров, с указанием компонента 
                    //в котором они будут использоваться и названием списка.
                    GMap.NET.WindowsForms.GMapOverlay markersOverlay =
                        new GMap.NET.WindowsForms.GMapOverlay(gMapControl1, "marker");
                    //Инициализация нового ЗЕЛЕНОГО маркера, с указанием его координат.
                    GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen markerG =
                        new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(
                        new GMap.NET.PointLatLng(lat, lng));
                    markerG.ToolTip =
                        new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(markerG);
                    //Указываем, что подсказку маркера, необходимо отображать всегда.
                    markerG.ToolTipMode = GMap.NET.WindowsForms.MarkerTooltipMode.Always;
                    gMapControl1.OnMarkerClick += MarkerClick;
                    //Текст подсказки маркера.                 
                    markerG.ToolTipText = dataMarker;
                    //Добавляем маркеры в список маркеров.
                    markersOverlay.Markers.Add(markerG);
                    //Очищаем список маркеров компонента.
                    gMapControl1.Overlays.Clear();
                    //Добавляем в компонент, список маркеров.
                    gMapControl1.Overlays.Add(markersOverlay);
                    //Устанавливаем позицию карты.
                    gMapControl1.Position = new GMap.NET.PointLatLng(lat, lng);
                    //Указываем, что при загрузке карты будет использоваться 
                    //16ти кратное приближение.
                    gMapControl1.Zoom = 17;
                    //Обновляем карту.
                    gMapControl1.Refresh();
                }
            }
        }
        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }
        private void yfpflToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //закрыть
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}
