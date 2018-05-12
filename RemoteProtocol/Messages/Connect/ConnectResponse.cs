using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol.Messages {
    public class ConnectResponse : Response {
        public ConnectResponse(bool status, int seq = 0) : base(status, seq) {
        }
    }
}
