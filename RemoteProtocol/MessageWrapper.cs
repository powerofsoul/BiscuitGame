using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Entities;

namespace RemoteProtocol {
    public class MessageWrapper {
        public string MessageType { get; }
        public object Message { get; }

        public MessageWrapper(object message, string messageType) {
            MessageType = messageType;
            Message = message;
        }
    }
}
