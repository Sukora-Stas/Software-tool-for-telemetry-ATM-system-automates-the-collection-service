using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleAPIroutes_GMap
{
    public partial class otchet_RKC : Form
    {
        public otchet_RKC()
        {
            InitializeComponent();
        }

        private void rKCBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.rKCBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.inkasaciaDataSet);

        }

        private void otchet_RKC_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "inkasaciaDataSet.RKC". При необходимости она может быть перемещена или удалена.
            this.rKCTableAdapter.Fill(this.inkasaciaDataSet.RKC);

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
            this.Hide();
        }
    }
}
