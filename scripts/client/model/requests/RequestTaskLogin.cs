using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using voidsccut.scripts.messageService;
using voidsccut.scripts.shared.serverTypes;

namespace voidsccut.scripts.client.model.requests;

public class RequestTaskLogin(UserNamePassword userNamePassword) : IRequestTask
{
    public bool IsStarted { get; private set; } = false;
    public bool IsCompleted => _task != null && _task.IsCompleted;
    public bool IsFailed { get; private set; } = false;
    private Task<TokenTime> _task;

    public void SendRequest(HttpClient client)
    {
        IsStarted = true;
        _task = PostDataAsync(client, Config.ServerUrl+"/login");
    }

    private async Task<TokenTime> PostDataAsync(HttpClient httpClient, string url)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, userNamePassword);
        if (!response.IsSuccessStatusCode)
        {
            IsFailed = true;
            return null;
        }
        try
        {
            return await response.Content.ReadFromJsonAsync<TokenTime>(Config.JsonOptions);
        }
        catch (Exception e)
        {
            IsFailed = true;
            return null;
        }
    }
    
    public void ApplyResults(IRequestTaskResultAggregator aggregator)
    {
        aggregator.SetTokenTime(_task.Result);
    }
    public void OnSuccessMessaging(MessageManager manager)
    {
        manager.TransmitMessage(MessageType.NewTokenAvailable);
        manager.TransmitMessage(MessageType.ClientAuthorized);
    }
    public void OnFailureMessaging(MessageManager manager)
    {
        manager.TransmitMessage(MessageType.ClientAuthorizationFailed);
    }
}