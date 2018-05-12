using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Messages;

namespace RemoteProtocol.Entities {
    public static class ExecuteActions {
        private static Dictionary<Type, Action<IRequest, Socket>> _actions = new Dictionary<Type, Action<IRequest, Socket>>();

        public static void HandleConnect(IRequest request, Socket client) {
            var connectRequest = (ConnectRequest)request;
            Server.Instance.Users.Add(client, new User(connectRequest.Username, client));
            var response = new ConnectResponse(true, request.Seq);

            Server.Instance.SendMessage(response, new NetworkStream(client));
        }

        public static void HandleSendMessages(IRequest request, Socket client) {
            var sendMessageRequest = (SendMessageRequest)request;
            foreach (var user in Server.Instance.Users) {
                Server.Instance.SendMessage(new SendMessageResponse(user.Value.Name, sendMessageRequest.Message), user.Value.ClientStream);
            }
        }

        internal static void DetermineRequest(IRequest objectMessage, Socket client) {
            _actions[objectMessage.GetType()].Invoke(objectMessage, client);
        }

        static ExecuteActions() {
            _actions.Add(typeof(ConnectRequest), HandleConnect);
            _actions.Add(typeof(SendMessageRequest), HandleSendMessages);
        }
    }
}
