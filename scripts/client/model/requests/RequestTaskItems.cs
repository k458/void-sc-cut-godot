using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using voidsccut.scripts.messageService;
using voidsccut.scripts.shared.serverTypes.items;

namespace voidsccut.scripts.client.model.requests;

public class RequestTaskItems:IRequestTask
{
    public bool IsStarted { get; private set; } = false;
    public bool IsCompleted => _task != null && _task.IsCompleted;
    public bool IsFailed { get; private set; } = false;
    private Task<ItemsDto> _task;
    public void SendRequest(HttpClient client)
    {
        IsStarted = true;
        _task = GetDataAsync(client, Config.ServerUrl+"/items");
    }

    private async Task<ItemsDto> GetDataAsync(HttpClient httpClient, string url)
    {
        HttpResponseMessage response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            IsFailed = true;
            return null;
        }
        try
        {
            return await response.Content.ReadFromJsonAsync<ItemsDto>(Config.JsonOptions);
        }
        catch (Exception e)
        {
            IsFailed = true;
            return null;
        }
    }
    
    
    public void ApplyResults(IRequestTaskResultAggregator aggregator)
    {
        aggregator.SetItemsDto(_task.Result);
    }
    public void OnSuccessMessaging(MessageManager manager)
    {
        
    }
    public void OnFailureMessaging(MessageManager manager)
    {
        
    }
}