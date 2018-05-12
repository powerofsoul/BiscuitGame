using System;

namespace RemoteProtocol.Entities {
    public interface IRequest {
        int Seq { get; }
    }
}