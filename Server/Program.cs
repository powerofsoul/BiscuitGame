using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol;

namespace Server {
    class Program {
        static void Main(string[] args) {
            var server = RemoteProtocol.Entities.Server.Instance;

            Console.Read();
        }
    }
}
