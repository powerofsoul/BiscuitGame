using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Entities;

namespace RemoteProtocol {
    public class SendMessageRequest : Request {
        public string Message { get; }

        public SendMessageRequest(string message) : base(0){
            Message = message;
        }
    }
}
