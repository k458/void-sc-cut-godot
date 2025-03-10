using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace voidsccut.scripts.client.model.requests;

public class RequestTaskRecreateToken : IRequestTask
{
    public bool IsStarted { get; private set; } = false;
    public bool IsCompleted => _task != null && _task.IsCompleted;
    public bool IsFailed { get; private set; } = false;
    private Task<TokenTime> _task;

    public void SendRequest(HttpClient client)
    {
        IsStarted = true;
        _task = GetDataAsync(client, Config.ServerUrl+"/recreateToken");
    }

    private async Task<TokenTime> GetDataAsync(HttpClient httpClient, string url)
    {
        HttpResponseMessage response = await httpClient.GetAsync(url);
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
}