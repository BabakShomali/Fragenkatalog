using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fragenkatalog.Model;

// Test-Kommentar für GIT
namespace Fragenkatalog.Datenhaltung.DB.MySql
{
    public class DbBenutzer_Frage_Fach_ReadException : Exception
    {
        static private readonly string message = "Error message ausgeben";

        public DbBenutzer_Frage_Fach_ReadException() : base(message)
        {
        }
    }
    class DbBenutzer_Frage_Fach : Benutzer_Frage_Fach
    {
        public DbBenutzer_Frage_Fach(int richtig, int falsch, Benutzer benutzer, Frage frage, Fach fach) : base(richtig, falsch, benutzer, frage, fach)
        {

        }

        static public DbBenutzer_Frage_Fach Create(Connector connector, int richtig, int falsch, Benutzer benutzer, Frage frage, Fach fach)
        {
            connector.Connection.Open();

            string query = "INSERT INTO T_Benutzer_Fragen_Faecher (p_f_benutzer_nr, p_f_frage_nr, p_f_fach_nr, richtig, falsch) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');";
            query = String.Format(query, benutzer, frage, fach, richtig, falsch);
            uint benutzer_frage_fach_nr = (uint)connector.ExecuteNonQuery(query);

            connector.Connection.Close();


            return Read(connector, benutzer.Benutzer_nr, frage.Frage_nr, fach.Fach_nr);
        }


        static public List<DbBenutzer_Frage_Fach> Read(Connector connector, uint benutzer_nr)
        {
            List<DbBenutzer_Frage_Fach> benutzerfragefachliste = new List<DbBenutzer_Frage_Fach>();
            DbBenutzer_Frage_Fach benutzerfragefach = null;
            Benutzer benutzer = DbBenutzer.Read(connector, benutzer_nr);
            connector.Connection.Open();
            string query = "SELECT * FROM t_user_fragen WHERE p_f_benutzer_nr = " + benutzer_nr;
            DbDataReader reader = connector.ExecuteReader(query);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int _benutzer_nr = (int)reader["p_f_benutzer_nr"];
                    uint _fach_nr = (uint)reader["p_f_fach_nr"];
                    uint _frage_nr = (uint)reader["p_f_frage_nr"];
                    int _richtig = (int)reader["richtig"];
                    int _falsch = (int)reader["falsch"];
                    Fach fach = DbFach.Read(connector, _fach_nr);
                    Frage frage = DbFrage.Read(connector, _frage_nr);
                    benutzerfragefach = new DbBenutzer_Frage_Fach(_richtig, _falsch, benutzer, frage, fach);
                    benutzerfragefachliste.Add(benutzerfragefach);
                }
            }

            else
            {
                connector.Connection.Close();
                throw new DbFrageReadException();
            }
            connector.Connection.Close();
            return benutzerfragefachliste;
        }
        static public DbBenutzer_Frage_Fach Read(Connector connector, uint benutzer_nr, uint frage_nr, uint fach_nr)
        {

            DbBenutzer_Frage_Fach dbBenutzerFrageFach = null;
            Fach fach = DbFach.Read(connector, fach_nr);
            Frage frage = DbFrage.Read(connector, frage_nr);
            Benutzer benutzer = DbBenutzer.Read(connector, benutzer_nr);

            connector.Connection.Open();
            string query = "SELECT * FROM T_Benutzer_Fragen_Faecher WHERE p_f_benutzer_nr = " + benutzer_nr + " AND p_f_frage_nr" + frage_nr + " AND p_f_fach_nr" + fach_nr;
            DbDataReader reader = connector.ExecuteReader(query);
            if (reader.HasRows)
            {
                int _richtig = (int)reader["richtig"];
                int _falsch = (int)reader["falsch"];
                dbBenutzerFrageFach = new DbBenutzer_Frage_Fach(_richtig, _falsch, benutzer, frage, fach);
            }
            else
            {
                connector.Connection.Close();
                throw new DbBenutzer_Frage_Fach_ReadException();
            }
            connector.Connection.Close();
            return dbBenutzerFrageFach;
        }

        public void Update(Connector connector)
        {
            connector.Connection.Open();

            string query = "UPDATE T_Benutzer_Frage_Fach SET richtig = '{0}', falsch = '{1}' WHERE  p_f_benutzer_nr = " + this.Benutzer.Benutzer_nr + " AND p_f_frage_nr" + this.Frage.Frage_nr + " AND p_f_fach_nr" + this.Fach.Fach_nr;
            query = String.Format(query, this.Richtig, this.Falsch);
            connector.ExecuteNonQuery(query);

            connector.Connection.Close();
        }

        public void Delete(Connector connector)
        {
            connector.Connection.Open();

            string query = "DELETE FROM T_Fach WHERE p_f_benutzer_nr = " + this.Benutzer.Benutzer_nr + " AND p_f_frage_nr" + this.Frage.Frage_nr + " AND p_f_fach_nr" + this.Fach.Fach_nr;
            connector.ExecuteNonQuery(query);

            connector.Connection.Close();
        }
    }
}
