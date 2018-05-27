using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Biscuite.Extra;
using RemoteProtocol;
using RemoteProtocol.Entities;
using RemoteProtocol.Messages;

namespace Biscuite.Windows {
    class LoginWindowViewModel {
        public ICommand LoginCommand { get; }

        public string Username { get; set; }
        private Window AttachedWindow { get; }

        public LoginWindowViewModel(Window window) {
            LoginCommand = new DelegateCommand((o) => Login(o));
            AttachedWindow = window;
            Client.Instance.OnResponseReceived += OnConnectResponse;
        }

        private void OnConnectResponse(ResponseReceivedEventArgs args) {
            if (args.Message.GetType() != typeof(ConnectResponse)) return;

            var connectResponse = (ConnectResponse)args.Message;
            if (connectResponse.Status == false) {
                MessageBox.Show("Incorrect credentials");
            } else {
                Application.Current.Dispatcher.Invoke(() => {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    AttachedWindow.Close();
                });
                Client.Instance.OnResponseReceived -= OnConnectResponse;
            }
        }

        private void Login(object passwrodBox) {
            var password = ((PasswordBox)passwrodBox).Password;
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password)) {
                MessageBox.Show("Please enter valid credentials", "Invalid username or password");
            } else {
                Client.Instance.SendMessage(new ConnectRequest(Username, password));
            }
        }

        private void OnConnect(ConnectResponse connectResponse) {
            
        }
    }
}
