using System;
using System.Windows.Forms;
using GoogleAPIroutes_GMap.Forms;

namespace GoogleAPIroutes_GMap.Reports
{
    public partial class OtchetSotrudniki : Form
    {
        public OtchetSotrudniki()
        {
            InitializeComponent();
        }
        private void otchet_sotrudniki_Load(object sender, EventArgs e)
        {
            sotrudnikTableAdapter.Fill(inkasaciaDataSet.Sotrudnik);

        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "ATM.chm");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
