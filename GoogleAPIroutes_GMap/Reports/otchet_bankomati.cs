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
    public partial class otchet_bankomati : Form
    {
        public otchet_bankomati()
        {
            InitializeComponent();
        }

        private void bankomatBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bankomatBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.inkasaciaDataSet);

        }

        private void otchet_bankomati_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "inkasaciaDataSet.Bankomat". При необходимости она может быть перемещена или удалена.
            this.bankomatTableAdapter.Fill(this.inkasaciaDataSet.Bankomat);

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
            this.Hide();
        }
    }
}
