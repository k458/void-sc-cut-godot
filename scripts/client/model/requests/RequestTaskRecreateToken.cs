using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using voidsccut.scripts.messageService;

namespace voidsccut.scripts.client.model.requests;

public class RequestTaskRecreateToken : RequestTask
{
    private TokenTime _tokenTime;
    
    protected override void OnStart()
    {
        Task task = Do("/recreateToken");
    }
    private async Task Do(string url)
    {
        HttpResponseMessage response;
        
        try
        {
            response = await Client.GetAsync(url);
        }
        catch (Exception e)
        {
            IsFailed = true;
            return;
        }
        
        if (!response.IsSuccessStatusCode)
        {
            IsFailed = true;
            return;
        }
        
        try
        {
            _tokenTime = await response.Content.ReadFromJsonAsync<TokenTime>(Config.JsonOptions);
        }
        catch (Exception e)
        {
            IsFailed = true;
            return;
        }
        IsFinished = true;
    }
    
    protected override void OnFailure()
    {
        MessageTransmitter.TransmitMessage(MessageType.ClientAuthorizationFailed);
    }

    protected override void OnFinish()
    {
        Aggregator.SetTokenTime(_tokenTime);
        MessageTransmitter.TransmitMessage(MessageType.NewTokenAvailable);
    }
}