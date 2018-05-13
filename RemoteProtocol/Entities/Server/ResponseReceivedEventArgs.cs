using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol.Entities {
    public class ResponseReceivedEventArgs {
        public object Message { get; }

        public ResponseReceivedEventArgs(object response) {
            Message = response;
        }
    }
}
