using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteProtocol.Entities;
using RemoteProtocol.Messages;

namespace RemoteProtocol {
    public class Request : IRequest {
        public int Seq { get; }

        public Request(int seq = 0) {
            Seq = seq;
        }
    }
}
