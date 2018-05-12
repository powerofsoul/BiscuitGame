using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol.Messages
{
    public class ConnectRequest : Request
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ConnectRequest(string username, string password, int seq=0) : base(seq) {
            Username = username;
            Password = password;
        }
    }
}
