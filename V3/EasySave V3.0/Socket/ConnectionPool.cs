using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveApp.Socket
{
    public class ConnectionPool
    {
        public List<Connection> Connections { get; set; }
        private static ConnectionPool _instance = null;

        public ConnectionPool()
        {
            Connections = new();
        }

        public static ConnectionPool GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConnectionPool();
            }
            return _instance;
        }

        public void AddConnection(Connection connection)
        {
            Connections.Add(connection);
        }

        public void RemoveConnection(Connection connection)
        {
            Connections.Remove(connection);
        }
    }
}
