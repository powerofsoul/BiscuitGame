using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol {
    public class SendMessageResponse : Response{
        public string UserName { get; }
        public string Message { get; }

        public SendMessageResponse(string userName, string message) : base(true, 0) {
            UserName = userName;
            Message = message;
        }
    }
}
