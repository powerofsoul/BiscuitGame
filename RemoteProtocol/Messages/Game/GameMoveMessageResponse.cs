using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol {
    public class GameMoveMessageResponse {
        public List<Tuple<Point, Point>> Lines { get; }
        public List<Square> Squares { get; }

        public GameMoveMessageResponse(List<Tuple<Point, Point>> lines, List<Square> squares) {
            Lines = lines;
            Squares = squares;
        }
    }
}
