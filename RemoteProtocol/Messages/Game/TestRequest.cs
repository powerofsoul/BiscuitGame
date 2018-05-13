using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol {
    public class TestRequest : Request {
        public string M { get; }

        public TestRequest(string m) {
            M = m;
        }
    }
}
