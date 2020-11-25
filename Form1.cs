using Fragenkatalog.Datenhaltung.DB.MySql;
using Fragenkatalog.Model;
using System;
using System.Windows.Forms;

namespace Fragenkatalog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppStatus.EingeloggterBenutzer = DbBenutzer.Login(textBox1.Text, textBox2.Text);
            Hide();
            MessageBox.Show("Benutzer ist ein : " + AppStatus.EingeloggterBenutzer.GetType().Name, "Benutzertyp");

            if (AppStatus.EingeloggterBenutzer is Admin)
            {
                BenutzerHinzufuegenForm bhf = new BenutzerHinzufuegenForm();
                bhf.ShowDialog();
            }
            else if (AppStatus.EingeloggterBenutzer is Schueler)
            {
                FrageBeantwortenForm fbf = new FrageBeantwortenForm();
                fbf.ShowDialog();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Admin has been contacted", "Forgot password", MessageBoxButtons.OK);
        }
    }
}
