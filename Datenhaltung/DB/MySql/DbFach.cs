using Fragenkatalog.Model;
using System;
using System.Data.Common;

namespace Fragenkatalog.Datenhaltung.DB.MySql
{
    public class DbFachReadException : Exception
    {
        static private readonly string message = "Fach konnte nicht gefunden werden";

        public DbFachReadException() : base(message)
        {
        }
    }

    class DbFach : Fach
    {
        public DbFach(uint fach_nr, uint kapazitaet, uint anzahlWiederholungen, uint wiederholungsspanne, uint benutzer_nr) : base(fach_nr, kapazitaet, anzahlWiederholungen, wiederholungsspanne)
        {
        }

        // CRUD - Funktion start

        // Create in DB
        static public DbFach Create(Connector connector, uint kapazitaet, uint anzahlWiederholungen, uint wiederholungsspanne, uint f_benutzer_nr)
        {
            connector.Connection.Open();

            string query = "INSERT INTO T_Faecher (kapazitaet, anzahl_wiederholungen, wiederholungs_zeitspanne, f_benutzer_nr ) VALUES ('{0}', '{1}', '{2}', '{3}');";
            query = String.Format(query, kapazitaet, anzahlWiederholungen, wiederholungsspanne, f_benutzer_nr);
            uint nummer_neues_fach = (uint)connector.ExecuteNonQuery(query);

            connector.Connection.Close();

            return Read(connector, nummer_neues_fach);
        }

        // Read from DB
        static public DbFach Read(Connector connector, uint fach_nr)
        {
            DbFach dbFach = null;
            connector.Connection.Open();
            string query = "SELECT * FROM T_Faecher WHERE p_fach_nr = " + fach_nr;
            DbDataReader reader = connector.ExecuteReader(query);
            if (reader.HasRows)
            {
                reader.Read();
                uint _fach_nr = (uint)reader["p_fach_nr"];
                uint _kapazitaet = (uint)reader["kapazitaet"];
                uint _anzahl_wiederholungen = (uint)reader["anzahl_wiederholungen"];
                uint _wiederholungs_zeitspanne = (uint)reader["wiederholungs_zeitspanne"];
                uint _benutzer_nr = (uint)reader["f_benutzer_nr"];
                dbFach = new DbFach(_fach_nr, _kapazitaet, _anzahl_wiederholungen, _wiederholungs_zeitspanne, _benutzer_nr);
            }
            else
            {
                connector.Connection.Close();
                throw new DbFachReadException();
            }

            connector.Connection.Close();
            return dbFach;
        }

        // Update to DB
        public void Update(Connector connector)
        {
            connector.Connection.Open();

            string query = "UPDATE T_Fach SET kapazitaet='{0}', anzahl_wiederholungen='{1}', wiederholungs_zeitspanne='{2}', f_benutzer_nr='{3}' WHERE p_fach_nr='{4}'";
            query = String.Format(query, Kapazitaet, AnzahlWiederholungen, Wiederholungsspanne, Benutzer_nr);
            connector.ExecuteNonQuery(query);

            connector.Connection.Close();
        }

        // Delete fom DB
        public void Delete(Connector connector)
        {
            connector.Connection.Open();

            string query = "DELETE FROM T_Fach WHERE p_fach_nr=" + Fach_nr;
            connector.ExecuteNonQuery(query);

            connector.Connection.Close();
        }

        // CRUD - Funktion ende

    }
}
