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

        static ExecuteActions() {
            _actions.Add(typeof(ConnectRequest), HandleConnect);
            _actions.Add(typeof(SendMessageRequest), HandleSendMessages);
            _actions.Add(typeof(StartGameRequest), HandleStartGameRequest);
            _actions.Add(typeof(ChallangeResponse), HandleChallangeResponse);
            _actions.Add(typeof(GameSendMessageRequest), HandleGameSendMessages);
            _actions.Add(typeof(GameMoveMessageRequest), HandleGameMoveMessageRequest);
        }

        

        public static void HandleConnect(IRequest request, Socket client) {
            var connectRequest = (ConnectRequest)request;
            Server.Instance.Users.Add(client, new User(connectRequest.Username, client));
            var response = new ConnectResponse(true, request.Seq);

            Server.Instance.SendMessage(response, new NetworkStream(client));
            Server.Instance.SendUserList();
        }

        public static void HandleSendMessages(IRequest request, Socket client) {
            var sendMessageRequest = (SendMessageRequest)request;
            foreach (var user in Server.Instance.Users) {
                Server.Instance.SendMessage(new SendMessageResponse(Server.Instance.Users[client].Name, sendMessageRequest.Message), user.Value.ClientStream);
            }
        }

        private static void HandleGameMoveMessageRequest(IRequest arg1, Socket arg2) {
            var request = (GameMoveMessageRequest)arg1;

            Game.Games[request.GameId].HandleMovement(request, arg2);
        }

        public static void HandleGameSendMessages(IRequest request, Socket client) {
            var sendMessageRequest = (GameSendMessageRequest)request;
            Game.Games[sendMessageRequest.GameId].SendMessage(Server.Instance.Users[client].Name, sendMessageRequest.Message);
        }

        private static void HandleStartGameRequest(IRequest arg, Socket client) {
            var request = (StartGameRequest)arg;
            var chllangedUser = Server.Instance.Users.First(u => u.Value.Name.Equals(request.Opponent));
            Server.Instance.SendMessage(new ChallangeRequest(Server.Instance.Users[client].Name), new NetworkStream(chllangedUser.Key));
        }


        private static void HandleChallangeResponse(IRequest arg, Socket client) {
            var response = (ChallangeResponse)arg;
            var secondUser = Server.Instance.Users.First(u => u.Value.Name.Equals(response.Opponent)).Value;
            Server.Instance.Games.Add(new Game(Server.Instance.Users[client], secondUser));
        }

        internal static void DetermineRequest(IRequest objectMessage, Socket client) {
            _actions[objectMessage.GetType()].Invoke(objectMessage, client);
        }

    }
}
