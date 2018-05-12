using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol.Entities {
    public class ResponseReceivedEventArgs {
        public IResponse Response { get; }

        public ResponseReceivedEventArgs(IResponse response) {
            Response = response;
        }
    }
}
