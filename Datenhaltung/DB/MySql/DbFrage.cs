using Fragenkatalog.Model;
using System;
using System.Data.Common;

namespace Fragenkatalog.Datenhaltung.DB.MySql
{
    public class DbFrageReadException : Exception
    {
        static private readonly string message = "Frage konnte nicht gefunden werden";

        public DbFrageReadException() : base(message) 
        {
        }
    }

    class DbFrage : Frage
    {
        public DbFrage(uint frage_nr, string frage, char loesung, string antwortA, string antwortB, string antwortC, string antwortD) 
                    : base(frage_nr, frage, loesung, antwortA, antwortB, antwortC, antwortD)
        {
        }

        // CRUD - Funktion start

        // Create in DB
        static public DbFrage Create(Connector connector, string frage, char loesung, string antwortA, string antwortB, string antwortC, string antwortD)
        {
            connector.Connection.Open();

            string query = "INSERT INTO T_Fragen (frage, loesung, antwortA, antwortB, antwortC, antwortD) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');";
            query = String.Format(query, frage, loesung, antwortA, antwortB, antwortC, antwortD);
            uint nummer_neue_frage = (uint) connector.ExecuteNonQuery(query);

            connector.Connection.Close();

            return Read(connector, nummer_neue_frage);
        }

        // Read from DB
        static public DbFrage Read(Connector connector, uint frage_nr)
        {
            // unser Rückgabe-Objekt
            DbFrage dbFrage = null;

            connector.Connection.Open();
            string query = "SELECT * FROM T_Fragen WHERE p_frage_nr = " + frage_nr;
            DbDataReader reader = connector.ExecuteReader(query);
            if (reader.HasRows)
            {
                reader.Read();
                uint _frage_nr = (uint)reader["p_frage_nr"];
                string _frage = (string)reader["frage"];
                string _loesung = (string)reader["loesung"];
                string _antwortA = (string)reader["antwortA"];
                string _antwortB = (string)reader["antwortB"];
                string _antwortC = (string)reader["antwortC"];
                string _antwortD = (string)reader["antwortD"];
                dbFrage = new DbFrage(_frage_nr, _frage,_loesung[0],_antwortA, _antwortB, _antwortC,_antwortD);
            }
            else
            {
                connector.Connection.Close();
                throw new DbFrageReadException();
            }
            connector.Connection.Close();

            return dbFrage;
        }


        // Update to DB
        public void Update(Connector connector)
        {
            connector.Connection.Open();

            // kapazitaet, anzahl_wiederholungen, wiederholungs_zeitspanne, f_benutzer_nr
            string query = "UPDATE T_Fragen SET kapazitaet='{0}',anzahl_wiederholungen='{1}',wiederholungs_zeitspanne='{2}',f_benutzer_nr='{3}', antwortC='{4}', antwortD='{5}' WHERE p_frage_nr='{6}'";
            query = String.Format(query, Frage_, Loesung, AntwortA, AntwortB, AntwortC, AntwortD);
            connector.ExecuteNonQuery(query);

            connector.Connection.Close();
        }

        // Delete fom DB
        public void Delete(Connector connector)
        {
            connector.Connection.Open();

            string query = "DELETE FROM T_Fragen WHERE p_frage_nr="+Frage_nr;
            connector.ExecuteNonQuery(query);

            connector.Connection.Close();
        }

        // CRUD - Funktion ende
    }
}
