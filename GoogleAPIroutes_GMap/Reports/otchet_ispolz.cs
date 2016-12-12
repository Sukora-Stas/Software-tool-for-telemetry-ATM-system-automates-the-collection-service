using System;
using System.Windows.Forms;
using GoogleAPIroutes_GMap.Forms;

namespace GoogleAPIroutes_GMap.Reports
{
    public partial class OtchetIspolz : Form
    {
        public OtchetIspolz()
        {
            InitializeComponent();
        }
        private void otchet_ispolz_Load(object sender, EventArgs e)
        {
           lOGTableAdapter.Fill(inkasaciaDataSet.LOG);
        }
        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //закрыть
            Hide();
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //выход
            Application.Exit();
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
    }
}
