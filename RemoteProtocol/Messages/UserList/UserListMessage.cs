using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol{
    public class UserListMessage : Response {
        public IEnumerable<string> Users { get; }

        public UserListMessage(IEnumerable<string> users): base(true, 0) {
            Users = users;
        }
    }
}
