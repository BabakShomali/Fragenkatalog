using Fragenkatalog.Datenhaltung.DB.MySql;
using Fragenkatalog.Model;
using System;
using System.Windows.Forms;

namespace Fragenkatalog
{
    public partial class BenutzerHinzufuegenForm : Form
    {
        public BenutzerHinzufuegenForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AppStatus.EingeloggterBenutzer is null) return;

            DbAdmin dbAdmin = AppStatus.EingeloggterBenutzer as DbAdmin;

            if (dbAdmin is null) return;

            int rolle_nr = comboBox1.SelectedIndex;
            if (rolle_nr == -1) return;
            rolle_nr += 1;
            Benutzer benutzer = AppStatus.Datenhaltungsadapter.BenutzerAnlegen(textBox1.Text, textBox2.Text, textBox3.Text, (uint)rolle_nr);
            MessageBox.Show("New user "+benutzer.Login_name+" has been created!", "Success!");
        }

        public bool passwordCheck()
        {
            if (textBox3.Text == textBox4.Text)
                return true;

            else return false;
        }
    }
}
