using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fragenkatalog.UI.Windows.Forms
{
    public partial class AdminUIForm : Form
    {
        public AdminUIForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string email = textBox2.Text;
            string pw1 = textBox3.Text;
            string pw2 = textBox4.Text;
            if (pw1 != pw2)
            {
                // Fehlermeldung 
                return;
            }
            uint rolle_nr = (uint) comboBox1.SelectedIndex + 1;

            Adapter.CreateUser(username, email, pw1, rolle_nr);

        }
    }
}
