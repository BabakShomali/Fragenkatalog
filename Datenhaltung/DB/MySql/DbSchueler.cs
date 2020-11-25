using Fragenkatalog.Model;

namespace Fragenkatalog.Datenhaltung.DB.MySql
{
    class DbSchueler : Schueler
    {
        // Rollen-Login für Schüler
        static private readonly string rollenLoginName = "fragenkatalog_schueler";
        static private readonly string rollenPasswort = "SdfgrP12&qwf3a";

        static public Connector Connector { get; private set; }

        public DbSchueler(uint benutzer_nr, string login_name, string email_adresse, string passwort) : base(benutzer_nr, login_name, email_adresse, passwort)
        {
            Connector = new Connector(rollenLoginName, rollenPasswort);
        }

        // nur zum Testen
        public DbFrage ReadFrage(uint frage_nr)
        {
            return DbFrage.Read(Connector, frage_nr);
        }

        public Schueler Read(Connector connector, uint benutzer_nr)
        {
            return (Schueler)DbBenutzer.Read(connector, benutzer_nr);
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
    }
}
