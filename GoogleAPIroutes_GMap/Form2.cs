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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void logPasBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.logPasBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.inkasaciaDataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "inkasaciaDataSet.LogPas". При необходимости она может быть перемещена или удалена.
            this.logPasTableAdapter.Fill(this.inkasaciaDataSet.LogPas);

        }
    }
}
