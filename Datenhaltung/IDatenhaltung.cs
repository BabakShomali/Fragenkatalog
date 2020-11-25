using Fragenkatalog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragenkatalog.Datenhaltung
{
    interface IDatenhaltung
    {
        // #bv001 : Benutzer anlegen
        Benutzer BenutzerAnlegen(string login_name, string email_adresse, string passwort, uint rollen_nr);

        // #bv001-1 : Schueler anlegen
        Benutzer SchuelerAnlegen(string login_name, string email_adresse, string passwort);

        // #bv001-2 : Dozent anlegen
        Benutzer DozentAnlegen(string login_name, string email_adresse, string passwort);

        // #bv005
        void BenutzerSpeichern(Benutzer benutzer);

        Benutzer Einloggen(string login_name, string passwort);
    }
}
