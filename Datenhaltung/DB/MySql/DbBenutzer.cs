using Fragenkatalog.Model;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace Fragenkatalog.Datenhaltung.DB.MySql
{
    class DbBenutzer : Benutzer
    {
        // Rollen-Login für Benutzer
        static private readonly string rollenLoginName = "fragenkatalog_benutzer";
        static private readonly string rollenPasswort = "UdBadrPwqr23w$*a";

        static public Connector Connector { get; private set; }

        public DbBenutzer(uint benutzer_nr, string login_name, string email_adresse, string passwort, uint rollen_nr) : base(benutzer_nr, login_name, email_adresse, passwort, rollen_nr)
        {
            if (Connector is null)
            {
                Connector = new Connector(rollenLoginName, rollenPasswort);
            }
        }

        public static Benutzer Login(string user, string password)
        {
            if (Connector is null)
            {
                Connector = new Connector(rollenLoginName, rollenPasswort);
            }
            Connector.Connection.Open();
            string loginString = "SELECT * FROM t_benutzer WHERE login_name='" + user + "' AND passwort=PASSWORD('" + password + "');";
            DbDataReader reader = Connector.ExecuteReader(loginString);

            if (reader.HasRows)
            {
                reader.Read();

                uint benutzer_nr = (uint)reader["p_benutzer_nr"];
                string login_name = reader["login_name"] as string;
                string passwort = reader["passwort"] as string;
                string email_adresse = reader["email_adresse"] as string;
                uint rollen_nr = (uint)reader["fk_rolle_nr"];

                Connector.Connection.Close();
                switch (rollen_nr)
                {
                    case 1:
                        return new DbAdmin(benutzer_nr, login_name, email_adresse, passwort);
                    case 2:
                        return new DbDozent(benutzer_nr, login_name, email_adresse, passwort);
                    case 3:
                        return new DbSchueler(benutzer_nr, login_name, email_adresse, passwort);
                    default:
                        throw new LoginFailedException();
                }
            }
            else
            {
                Connector.Connection.Close();
                throw new LoginFailedException();
            }
        }

        // CRUD - Methoden start

        public static Benutzer Create(Connector connector, string login_name, string email_adresse, string passwort, uint rollen_nr)
        {
            string tabellenName;
            switch (rollen_nr)
            {
                case 1:
                    tabellenName = "T_Admins";
                    break;
                case 2:
                    tabellenName = "T_Dozenten";
                    break;
                case 3:
                    tabellenName = "T_Schueler";
                    break;
                default:
                    // TODO besser als Check-Constraint in der DB realisieren
                    throw new UnsupportedRoleException();
            }

            MySqlConnection connection = connector.Connection;

            connection.Open();

            string query = String.Format("INSERT INTO T_Benutzer (`login_name`,`email_adresse`, `passwort`, `fk_rolle_nr`) VALUES ('{0}', '{1}', PASSWORD('{2}'), '{3}' );",
                                                login_name, email_adresse, passwort, rollen_nr);
            connector.ExecuteNonQuery(query);

            query = "SELECT p_benutzer_nr FROM T_Benutzer WHERE login_name='" + login_name + "';";
            uint benutzer_nr = (uint)connector.ExecuteScalar(query);

            query = "INSERT INTO `" + tabellenName + "` (`p_f_benutzer_nr`) VALUES (" + benutzer_nr + ");";
            connector.ExecuteNonQuery(query);

            // das muss eventuel aus einer globalen Konfiguration gelesen werden
            uint[] kapazitaet = { 10, 20, 30, 40, 50 }; // Karten
            uint[] anzahlWiederholungen = { 3, 2, 1, 0, 0 };
            uint[] wiederholungsspanne = { 1, 2, 5, 10, 14 }; //  Tage

            for (int i = 0; i < 5; i++)
            {
                query = "INSERT INTO T_Faecher (kapazitaet, anzahl_wiederholungen, wiederholungs_zeitspanne, f_benutzer_nr ) VALUES ('{0}', '{1}', '{2}', '{3}');";
                query = String.Format(query, kapazitaet[i], anzahlWiederholungen[i], wiederholungsspanne[i], benutzer_nr);
                uint nummer_neues_fach = (uint)connector.ExecuteNonQuery(query);
            }

            connector.Connection.Close();

            return Read(connector, benutzer_nr);
        }

        public static Benutzer Read(Connector connector, uint benutzer_nr)
        {
            // unser Rückgabe-Objekt
            Benutzer benutzer = null;

            connector.Connection.Open();
            string query = "SELECT * FROM T_Benutzer WHERE p_benutzer_nr = " + benutzer_nr;
            DbDataReader reader = connector.ExecuteReader(query);
            if (reader.HasRows)
            {
                reader.Read();
                uint _p_benutzer_nr = (uint)reader["p_benutzer_nr"];
                string _login_name = (string)reader["login_name"];
                string _email_adresse = (string)reader["email_adresse"];
                string _passwort = (string)reader["passwort"];
                uint _fk_rolle_nr = (uint)reader["fk_rolle_nr"];

                switch (_fk_rolle_nr)
                {
                    case 1:
                        benutzer = new DbAdmin(_p_benutzer_nr, _login_name, _email_adresse, _passwort);
                        break;
                    case 2:
                        benutzer = new DbDozent(_p_benutzer_nr, _login_name, _email_adresse, _passwort);
                        break;
                    case 3:
                        benutzer = new DbSchueler(_p_benutzer_nr, _login_name, _email_adresse, _passwort);
                        break;
                    default:
                        // TODO besser als Check-Constraint in der DB realisieren
                        throw new UnsupportedRoleException();
                }
                reader.Close();

                // Jetzt lesen wir noch die Fächer ein
                // Warum ist das eigentlich hier implementiert worden ?
                // 
                query = "SELECT * FROM T_Faecher WHERE f_benutzer_nr = " + benutzer.Benutzer_nr;
                reader = connector.ExecuteReader(query);
                if (reader.HasRows)
                {
                    int i = 0;
                    while (i < 5 && reader.Read())
                    {
                        uint _fach_nr = (uint)reader["p_fach_nr"];
                        uint _kapazitaet = (uint)reader["kapazitaet"];
                        uint _anzahl_wiederholungen = (uint)reader["anzahl_wiederholungen"];
                        uint _wiederholungs_zeitspanne = (uint)reader["wiederholungs_zeitspanne"];
                        uint _benutzer_nr = (uint)reader["f_benutzer_nr"];
                        DbFach dbFach = new DbFach(_fach_nr, _kapazitaet, _anzahl_wiederholungen, _wiederholungs_zeitspanne, _benutzer_nr);

                        benutzer.FachListe[i] = dbFach;
                        i++;
                    }
                }
                reader.Close();
            }
            else
            {
                connector.Connection.Close();
                throw new DbFrageReadException();
            }
            connector.Connection.Close();

            return benutzer;
        }

        public void Update(Connector connector)
        {

            connector.Connection.Open();


            string query = "UPDATE T_Benutzer SET (`login_name`,`email_adresse`, `passwort`, `fk_rolle_nr`) VALUES ('{0}', '{1}', PASSWORD('{2}'), '{3}' ); WHERE p_benutzer_nr = '{4}'";
                                               
            query = String.Format(query, Login_name, Email_adresse, Passwort, Rollen_nr, Benutzer_nr);
            connector.ExecuteNonQuery(query);

            connector.Connection.Close();

        }

        public void Delete(Connector connector)
        {
            connector.Connection.Open();

            string query = "DELETE FROM T_Benutzer WHERE p_benutzer_nr=" + Benutzer_nr;
            connector.ExecuteNonQuery(query);

            connector.Connection.Close();
        }
        // CRUD - Methoden ende

    }
}
