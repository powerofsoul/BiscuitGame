using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol {
    public class GameSendMessageResponse : Response{
        public string UserName { get; }
        public string Message { get; }

        public GameSendMessageResponse(string userName, string message) : base(true, 0) {
            UserName = userName;
            Message = message;
        }
    }
}
