using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Biscuite.Extra;
using RemoteProtocol;
using RemoteProtocol.Entities;
using RemoteProtocol.Messages;

namespace Biscuite.Windows {
    class MainWindowViewModel : ViewModelBase {
        private ObservableCollection<string> _connectedPeople;
        public ObservableCollection<string> ConnectedPeople {
            get {
                return _connectedPeople ?? new ObservableCollection<string>();
            }
            set {
                _connectedPeople = value;
                OnPropertyChanged(nameof(ConnectedPeople));
            }
        }

        private string _chat;
        public string Chat {
            get {
                return _chat ?? "";
            }
            set{
                _chat = value;
                OnPropertyChanged(nameof(Chat));
            }
        }

        public ICommand SendMessageCommand { get; }

        private string _message;
        public string Message {
            get {
                return _message ?? "";
            }
            set {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public MainWindowViewModel() {
            SendMessageCommand = new DelegateCommand(() => SendMessage());
            ListenMessages();
        }

        private void SendMessage() {
            Client.Instance.SendMessage(new SendMessageRequest(Message));
            Message = "";
        }

        private void ListenMessages() {
            Client.Instance.OnResponseReceived += AddMessage;
            Client.Instance.OnResponseReceived += RefreshUsers;
        }

        private void RefreshUsers(ResponseReceivedEventArgs args) {
            if (args.Response.GetType() != typeof(UserListMessage)) return;
            var response = (UserListMessage)args.Response;
            ConnectedPeople = new ObservableCollection<string>(response.Users);
        }

        private void AddMessage(ResponseReceivedEventArgs args) {
            if (args.Response.GetType() != typeof(SendMessageResponse)) return;

            var responseMessage = (SendMessageResponse)args.Response;
            Application.Current.Dispatcher.Invoke(() => {
                Chat += $"{responseMessage.UserName}:{responseMessage.Message}{Environment.NewLine}";
            });
        }
    }
}
