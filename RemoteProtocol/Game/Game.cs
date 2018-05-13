using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Entities;

namespace RemoteProtocol {
    public class Game {
        private static int _globalID = 0;

        public User Player1 { get; }
        public User Player2 { get; }
        public int GameSize = 4;
        public int GameID { get; }

        public Game(User player1, User player2) {
            Player1 = player1;
            Player2 = player2;
            GameID = _globalID++;

            StartGame();
        }

        private void StartGame() {
            Server.Instance.SendMessage(new TestRequest($"Game id is {GameID}"), Player1.ClientStream);
            Server.Instance.SendMessage(new TestRequest($"Game id is {GameID}"), Player2.ClientStream);
        }
    }
}
