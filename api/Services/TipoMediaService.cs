using Npgsql;
using System.Data;
using api.Models;
using api.Extensions;
using api.Providers;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;

namespace api.Services
{
    public class TipoMediaService
    {
        #region Private Methods
        protected TipoMedia BuscarModel(NpgsqlDataReader reader) {
            var model = new TipoMedia {
                id = reader.GetInt("id"),
                description = reader.GetString("description")
            };
            return model;
        }

        protected void DefinirParametros(NpgsqlCommand cmd, TipoMedia tipo, bool isInsert)
        {
            if (isInsert) {
                cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "@id", Direction = ParameterDirection.Output});
                cmd.Parameters.AddWithValue("@created_at", System.DateTime.Now);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", tipo.id);
            }
            cmd.Parameters.AddWithValue("@description", tipo.description);
            cmd.Parameters.AddWithValue("@updated_at", System.DateTime.Now);
        }

        #endregion

        #region Public Methods
        public IEnumerable BuscarAll(string intervalo, int pg)
        {
            if (String.IsNullOrEmpty(intervalo)) {
                intervalo = string.Empty;
            }
            var SQL = new StringBuilder();
            SQL.Append("SELECT id, description ");
            SQL.Append("FROM tipo_media ");
            SQL.Append("WHERE UPPER(description) LIKE '%'||@intervalo||'%' ");
            if (pg > -1) {
                SQL.Append($"OFFSET {10 * (pg -1)} ");
                SQL.Append($"LIMIT {10}");
            }
            using (var cnx = ConnectionProvider.GetConnection())
            {
                var cmd = cnx.CreateCommand();
                cmd.CommandText = SQL.ToString();
                cmd.Parameters.AddWithValue("@invervalo", intervalo.ToUpper());
                var retorno = new List<TipoMedia>();
                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var item = BuscarModel(reader);
                        retorno.Add(item);
                    }
                }
                return retorno;
            }
        }

        public int BuscarCount(string intervalo)
        {
            if (String.IsNullOrWhiteSpace(intervalo))
            {
                intervalo = String.Empty;
            }
            var SQL = new StringBuilder();
            SQL.Append("SELECT COUNT(id) AS COUNT ");
            SQL.Append("FROM tipo_media ");
            SQL.Append("WHERE UPPER(description) LIKE '%'||@intervalo||'%' ");
            using (var cnx = ConnectionProvider.GetConnection())
            {
                var cmd = cnx.CreateCommand();
                cmd.CommandText = SQL.ToString();
                cmd.Parameters.AddWithValue("@intervalo", intervalo.ToUpper());
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt("COUNT");
                    }
                }
                return 0;
            }
        }
        #endregion
    }
}