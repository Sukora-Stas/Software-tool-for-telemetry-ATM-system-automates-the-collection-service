using System;
using System.Windows.Forms;
using GoogleAPIroutes_GMap.Forms;

namespace GoogleAPIroutes_GMap.Reports
{
    public partial class OtchetInfokiosk : Form
    {
        public OtchetInfokiosk()
        {
            InitializeComponent();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //справка
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
        private void otchet_infokiosk_Load(object sender, EventArgs e)
        {
            infotableTableAdapter.Fill(inkasaciaDataSet.infotable);
        }
    }
}
