using Fragenkatalog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragenkatalog.Datenhaltung.DB.MySql
{
    class Adapter : IDatenhaltung
    {

        // Implementierung der Schnittstelle IDatenhaltung
        public Benutzer BenutzerAnlegen(string login_name, string email_adresse, string passwort, uint rollen_nr)
        {
            DbAdmin dbAdmin = (DbAdmin) AppStatus.EingeloggterBenutzer;

            return dbAdmin.Create(login_name, email_adresse, passwort, rollen_nr);
        }

        public Benutzer SchuelerAnlegen(string login_name, string email_adresse, string passwort)
        {
            return BenutzerAnlegen(login_name, email_adresse, passwort, 3);
        }

        public Benutzer DozentAnlegen(string login_name, string email_adresse, string passwort)
        {
            return BenutzerAnlegen(login_name, email_adresse, passwort, 2);
        }

        public void BenutzerSpeichern(Benutzer benutzer)
        {
            // bei dieser Art Caste wird keine Exception geschmissen
            // Falls der Cast-Typ falsch ist, wird null zurück gegeben
            DbDozent dbDozent = AppStatus.EingeloggterBenutzer as DbDozent;

            // Falls zu speichernder benutzer ein Schüler ist (rollen_nr==3) 
            // und eingeloggter Benutzer ein Dozent ist
            if (benutzer.Rollen_nr == 3 && dbDozent!=null)
            {
                dbDozent.Save(benutzer);
                return;
            }

            DbAdmin dbAdmin = (DbAdmin)AppStatus.EingeloggterBenutzer;
            dbAdmin.Save(benutzer);
        }

        public Benutzer Einloggen(string login_name, string passwort)
        {
            return DbBenutzer.Login(login_name, passwort);
        }
    }
}
