using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RemoteProtocol;
using RemoteProtocol.Entities;

namespace Biscuite.Windows {
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window {
        public int GameId { get; }
        public Dictionary<string, Button> Lines { get; }
        public Dictionary<int, StackPanel> Stackpanels { get; }
        public string Username { get; }
        public GameWindow(int rows, int columns, int gameId, string username) {
            InitializeComponent();
            GameId = GameId;
            DataContext = new GameWindowViewModel(gameId);
            Username = username;
            Lines = new Dictionary<string, Button>();
            Stackpanels = new Dictionary<int, StackPanel>();
            for (int i = 0; i < rows; i++) {
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int j = 0; j < columns; j++) {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++)
                    Spawn(i, j);
            }

            for (int i = 0; i < rows; i++) {
                SpawnRight(i, columns - 1);
            }

            for (int i = 0; i < columns; i++) {
                SpawnBottom(rows - 1, i);
            }
            var k = 0;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    var stack = new StackPanel() {
                        Background = System.Windows.Media.Brushes.Transparent,
                        IsHitTestVisible = false
                    };
                    Grid.SetRow(stack, i);
                    Grid.SetColumn(stack, j);

                    MainGrid.Children.Add(stack);
                    Stackpanels.Add(k++, stack);
                }

            }

            Client.Instance.OnResponseReceived += OnGameResponseMessage;
        }

        private void OnGameResponseMessage(ResponseReceivedEventArgs args) {
            if (args.Message.GetType() != typeof(GameMoveMessageResponse)) return;
            var response = (GameMoveMessageResponse)args.Message;

            foreach (var line in response.Lines) {
                Application.Current.Dispatcher.Invoke(() => {
                    Lines[$"A{line.Item1.X}a{line.Item1.Y}b{line.Item2.X}a{line.Item2.Y}"].Background = System.Windows.Media.Brushes.Red;
                });
            }

            foreach (var square in response.Squares) {
                Application.Current.Dispatcher.Invoke(() => {
                    Stackpanels[square.Id].Background = square.Player == Username ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
                });
            }
        }

        void Spawn(int row, int column) {
            var leftButton = new Button() {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 5,
                Name = $"A{row}a{column}b{row + 1}a{column}"
            };

            Lines.Add(leftButton.Name, leftButton);
            var toptButton = new Button() {
                VerticalAlignment = VerticalAlignment.Top,
                Height = 5,
                Name = $"A{row}a{column}b{row}a{column + 1}"
            };
            Lines.Add(toptButton.Name, toptButton);
            Grid.SetRow(leftButton, row);
            Grid.SetColumn(leftButton, column);

            Grid.SetRow(toptButton, row);
            Grid.SetColumn(toptButton, column);

            MainGrid.Children.Add(leftButton);
            MainGrid.Children.Add(toptButton);


            leftButton.Click += SendRequest;
            toptButton.Click += SendRequest;
        }

        void SpawnRight(int row, int column) {
            var rightButton = new Button() {
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 5,
                Name = $"A{row}a{column + 1}b{row + 1}a{column + 1}",
            };
            Lines.Add(rightButton.Name, rightButton);
            Grid.SetRow(rightButton, row);
            Grid.SetColumn(rightButton, column);
            rightButton.Click += SendRequest;
            MainGrid.Children.Add(rightButton);
        }

        private void SendRequest(object sender, RoutedEventArgs e) {
            var points = ((Button)sender).Name.Remove(0, 1).Split('b');
            var from = new System.Drawing.Point(int.Parse(points[0].Split('a')[0]), int.Parse(points[0].Split('a')[1]));
            var to = new System.Drawing.Point(int.Parse(points[1].Split('a')[0]), int.Parse(points[1].Split('a')[1]));
            Client.Instance.SendMessage(new GameMoveMessageRequest(from, to, GameId));
        }

        void SpawnBottom(int row, int column) {
            var bottomButton = new Button() {
                VerticalAlignment = VerticalAlignment.Bottom,
                Height = 5,
                Name = $"A{row + 1}a{column}b{row + 1}a{column + 1}"
            };
            Lines.Add(bottomButton.Name, bottomButton);
            Grid.SetRow(bottomButton, row);
            Grid.SetColumn(bottomButton, column);

            MainGrid.Children.Add(bottomButton);

            bottomButton.Click += SendRequest;
        }
    }
}
