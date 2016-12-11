using System;
using System.Windows.Forms;
using GoogleAPIroutes_GMap.Forms;

namespace GoogleAPIroutes_GMap.Reports
{
    public partial class OtchetBankomati : Form
    {
        public OtchetBankomati()
        {
            InitializeComponent();
        }
        private void otchet_bankomati_Load(object sender, EventArgs e)
        {
            bankomatTableAdapter.Fill(inkasaciaDataSet.Bankomat);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //справка
            Help.ShowHelp(this, "ATM.chm");
        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
