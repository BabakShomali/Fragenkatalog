using Fragenkatalog.Datenhaltung.DB.MySql;
using Fragenkatalog.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fragenkatalog
{
    public partial class FrageBeantwortenForm : Form
    {
        public FrageBeantwortenForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbSchueler schueler = (DbSchueler)AppStatus.EingeloggterBenutzer;
            Frage frage = schueler.ReadFrage((uint) comboBox1.SelectedIndex + 1);

            textBox1.Text = frage.Frage_;
        }
    }
}
