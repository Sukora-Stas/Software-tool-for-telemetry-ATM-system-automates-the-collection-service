using System;
using System.Windows.Forms;
using GoogleAPIroutes_GMap.Forms;

namespace GoogleAPIroutes_GMap.Reports
{
    public partial class otchet_narad : Form
    {
        public otchet_narad()
        {
            InitializeComponent();
        }
        private void otchet_narad_Load(object sender, EventArgs e)
        {
            fineryTableAdapter.Fill(inkasaciaDataSet.finery);
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //выход
            Application.Exit();
        }
        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //закрыть
            Hide();
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
