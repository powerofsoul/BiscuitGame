using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Biscuite.Extra;

namespace Biscuite.Windows {
    class LoginWindowViewModel {
        public ICommand LoginCommand { get; }

        public string Username { get; set; }
        private Window AttachedWindow { get; }

        public LoginWindowViewModel(Window window) {
            LoginCommand = new DelegateCommand((o) => Login(o));
            AttachedWindow = window;
        }

        private void Login(object passwrodBox) {
            var password = ((PasswordBox)passwrodBox).Password;
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password)) {
                MessageBox.Show("Please enter valid credentials", "Invalid username or password");
            } else {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                AttachedWindow.Close();
            }
        }
    }
}
