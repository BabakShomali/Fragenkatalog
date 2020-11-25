using Fragenkatalog.Model;
using System;

namespace Fragenkatalog.Datenhaltung.DB.MySql
{
    class DbDozent : Dozent
    {
        // Rollen-Login für Dozenten
        static private readonly string rollenLoginName = "fragenkatalog_dozent";
        static private readonly string rollenPasswort = "GJKa1PdZ%&asw";


        static public Connector Connector { get; private set; }

        public DbDozent(uint benutzer_nr, string login_name, string email_adresse, string passwort) : base(benutzer_nr, login_name, email_adresse, passwort)
        {
            Connector = new Connector(rollenLoginName, rollenPasswort);
        }

        public Dozent Read(Connector connector, uint benutzer_nr)
        {
            return (Dozent)DbBenutzer.Read(connector, benutzer_nr);
        }

        public void Update(Connector connector)
        {
            DbBenutzer updateBenutzer = new DbBenutzer(Benutzer_nr, Login_name, Email_adresse, Passwort, Rollen_nr);
            updateBenutzer.Update(connector);
        }

        public void Delete(Connector connector)
        {
            DbBenutzer deleteBenutzer = new DbBenutzer(Benutzer_nr, Login_name, Email_adresse, Passwort, Rollen_nr);
            deleteBenutzer.Delete(connector);
        }

        // Condition : benutzer ist ein Schüler
        // TODO : implementieren 
        internal void Save(Benutzer benutzer)
        {
            if (benutzer is Schueler)
            {
                throw new NotImplementedException();
            }
        }
    }
}
