using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProtocol {
    public interface IResponse {
        bool Status { get; }
        int Seq { get; }
    }
}
