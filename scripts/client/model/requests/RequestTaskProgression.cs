using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using voidsccut.scripts.shared.serverTypes.progression;

namespace voidsccut.scripts.client.model.requests;

public class RequestTaskProgression :IRequestTask
{
    public bool IsStarted { get; private set; } = false;
    public bool IsCompleted => _task != null && _task.IsCompleted;
    public bool IsFailed { get; private set; } = false;
    private Task<ProgressionDto> _task;
    public void SendRequest(HttpClient client)
    {
        IsStarted = true;
        _task = GetDataAsync(client, Config.ServerUrl+"/progression");
    }

    private async Task<ProgressionDto> GetDataAsync(HttpClient httpClient, string url)
    {
        HttpResponseMessage response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            IsFailed = true;
            return null;
        }
        try
        {
            return await response.Content.ReadFromJsonAsync<ProgressionDto>(Config.JsonOptions);
        }
        catch (Exception e)
        {
            IsFailed = true;
            return null;
        }
    }
    
    
    public void ApplyResults(IRequestTaskResultAggregator aggregator)
    {
        aggregator.SetProgressionDto(_task.Result);
    }
}