using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Entities;

namespace RemoteProtocol {
    public class GameMoveMessageRequest : Request {
        public Point From { get; }
        public Point To { get; }
        public int GameId { get; }

        public GameMoveMessageRequest(Point from, Point to, int gameId) : base(0) {
            From = from;
            To = to;
            GameId = gameId;
        }
    }
}
