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
    public class GenderService
    {
        #region Private Methods
        protected Gender BuscarModel(NpgsqlDataReader reader) {
            var model = new Gender{
                id = reader.GetInt("id"),
                description = reader.GetString("description")
            };
            return model;
        }
        #endregion

        protected void DefinirParametros(NpgsqlCommand cmd, Gender gender, bool isInsert)
        {
            if (isInsert) {
                cmd.Parameters.Add(new NpgsqlParameter{ ParameterName = "@id", Direction = ParameterDirection.Output});
                cmd.Parameters.AddWithValue("@created_at", System.DateTime.Now);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", gender.id);
            }            
            cmd.Parameters.AddWithValue("@description", gender.description);
            cmd.Parameters.AddWithValue("@updated_at", System.DateTime.Now);

        }


        #region Public Methods
        public IEnumerable BuscarAll(string intervalo, int pg)
        {
            if (String.IsNullOrEmpty(intervalo))
            {
                intervalo = string.Empty;
            }
            var SQL = new StringBuilder();
            SQL.Append("SELECT id, description ");
            SQL.Append("FROM genders ");
            SQL.Append("WHERE UPPER(description) LIKE '%'||@intervalo||'%' ");
            if (pg > -1) {
                SQL.Append($"OFFSET {10 * (pg -1)} ");
                SQL.Append($"LIMIT {10}");
            }
            using (var cnx = ConnectionProvider.GetConnection()) {
                var cmd = cnx.CreateCommand();
                cmd.CommandText = SQL.ToString();
                cmd.Parameters.AddWithValue("@intervalo", intervalo.ToUpper());
                var retorno = new List<Gender>();
                using(var reader = cmd.ExecuteReader())
                {
                    while(reader.Read()) {
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
            SQL.Append("FROM genders ");
            SQL.Append("WHERE UPPER(description) LIKE '%'||@intervalo||'%' ");
            using (var cnx = ConnectionProvider.GetConnection()) {
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