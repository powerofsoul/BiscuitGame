using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol {
    public class ShowGameWindowRequest : Request {
        public int Rows { get; }
        public int Columns { get; }
        public int GameId { get; }

        public ShowGameWindowRequest(int rows, int columns, int gameId) {
            Rows = rows;
            Columns = columns;
            GameId = gameId;
        }
    }
}
