using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Messages;

namespace RemoteProtocol {
    public class Response : IResponse {
        public bool Status { get; }
        public int Seq { get; }

        public Response(bool status, int seq = 0) {
            Status = status;
            Seq = seq;
        }
    }
}
