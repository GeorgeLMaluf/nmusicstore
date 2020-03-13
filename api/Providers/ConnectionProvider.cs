using Npgsql;

namespace api.Providers
{
    public class ConnectionProvider
    {
        public static NpgsqlConnection GetConnection()
        {
            var cnx = new NpgsqlConnection(Program.cnnString);
            cnx.Open();
            return cnx;
        }
    }
}