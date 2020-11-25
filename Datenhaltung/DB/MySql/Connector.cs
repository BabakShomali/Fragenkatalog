using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Fragenkatalog.Datenhaltung.DB.MySql
{
    class Connector
    {
        private MySqlConnection connection;
        private string host = "localhost";
        private string database = "fragenkatalog";

        public MySqlConnection Connection { get { return connection; } }

        public Connector(string loginName, string passwort)
        {
            string conString = "server='" + host + "';database='" + database + "';uid='" + loginName + "';password='" + passwort + "';";
            connection = new MySqlConnection(conString);
        }

        public DbDataReader ExecuteReader(string query)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            DbDataReader reader = command.ExecuteReader();

            return reader;
        }

        public int ExecuteNonQuery(string query)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            return command.ExecuteNonQuery();
        }

        internal object ExecuteScalar(string query)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            return command.ExecuteScalar();
        }
    }
}
