using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using voidsccut.scripts.messageService;
using voidsccut.scripts.shared.serverTypes;

namespace voidsccut.scripts.client.model.requests;

public class RequestTaskUsers :IRequestTask
{
    public bool IsStarted { get; private set; } = false;
    public bool IsCompleted => _task != null && _task.IsCompleted;
    public bool IsFailed { get; private set; } = false;
    private Task<List<UserEntity>> _task;
    public void SendRequest(HttpClient client)
    {
        IsStarted = true;
        _task = GetDataAsync(client, Config.ServerUrl+"/users");
    }

    private async Task<List<UserEntity>> GetDataAsync(HttpClient httpClient, string url)
    {
        HttpResponseMessage response = await httpClient.GetAsync(url);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch(Exception e)
        {
            IsFailed = true;
            return null;
        }
        try
        {
            //Game.Main.Log(await response.Content.ReadAsStringAsync());
            return await response.Content.ReadFromJsonAsync<List<UserEntity>>(Config.JsonOptions);
        }
        catch (Exception e)
        {
            IsFailed = true;
            return null;
        }
    }
    
    
    public void ApplyResults(IRequestTaskResultAggregator aggregator)
    {
        aggregator.SetUsers(_task.Result);
    }

    public void OnSuccessMessaging(MessageManager manager)
    {
        
    }

    public void OnFailureMessaging(MessageManager manager)
    {
        
    }
}