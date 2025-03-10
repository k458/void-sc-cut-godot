using voidsccut.scripts.client.model;
using voidsccut.scripts.shared.serverTypes;

namespace voidsccut.scripts.shared;

public interface IClientService
{
    IProcessable Processable { get; }
    void AddRequest(RequestType type, UserNamePassword userNamePassword);
    void AddRequest(RequestType type);
    IRequestTaskResultProvider ResultProvider { get; }
}