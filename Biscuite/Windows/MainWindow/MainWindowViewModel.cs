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
        public string SelectedUser { get; set; }

        public ICommand SendMessageCommand { get; }
        public ICommand ChallangeCommand { get; }

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
            ChallangeCommand = new DelegateCommand(() => Challange());
            ListenMessages();
        }

        private void Challange() {
            Client.Instance.SendMessage(new StartGameRequest(SelectedUser));
        }

        private void SendMessage() {
            Client.Instance.SendMessage(new SendMessageRequest(Message));
            Message = "";
        }

        private void ListenMessages() {
            Client.Instance.OnResponseReceived += AddMessage;
            Client.Instance.OnResponseReceived += RefreshUsers;
            Client.Instance.OnResponseReceived += OnChallangeRequest;
            Client.Instance.OnResponseReceived += OnTestRequest;
        }

        private void OnTestRequest(ResponseReceivedEventArgs args) {
            if (args.Message.GetType() != typeof(TestRequest)) return;
            var test = (TestRequest)args.Message;

            MessageBox.Show(test.M);
        }

        private void OnChallangeRequest(ResponseReceivedEventArgs args) {
            if(args.Message.GetType() != typeof(ChallangeRequest)) return;
            var request = (ChallangeRequest)args.Message;

            var dialogResult = MessageBox.Show($"{request.FromUser}", " has challanged you !! Do you accept?", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes) {
                Client.Instance.SendMessage(new ChallangeResponse(request.FromUser, true));
            } else if (dialogResult == MessageBoxResult.No) {
                Client.Instance.SendMessage(new ChallangeResponse(request.FromUser, false));
            }
        }

        private void RefreshUsers(ResponseReceivedEventArgs args) {
            if (args.Message.GetType() != typeof(UserListMessage)) return;
            var response = (UserListMessage)args.Message;
            ConnectedPeople = new ObservableCollection<string>(response.Users);
        }

        private void AddMessage(ResponseReceivedEventArgs args) {
            if (args.Message.GetType() != typeof(SendMessageResponse)) return;

            var responseMessage = (SendMessageResponse)args.Message;
            Application.Current.Dispatcher.Invoke(() => {
                Chat += $"{responseMessage.UserName}:{responseMessage.Message}{Environment.NewLine}";
            });
        }
    }
}
