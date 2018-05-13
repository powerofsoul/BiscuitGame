using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol {
    public class StartGameRequest : Request {
        public string Opponent { get; set; }

        public StartGameRequest(string challangedUser) {
            Opponent = challangedUser;
        }
    }
}
