using Fragenkatalog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fragenkatalog.UI.Windows.Forms
{
    class Adapter : IUserInterface
    {
        static private event LoginDelegate LoginHandler;
        static private event CreateUserDelegate CreateUserHandler;

        public void ShowStartUI()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        static internal void Login(string username, string password)
        {
            try
            {
                LoginHandler(username, password);
            }
            catch(UIException ex)
            {
                MessageBox.Show(((Exception) ex).Message);
            }
        }

        static internal void CreateUser(string username, string email, string pw, uint rolle_nr)
        {
            try
            {
                CreateUserHandler(username,email, pw, rolle_nr);
            }
            catch (UIException ex)
            {
                MessageBox.Show(((Exception)ex).Message);
            }
        }

        public void RegistriereLoginHandler(LoginDelegate ld)
        {
            LoginHandler = ld;
        }


        public void RegistriereCreateUserHandler(CreateUserDelegate cud)
        {
            CreateUserHandler = cud;
        }


        public void ShowAdminUI(Benutzer eingeloggterBenutzer)
        {
            // MessageBox.Show("Benutzer ist ein : Administrator");
            AdminUIForm adf = new AdminUIForm();
            adf.ShowDialog();
        }

        public void ShowDozentUI(Benutzer eingeloggterBenutzer)
        {
            MessageBox.Show("Benutzer ist ein : Dozent");
        }

        public void ShowSchuelerUI(Benutzer eingeloggterBenutzer)
        {
            MessageBox.Show("Benutzer ist ein : Schüler");
        }

        public void ShowUserAddedUI(Benutzer neuerBenutzer)
        {
            MessageBox.Show("New user " + neuerBenutzer.Login_name + " has been created!", "User Added");
        }
    }
}
