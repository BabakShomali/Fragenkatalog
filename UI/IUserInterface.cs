using Fragenkatalog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragenkatalog.UI
{
    public delegate void LoginDelegate(string username, string password);
    public delegate void CreateUserDelegate(string username, string email, string pw, uint rolle_nr);

    interface IUserInterface
    {
        // 1. Eventhandler registrieren
        void RegistriereLoginHandler(LoginDelegate ld);
        void RegistriereCreateUserHandler(CreateUserDelegate cud);

        // 2. Anzeigen des UI
        void ShowStartUI();
        void ShowAdminUI(Benutzer eingeloggterBenutzer);
        void ShowDozentUI(Benutzer eingeloggterBenutzer);
        void ShowSchuelerUI(Benutzer eingeloggterBenutzer);

        void ShowUserAddedUI(Benutzer neuerBenutzer);
    }
}
