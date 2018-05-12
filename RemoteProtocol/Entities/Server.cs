using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RemoteProtocol.Entities {
    public class Server : RemoteTCP {
        private static Server _instance;
        public static Server Instance {
            get {
                if (_instance == null) _instance = new Server();
                return _instance;
            }
        }

        public List<User> Users { get; set; }

        public Server(string address = "127.0.0.1", int port = 5432) : base(address, port) {
            Initialize();
        }

        private void Initialize() {
            Users = new List<User>();
            var thread = new Thread(HandleNewConnections);
            thread.Start();
        }

        private void HandleNewConnections() {
            var iPAddress = IPAddress.Parse(Address);
            var listener = new TcpListener(iPAddress, Port);

            listener.Start();

            while (true) {
                try {
                    var client = listener.AcceptSocket();
                    new Task(() => HandleClient(client)).Start();
                }catch(Exception e) {
                    Console.WriteLine($"Error:{e.Message}");
                    HandleNewConnections();
                }
            }
        }

        private void HandleClient(Socket client) {
            while (true) {
                var message = new byte[BUFFER_SIZE];
                client.Receive(message);
                object objectMessage = DeserializeMessage(message);
                ExecuteActions.DetermineRequest((IRequest)objectMessage, client);
            }
        }
    }
}
