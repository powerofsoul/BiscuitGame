using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Messages;

namespace RemoteProtocol.Entities {
    public static class ExecuteActions {
        private static Dictionary<Type, Func<IRequest, IResponse>> _actions = new Dictionary<Type, Func<IRequest, IResponse>>();

        public static IResponse HandleConnect(IRequest request) {
            return new ConnectResponse(true, request.Seq);
        }

        internal static void DetermineRequest(IRequest objectMessage, Socket client) {
            Server.Instance.SendMessage(_actions[objectMessage.GetType()].Invoke(objectMessage), new NetworkStream(client));
        }

        static ExecuteActions() {
            _actions.Add(typeof(ConnectRequest), HandleConnect);
        }
    }
}
