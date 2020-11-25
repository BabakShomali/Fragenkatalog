using Fragenkatalog.Datenhaltung.DB.MySql;
using Fragenkatalog.Model;
using System;
using System.Windows.Forms;

namespace Fragenkatalog
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Applikationsweite Einstellungen
            AppStatus.Datenhaltungsadapter = new Datenhaltung.DB.MySql.Adapter();
            AppStatus.UIAdapter = new UI.Windows.Forms.Adapter();

            AppStatus.UIAdapter.RegistriereLoginHandler(DoLogin);
            AppStatus.UIAdapter.RegistriereCreateUserHandler(DoCreateUser);
            AppStatus.UIAdapter.ShowStartUI();
        }

        private static void DoCreateUser(string username, string email, string pw, uint rolle_nr)
        {   
            // TODO : Exceptionhandling ähnlich zu DoLogin
            Benutzer benutzer = AppStatus.Datenhaltungsadapter.BenutzerAnlegen(username, email, pw, rolle_nr);
            // MessageBox.Show("New user " + benutzer.Login_name + " has been created!", "Success!");
            AppStatus.UIAdapter.ShowUserAddedUI(benutzer);
        }

        private static void DoLogin(string username, string password)
        {
            try
            {
                AppStatus.EingeloggterBenutzer = AppStatus.Datenhaltungsadapter.Einloggen(username, password);

                if (AppStatus.EingeloggterBenutzer is Admin)
                {
                    AppStatus.UIAdapter.ShowAdminUI(AppStatus.EingeloggterBenutzer);
                }
                else if (AppStatus.EingeloggterBenutzer is Dozent)
                {
                    AppStatus.UIAdapter.ShowDozentUI(AppStatus.EingeloggterBenutzer);
                }
                else if (AppStatus.EingeloggterBenutzer is Schueler)
                {
                    AppStatus.UIAdapter.ShowSchuelerUI(AppStatus.EingeloggterBenutzer);
                }
            }
            catch (Datenhaltung.LoginFailedException)
            {
                throw new UI.UIException("Login");
            }
        }
    }
}
