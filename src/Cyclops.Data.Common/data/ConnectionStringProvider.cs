using Microsoft.Extensions.Configuration;
using System;

namespace Cyclops.Data.Common
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private Func<string> _GetConnection = null;
        private IConfiguration _Configuration;
        string IConnectionStringProvider.Get<T>()
        {
            if (_GetConnection == null)
            {
                string typeName = typeof(T).Name.ToLower();
                string key = String.Format("{0}.storage", typeName);
                return _Configuration.GetConnectionString(key);
            }
            else
            {
                return _GetConnection();
            }
        }

        string IConnectionStringProvider.Get(string key)
        {
            return Get(key);
        }

        string IConnectionStringProvider.Get()
        {
            return Get("storage");
        }

        private string Get(string key)
        {
            if (_GetConnection == null)
            {
                return _Configuration.GetConnectionString(key);
            }
            else
            {
                return _GetConnection();
            }
        }

        public ConnectionStringProvider(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public ConnectionStringProvider(Func<string> getConnection)
        {
            _GetConnection = getConnection;
        }
    }
}
