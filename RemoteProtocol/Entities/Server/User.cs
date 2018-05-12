using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol.Entities {
    public class User {
        public string Name { get; }
        public Socket ClientSocket { get; }
        public Stream ClientStream => new NetworkStream(ClientSocket);

        public User(string name, Socket clientSocket) {
            Name = name;
            ClientSocket = clientSocket;
        }
    }
}
