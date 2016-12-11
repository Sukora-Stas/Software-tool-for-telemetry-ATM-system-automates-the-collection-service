using System;
using System.Drawing;
using System.Windows.Forms;

namespace GoogleAPIroutes_GMap.Forms
{
    public partial class Logo : Form
    {
        public Logo()
        {
            InitializeComponent();
        }
        [Flags]
        enum AnimateWindowFlags
        {
            AwHide = 0x00010000,
            AwBlend = 0x00080000
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);
        private void Logo_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            AllowTransparency = true;
            BackColor = Color.AliceBlue;//цвет фона  
            TransparencyKey = BackColor;//он же будет заменен на прозрачный цвет
            timer1.Start();
        }
        public int Value = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value = progressBar1.Value + 1;
            }
            else
            {
                timer1.Stop();
                Authorization aut = new Authorization();
                Hide();
                aut.Show();
            }
        }
        private void Logo_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(Handle, 1000, AnimateWindowFlags.AwBlend | AnimateWindowFlags.AwHide);
        }
        private void Logo_FormClosed(object sender, FormClosedEventArgs e)
        {
             AnimateWindow(Handle, 1000, AnimateWindowFlags.AwBlend | AnimateWindowFlags.AwHide);
        }
        public Authorization Authorizationcs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Authorization Загрузка
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
