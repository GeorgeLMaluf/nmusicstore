using Npgsql;

namespace api.Extensions
{
    public static class NpgsqlDataReaderExtension
    {
        public static int GetInt(this NpgsqlDataReader reader, string fieldName)
        {
            var index= reader.GetOrdinal(fieldName);
            return reader.IsDBNull(index) ? 0 : reader.GetInt32(index);
        }

        public static string GetString(this NpgsqlDataReader reader, string fieldName)
        {
            var index = reader.GetOrdinal(fieldName);
            return reader.IsDBNull(index) ? string.Empty : reader.GetString(index);
        }
        
    }
}