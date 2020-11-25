using Fragenkatalog.Model;
using MySql.Data.MySqlClient;
using System;


namespace Fragenkatalog.Datenhaltung.DB.MySql
{
    class DbAdmin : Admin
    {
        // Rollen-Login für Admins
        static private readonly string rollenLoginName = "fragenkatalog_admin";
        static private readonly string rollenPasswort = "LKasdZ%&!?*";


        static public Connector Connector { get; private set; }

        public DbAdmin(uint benutzer_nr, string login_name, string email_adresse, string passwort) : base(benutzer_nr, login_name, email_adresse, passwort)
        {
            Connector = new Connector(rollenLoginName, rollenPasswort);
        }

        // Erzeugen eines Admins :  Create(login_name, email_adresse, passwort);
        // Erzeugen eines Dozenten  Create(login_name, email_adresse, passwort, 2);
        // Erzeugen eines Schueler  Create(login_name, email_adresse, passwort, 3);
        public Benutzer Create(string login_name, string email_adresse, string passwort, uint rollen_nr = 1)
        {
            return DbBenutzer.Create(Connector, login_name, email_adresse, passwort, rollen_nr);
        }

        //[Obsolete("Diese Methode ist obsolet. Bitte verwenden Sie die Methode Create", true)]
        //public Benutzer BenutzerAnlegen(string login_name, string email_adresse, string passwort, uint rollen_nr)
        //{
        //    return Create(login_name, email_adresse, passwort, rollen_nr);
        //}
        public Admin Read(Connector connector, uint benutzer_nr)
        {
            return (Admin)DbBenutzer.Read(connector, benutzer_nr);
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

        // TODO : bitte asap implementieren 
        public void Save(Benutzer benutzer)
        {
            throw new NotImplementedException();
        }
    }
}
