using System.Net.Http;
using System.Threading.Tasks;
using voidsccut.scripts.messageService;
using voidsccut.scripts.shared;

namespace voidsccut.scripts.client.model.requests;

public abstract class RequestTask : IProcessable
{
    protected HttpClient Client;
    protected IRequestTaskResultAggregator Aggregator { get; private set; }
    protected IMessageTransmitter MessageTransmitter{ get; private set; }
    
    
    public bool IsFinished { get; protected set; } = false;
    private bool IsStarted = false;
    private bool IsInit = false;
    public bool IsFailed{ get; protected set; } = false;

    public void Init(HttpClient client, IRequestTaskResultAggregator aggregator, MessageManager messageTransmitter)
    {
        IsInit = true;
        Client = client;
        Aggregator = aggregator;
        MessageTransmitter = messageTransmitter;
    }

    public void Process(float deltaTime)
    {
        if (!IsInit)
        {
            IsFinished = true;
        }
        else if (!IsStarted)
        {
            IsStarted = true;
            IsFinished = false;
            OnStart();
        }
        else if (IsFailed)
        {
            IsFinished = true;
            OnFailure();
        }
        else if (IsFinished)
        {
            OnFinish();
        }
    }
    protected abstract void OnStart();
    protected abstract void OnFailure();
    protected abstract void OnFinish();

    public override string ToString()
    {
        string status = IsFailed ? "Failed" : IsFinished ? "Finished" : IsStarted ? "Started" : "NONE";
        return this.GetType().ToString() + status;
    }
}