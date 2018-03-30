using Cyclops.Data.Common;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cyclops.Data.MySql
{
    public abstract class DataProvider
    {
        public string ConnectionStringKey { get; set; }

        protected ILogger Logger { get; set; }

        protected IConnectionStringProvider ConnectionStringProvider { get; set; }

        public virtual MySqlConnection GetConnection(string key = "")
        {
            string connectionString = !String.IsNullOrWhiteSpace(key) ? ConnectionStringProvider.Get(key) : ConnectionStringProvider.Get();
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            return new MySqlConnection(connectionString);
        }

        public Response<T> ExecuteReader<T>(
            Action<MySqlCommand, Parameters> initializeCommand,
            Action<MySqlDataReader, List<T>> borrowReader, Parameters parameters) where T : class, new()
        {
            Response<T> response = new Response<T>();
            try
            {
                using (MySqlConnection cn = GetConnection(ConnectionStringKey))
                {
                    cn.Open();
                    using (MySqlCommand cmd = cn.CreateCommand())
                    {
                        initializeCommand(cmd, parameters);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            borrowReader(reader, response.Items);
                            response.SetStatus(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var obj = (object)parameters ?? "no parameters";
                Logger.LogError(ex, "data access error", obj);
                response.SetStatus(ex);
            }
            return response;
        }

        public Response<T> ExecuteReader<T>(Action<MySqlCommand,T> initializeCommand, Action<MySqlDataReader, List<T>> borrowReader, T t) where T : class, new()
        {
            Response<T> response = new Response<T>();
            try
            {
                using (MySqlConnection cn = GetConnection(ConnectionStringKey))
                {
                    cn.Open();
                    using (MySqlCommand cmd = cn.CreateCommand())
                    {
                        initializeCommand(cmd, t);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            borrowReader(reader, response.Items);
                            response.SetStatus(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "data access error", t);
                response.SetStatus(ex);
            }
            return response;
        }


    }
}
