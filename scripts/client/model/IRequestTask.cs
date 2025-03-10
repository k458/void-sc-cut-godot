using System.Net.Http;

namespace voidsccut.scripts.client.model;

public interface IRequestTask
{
    void SendRequest(HttpClient client);
    bool IsStarted { get; }
    bool IsCompleted { get; }
    bool IsFailed { get; }
    void ApplyResults(IRequestTaskResultAggregator aggregator);
}