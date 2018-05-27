using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Entities;

namespace RemoteProtocol {
    public class Game {
        private static int _globalID = 0;
        public static List<Game> Games = new List<Game>();

        public User Player1 { get; }
        public User Player2 { get; }
        public int GameSize = 4;
        public int GameID { get; }

        public Game(User player1, User player2) {
            Player1 = player1;
            Player2 = player2;
            GameID = _globalID++;
            Games.Add(this);

            StartGame(5,5);
        }

        private void StartGame(int rows, int columns) {
            Server.Instance.SendMessage(new ShowGameWindowRequest(rows, columns, GameID), Player1.ClientStream);
            Server.Instance.SendMessage(new ShowGameWindowRequest(rows, columns, GameID), Player2.ClientStream);
        }

        public void SendMessage(string name, string message) {
            Server.Instance.SendMessage(new GameSendMessageResponse(name, message), Player1.ClientStream);
            Server.Instance.SendMessage(new GameSendMessageResponse(name, message), Player2.ClientStream);
        }
    }
}
