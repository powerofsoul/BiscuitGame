using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol {
    public class ChallangeResponse : Request {
        public string Opponent { get; set; }
        public bool Accepted { get; set; }

        public ChallangeResponse(string opponent, bool accepted) {
            Opponent = opponent;
            Accepted = accepted;
        }
    }
}
