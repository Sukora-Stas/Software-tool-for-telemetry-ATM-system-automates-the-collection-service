using System;
using System.Windows.Forms;
using GoogleAPIroutes_GMap.Forms;

namespace GoogleAPIroutes_GMap.Reports
{
    public partial class OtchetRkc : Form
    {
        public OtchetRkc()
        {
            InitializeComponent();
        }
        private void otchet_RKC_Load(object sender, EventArgs e)
        {
           rKCTableAdapter.Fill(inkasaciaDataSet.RKC);

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

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
