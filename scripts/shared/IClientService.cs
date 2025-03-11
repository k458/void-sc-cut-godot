using voidsccut.scripts.client.model;
using voidsccut.scripts.shared.serverTypes;

namespace voidsccut.scripts.shared;

public interface IClientService
{
    void Init();
    IProcessable Processable { get; }
    void AddRequest(RequestType type, UserNamePassword userNamePassword);
    void AddRequest(RequestType type);
    void Logout();
    IRequestTaskResultProvider ResultProvider { get; }
}