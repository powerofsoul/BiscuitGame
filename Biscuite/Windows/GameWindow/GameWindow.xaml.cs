using System;
using System.Collections.Generic;
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

namespace Biscuite.Windows {
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window {
        public GameWindow(int rows, int columns) {
            InitializeComponent();
            for(int i = 0; i < rows; i++) {
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }

            for(int j = 0; j < columns; j++) {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for(int i = 0; i < rows; i++) {
                for(int j = 0; j < columns; j++)
                    Spawn(i, j);
            }
        }

        void Spawn(int row, int column) {
            var leftButton = new Button() {
                HorizontalAlignment= HorizontalAlignment.Left,
                Width=5,
            };
            var rightButton = new Button() {
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 5,
            };
            var toptButton = new Button() {
                VerticalAlignment = VerticalAlignment.Top,
                Height = 5,
            };
            var bottomButton = new Button() {
                VerticalAlignment = VerticalAlignment.Bottom,
                Height = 5,
            };

            Grid.SetRow(leftButton, row);
            Grid.SetColumn(leftButton, column);

            Grid.SetRow(rightButton, row);
            Grid.SetColumn(rightButton, column);

            Grid.SetRow(toptButton, row);
            Grid.SetColumn(toptButton, column);

            Grid.SetRow(bottomButton, row);
            Grid.SetColumn(bottomButton, column);

            MainGrid.Children.Add(leftButton);
            MainGrid.Children.Add(rightButton);
            MainGrid.Children.Add(toptButton);
            MainGrid.Children.Add(bottomButton);
        }
    }
}
