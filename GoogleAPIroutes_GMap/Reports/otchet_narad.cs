using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoogleAPIroutes_GMap.Forms;

namespace GoogleAPIroutes_GMap
{
    public partial class otchet_narad : Form
    {
        public otchet_narad()
        {
            InitializeComponent();
        }
        private void fineryBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.fineryBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.inkasaciaDataSet);
        }
        private void otchet_narad_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "inkasaciaDataSet.finery". При необходимости она может быть перемещена или удалена.
            this.fineryTableAdapter.Fill(this.inkasaciaDataSet.finery);
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //выход
            Application.Exit();
        }
        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}
