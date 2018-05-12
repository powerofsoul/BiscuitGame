using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol.Entities {
    public class User {
        public string Name;
        public Socket ClientSocket;
    }
}
