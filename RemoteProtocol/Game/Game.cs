using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Entities;

namespace RemoteProtocol {
    public class Game {
        private static int _globalID = 0;
        public static List<Game> Games = new List<Game>();

        public User Player1 { get; }
        public User Player2 { get; }

        public const int GameSize = 5;
        public int GameID { get; }

        private User _currentUser;
        public User CurrentUser {
            get {
                return _currentUser;
            }
            set {
                _currentUser = value;
                var message = $"{_currentUser.Name} turn";
                Server.Instance.SendMessage(new GameSendMessageResponse("SERVER:", message), Player1.ClientStream);
                Server.Instance.SendMessage(new GameSendMessageResponse("SERVER:", message), Player2.ClientStream);
            }
        }

        public List<Tuple<Point, Point>> Lines { get; }
        public List<Square> Squares { get; set; }

        public Game(User player1, User player2) {
            Player1 = player1;
            Player2 = player2;
            GameID = _globalID++;
            Games.Add(this);
            Squares = new List<Square>();
            Lines = new List<Tuple<Point, Point>>();
            CurrentUser = Player1;
            StartGame(5, 5);

            var message = $"{Player1.Name}  VS  {Player2.Name} Start. Current player is {CurrentUser.Name}";
            Server.Instance.SendMessage(new GameSendMessageResponse("SERVER:", message), Player1.ClientStream);
            Server.Instance.SendMessage(new GameSendMessageResponse("SERVER:", message), Player2.ClientStream);


        }

        private void StartGame(int rows, int columns) {
            Server.Instance.SendMessage(new ShowGameWindowRequest(rows, columns, GameID), Player1.ClientStream);
            Server.Instance.SendMessage(new ShowGameWindowRequest(rows, columns, GameID), Player2.ClientStream);
        }

        public void SendMessage(string name, string message) {
            Server.Instance.SendMessage(new GameSendMessageResponse(name, message), Player1.ClientStream);
            Server.Instance.SendMessage(new GameSendMessageResponse(name, message), Player2.ClientStream);
        }

        internal void HandleMovement(GameMoveMessageRequest request, Socket sender) {
            CheckIfSomeonewon();
            if (CurrentUser.ClientSocket != sender) return;

            var line = new Tuple<Point, Point>(request.From, request.To);

            if (Lines.Contains(line)) {
                Server.Instance.SendMessage(new GameSendMessageResponse("Server:", "Invalid move"), new NetworkStream(sender));
                return;
            }

            Lines.Add(line);
            var startingPoint = new Point(0, 0);
            bool addedNew = false;
            for (int i = 0; i < GameSize * GameSize; i++) {
                if (Squares.FirstOrDefault(s => s.Id == i) != null) {
                    if (startingPoint.Y == GameSize - 1) {
                        startingPoint.X++;
                        startingPoint.Y = 0;
                    } else
                        startingPoint.Y++;
                    continue;
                }
                var lines = 0;
                if (Lines.Contains(new Tuple<Point, Point>(startingPoint, new Point(startingPoint.X, startingPoint.Y + 1))))
                    lines++;

                if (Lines.Contains(new Tuple<Point, Point>(startingPoint, new Point(startingPoint.X + 1, startingPoint.Y))))
                    lines++;

                if (Lines.Contains(new Tuple<Point, Point>(new Point(startingPoint.X + 1, startingPoint.Y), new Point(startingPoint.X + 1, startingPoint.Y + 1))))
                    lines++;

                if (Lines.Contains(new Tuple<Point, Point>(new Point(startingPoint.X, startingPoint.Y + 1), new Point(startingPoint.X + 1, startingPoint.Y + 1))))
                    lines++;

                if (lines == 4) {
                    Squares.Add(new Square(Server.Instance.Users[sender].Name, i));
                    addedNew = true;
                }

                if (startingPoint.Y == GameSize - 1) {
                    startingPoint.X++;
                    startingPoint.Y = 0;
                } else
                    startingPoint.Y++;
            }
            if (addedNew) {
                CurrentUser = _currentUser;
            } else {
                CurrentUser = CurrentUser == Player1 ? Player2 : Player1;
            }

            var response = new GameMoveMessageResponse(Lines, Squares);
            Server.Instance.SendMessage(response, Player1.ClientStream);
            Server.Instance.SendMessage(response, Player2.ClientStream);
            CheckIfSomeonewon();
        }

        private void CheckIfSomeonewon() {
            if (Squares.Count == GameSize * GameSize) {
                var player1Squares = Squares.Where(o => o.Id == 1).Count();
                var player2Squares = Squares.Where(o => o.Id == 2).Count();

                var message = "";
                if (player1Squares == player2Squares) {
                    message = "The game is a tie";
                } else if (player1Squares > player2Squares) {
                    message = $"{Player1.Name} has won";
                } else {
                    message = $"{Player2.Name} has won";
                }

                return;
            }
        }
    }

    public class Square {
        public string Player { get; }
        public int Id { get; }

        public Square(string player, int id) {
            Player = player;
            Id = id;
        }
    }
}
