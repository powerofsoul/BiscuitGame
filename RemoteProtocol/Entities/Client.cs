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

        private Client(string address="127.0.0.1", int port=5432) : base(address, port) {
            Initialize();
        }

        private void Initialize() {
            _server = new TcpClient();
            _server.Connect(Address, Port);
        }

        public void SendMessage(IRequest message) {
            base.SendMessage(message, _server.GetStream());
        }

        public void WaitForResponse<T>(Action<T> action) where T : IResponse {
            Task.Run(() => {
                var message = new byte[BUFFER_SIZE];
                _server.GetStream().Read(message, 0, BUFFER_SIZE);
                var receivedMessage = DeserializeMessage(message);

                if (receivedMessage.GetType() == typeof(T))
                    action.Invoke((T)receivedMessage);
                else
                    WaitForResponse<T>(action);
            });
         }
    }
}
