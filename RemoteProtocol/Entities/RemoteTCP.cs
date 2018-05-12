using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RemoteProtocol.Messages;

namespace RemoteProtocol.Entities {
    public class RemoteTCP {
        public string Address { get; }
        public int Port { get; }
        internal const int BUFFER_SIZE = 10000;

        public RemoteTCP(string address, int port) {
            Address = address;
            Port = port;
        }

        internal void SendMessage(object message, Stream stream) {
            var messageWrapper = new MessageWrapper(message, message.GetType().ToString());
            var jsonMessage = JsonConvert.SerializeObject(messageWrapper);
            Console.WriteLine("Send: " + jsonMessage.Replace("\0", ""));
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(jsonMessage);
            stream.Write(ba, 0, ba.Length);
        }

        public static object DeserializeMessage(byte[] message) {
            string receivedMessage = Encoding.Default.GetString(message);
            Console.WriteLine("Received:" + receivedMessage.Replace("\0", ""));
            var messageWrapper = JsonConvert.DeserializeObject<MessageWrapper>(receivedMessage);
            var objectMessage = JsonConvert.DeserializeObject(Convert.ToString(messageWrapper.Message), Type.GetType(messageWrapper.MessageType));
            return objectMessage;
        }
    }
}
