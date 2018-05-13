using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Messages;

namespace RemoteProtocol.Entities {
    public class Client : RemoteTCP {
        private static Client _instance;
        public static Client Instance {
            get {
                if (_instance == null) _instance = new Client();
                return _instance;
            }
        }

        private TcpClient _server;

        public event Action<ResponseReceivedEventArgs> OnResponseReceived;

        private Client(string address = "127.0.0.1", int port = 5432) : base(address, port) {
            Initialize();
        }

        private void Initialize() {
            _server = new TcpClient();
            _server.Connect(Address, Port);
            Task.Run(() => WaitForResponse());
        }

        public void SendMessage(IRequest message) {
            base.SendMessage(message, _server.GetStream());
        }

        public void WaitForResponse() {
            while (true) {
                var message = new byte[BUFFER_SIZE];
                _server.GetStream().Read(message, 0, BUFFER_SIZE);
                var receivedMessage = DeserializeMessage(message);
                OnResponseReceived(new ResponseReceivedEventArgs(receivedMessage));
            }
        }
    }
}
