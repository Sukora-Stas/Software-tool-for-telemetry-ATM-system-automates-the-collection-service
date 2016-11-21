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
    public partial class otchet_ispolz : Form
    {
        public otchet_ispolz()
        {
            InitializeComponent();
        }
        private void lOGBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.lOGBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.inkasaciaDataSet);
        }
        private void otchet_ispolz_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "inkasaciaDataSet.LOG". При необходимости она может быть перемещена или удалена.
            this.lOGTableAdapter.Fill(this.inkasaciaDataSet.LOG);
        }
        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //закрыть
            this.Hide();
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
