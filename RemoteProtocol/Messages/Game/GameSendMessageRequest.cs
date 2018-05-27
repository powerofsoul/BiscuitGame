using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Entities;

namespace RemoteProtocol {
    public class GameSendMessageRequest : Request {
        public string Message { get; }
        public int GameId { get; }

        public GameSendMessageRequest(string message, int gameId) {
            Message = message;
            GameId = gameId;
        }
    }
}
