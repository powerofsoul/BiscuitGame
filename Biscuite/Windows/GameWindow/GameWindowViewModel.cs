using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Biscuite.Extra;
using RemoteProtocol;
using RemoteProtocol.Entities;

namespace Biscuite.Windows {
    public class GameWindowViewModel : ViewModelBase {
        private string _chat;
        public string Chat {
            get {
                return _chat ?? "";
            }
            set {
                _chat = value;
                OnPropertyChanged(nameof(Chat));
            }
        }
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
        public int GameId { get; }
        public ICommand SendMessageCommand { get; }

        public GameWindowViewModel(int gameId) {
            Client.Instance.OnResponseReceived += AddMessage;
            SendMessageCommand = new DelegateCommand(() => SendMessage());
            GameId = gameId;
        }

        private void AddMessage(ResponseReceivedEventArgs args) {
            if (args.Message.GetType() != typeof(GameSendMessageResponse)) return;

            var responseMessage = (GameSendMessageResponse)args.Message;
            Application.Current.Dispatcher.Invoke(() => {
                Chat += $"{responseMessage.UserName}:{responseMessage.Message}{Environment.NewLine}";
            });
        }

        private void SendMessage() {
            Client.Instance.SendMessage(new GameSendMessageRequest(Message, GameId));
            Message = "";
        }
    }
}
