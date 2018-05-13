using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol {
    public class ChallangeRequest : Request {
        public string FromUser { get; set; }

        public ChallangeRequest(string from) {
            FromUser = from;
        }
    }
}
