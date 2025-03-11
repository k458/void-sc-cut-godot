using System.Net.Http;
using voidsccut.scripts.messageService;

namespace voidsccut.scripts.client.model;

public interface IRequestTask
{
    void SendRequest(HttpClient client);
    bool IsStarted { get; }
    bool IsCompleted { get; }
    bool IsFailed { get; }
    void ApplyResults(IRequestTaskResultAggregator aggregator);
    void OnSuccessMessaging(MessageManager manager);
    void OnFailureMessaging(MessageManager manager);
}